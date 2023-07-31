using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.DTO;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.BLL.Services.Contracts;

namespace TransactionsTestTask.BLL.Services
{
    public class AuthService : IAuthService
    {
        public Task<ServiceResult<LoginResult>> LoginAsync(LoginDTO loginData)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> RegisterAsync(RegisterDTO registerData)
        {
            throw new NotImplementedException();
        }
    }
}
