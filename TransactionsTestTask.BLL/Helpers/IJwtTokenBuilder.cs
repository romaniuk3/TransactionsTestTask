using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.BLL.Helpers
{
    public interface IJwtTokenBuilder
    {
        string GenerateToken(User user);
    }
}
