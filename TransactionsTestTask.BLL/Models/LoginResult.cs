using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsTestTask.BLL.Models
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
