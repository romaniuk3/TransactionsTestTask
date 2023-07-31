using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.Helpers;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.BLL.Services.Contracts
{
    public interface ITransactionService
    {
        ServiceResult<List<Transaction>> GetTransactions(TransactionQueryParameters queryParameters);
    }
}
