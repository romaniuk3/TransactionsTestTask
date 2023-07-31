using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.Enums;

namespace TransactionsTestTask.BLL.Helpers
{
    public class TransactionQueryParameters
    {
        public TransactionStatus? Status { get; set; }
        public List<TransactionType?>? Types { get; set; }
        public string? ClientName { get; set; }
    }
}
