using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.Enums;
using TransactionsTestTask.BLL.Helpers;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.BLL.Services.Contracts
{
    public interface ITransactionService
    {
        Task<ServiceResult> ImportFromExcel(IFormFile excelFile, string? userId);
        ServiceResult<List<Transaction>> GetTransactions(TransactionQueryParameters queryParameters);
        Task<ServiceResult> UpdateStatusAsync(int transactionId, TransactionStatus? status);
    }
}
