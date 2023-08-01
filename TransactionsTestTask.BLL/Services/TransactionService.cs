using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.Enums;
using TransactionsTestTask.BLL.Helpers;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.BLL.ServiceErrors;
using TransactionsTestTask.BLL.Services.Contracts;
using TransactionsTestTask.DAL.Data;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> ImportFromExcel(IFormFile file, string? userId)
        {
            if (!FileHelper.IsExcelExtension(file.FileName))
            {
                return new ServiceResult(TransactionServiceErrors.INCORRECT_FILE_EXTENSION);
            }

            if (string.IsNullOrEmpty(userId))
            {
                return new ServiceResult(UserServiceErrors.USER_NOT_FOUND_BY_ID);
            }

            var transactions = new List<Transaction>();
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(stream);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;
            if (rowCount < 2)
            {
                return new ServiceResult(TransactionServiceErrors.INVALID_ROWS_COUNT);
            }

            for (int row = 2; row <= rowCount; row++)
            {
                var decimalAmount = FileHelper.ConvertStringToDecimal(worksheet.Cells[row, 5].Value?.ToString()?.Trim()!);
                if (decimalAmount == null)
                {
                    return new ServiceResult(TransactionServiceErrors.INCORRECT_AMOUNT_VALUE);
                }

                transactions.Add(new Transaction()
                {
                    Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                    Status = worksheet.Cells[row, 2].Value?.ToString()?.Trim(),
                    Type = worksheet.Cells[row, 3].Value?.ToString()?.Trim(),
                    ClientName = worksheet.Cells[row, 4].Value?.ToString()?.Trim(),
                    Amount = decimalAmount,
                    UserId = userId
                });
            }
            await SaveTransactions(transactions);

            return new();
        }

        private async Task SaveTransactions(List<Transaction> transactions)
        {
            await _context.UpsertTransactionsAsync(transactions, _context);
        }

        public ServiceResult<List<Transaction>> GetTransactions(TransactionQueryParameters queryParameters)
        {
            var transactions = _context.Transactions.AsNoTracking();
            transactions = FilterByTransactionTypes(transactions, queryParameters.Types);
            transactions = FilterByTransactionStatus(transactions, queryParameters.Status);
            transactions = SearchByClientName(transactions, queryParameters.ClientName);

            return new(transactions.ToList());
        }

        private IQueryable<Transaction> FilterByTransactionTypes(IQueryable<Transaction> transactions, List<TransactionType?>? transactionTypes)
        {
            if (!TransactionTypesPassed(transactionTypes)) return transactions;

            var typesToFilterBy = transactionTypes?.Select(t => t.ToString()).ToList();

            return transactions.Where(t => typesToFilterBy!.Contains(t.Type!));
        }

        private bool TransactionTypesPassed(List<TransactionType?>? transactionTypes)
        {
            return transactionTypes != null && transactionTypes.FirstOrDefault() != null;
        }

        private IQueryable<Transaction> FilterByTransactionStatus(IQueryable<Transaction> transactions, TransactionStatus? transactionStatus)
        {
            if (transactionStatus == null) return transactions;

            return transactions.Where(t => t.Status == transactionStatus.ToString());
        }

        private IQueryable<Transaction> SearchByClientName(IQueryable<Transaction> transactions, string? clientName)
        {
            if (string.IsNullOrEmpty(clientName)) return transactions;

            return transactions.Where(t => t.ClientName!.Contains(clientName));
        }
    }
}