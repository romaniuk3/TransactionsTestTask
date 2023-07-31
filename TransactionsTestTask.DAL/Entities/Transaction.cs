using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsTestTask.DAL.Entities
{
    public class Transaction : BaseEntity
    {
        public string? Status { get; set; }
        public string? Type { get; set;}
        public string? ClientName { get; set; }
        public decimal Amount { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
