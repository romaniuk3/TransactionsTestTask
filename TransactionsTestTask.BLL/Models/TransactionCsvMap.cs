using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.BLL.Models
{
    public class TransactionCsvMap : ClassMap<Transaction>
    {
        public TransactionCsvMap()
        {
            Map(m => m.Id).Name("TransactionId");
            Map(m => m.Status);
            Map(m => m.Type);
            Map(m => m.ClientName);
            Map(m => m.Amount).Convert(c => $"${c.Value.Amount}");
        }
    }
}
