using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.DTO;
using TransactionsTestTask.BLL.Models;

namespace TransactionsTestTask.BLL.Services.Contracts
{
    public interface IAuthService
    {
        Task<ServiceResult> RegisterAsync(RegisterDTO registerData);
        Task<ServiceResult<LoginResult>> LoginAsync(LoginDTO loginData);
    }
}
