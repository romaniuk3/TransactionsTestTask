using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.DTO;
using TransactionsTestTask.BLL.Helpers;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.BLL.ServiceErrors;
using TransactionsTestTask.BLL.Services.Contracts;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenBuilder _jwtTokenBuilder;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IJwtTokenBuilder jwtTokenBuilder)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtTokenBuilder = jwtTokenBuilder;
        }

        public async Task<ServiceResult<LoginResult>> LoginAsync(LoginDTO loginData)
        {
            var user = await _userManager.FindByEmailAsync(loginData.Email);
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user!, loginData.Password);

            if (user == null || !isPasswordValid)
            {
                return new ServiceResult<LoginResult>(UserServiceErrors.INCORRECT_EMAIL_OR_PASSWORD);
            }

            var token = _jwtTokenBuilder.GenerateToken(user);
            var tokenExpiresInSeconds = Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"]) * 60;
            var loginResult = new LoginResult
            {
                Token = token,
                ExpiresIn = tokenExpiresInSeconds
            };

            return new ServiceResult<LoginResult>(loginResult);
        }

        public async Task<ServiceResult> RegisterAsync(RegisterDTO registerData)
        {
            var user = new User
            {
                UserName = registerData.Username,
                Email = registerData.Email,
            };

            var createUserResult = await _userManager.CreateAsync(user, registerData.Password);
            if (!createUserResult.Succeeded)
            {
                var errors = createUserResult.Errors.Select(e => new KeyValuePair<string, string>(e.Code, e.Description)).ToList();
                return new ServiceResult(errors);
            }

            return new();
        }
    }
}
