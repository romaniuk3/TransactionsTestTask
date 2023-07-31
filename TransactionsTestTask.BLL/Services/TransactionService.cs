using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.Enums;
using TransactionsTestTask.BLL.Helpers;
using TransactionsTestTask.BLL.Models;
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
            if (transactionTypes == null || transactionTypes.Count == 0) return transactions;

            var typesToFilterBy = transactionTypes.Select(t => t.ToString()).ToList();

            return transactions.Where(t => typesToFilterBy.Contains(t.Type!));
        }

        private IQueryable<Transaction> FilterByTransactionStatus(IQueryable<Transaction> transactions, TransactionStatus? transactionStatus)
        {
            if (transactionStatus == null) return transactions;

            return transactions.Where(t => t.Status == transactionStatus.ToString());
        }

        private IQueryable<Transaction> SearchByClientName(IQueryable<Transaction> transactions, string? clientName)
        {
            if (clientName == null) return transactions;

            return transactions.Where(t => t.ClientName == clientName);
        }
    }
}