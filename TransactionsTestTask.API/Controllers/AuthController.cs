using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionsTestTask.BLL.DTO;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.BLL.Services;
using TransactionsTestTask.BLL.Services.Contracts;

namespace TransactionsTestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerData)
        {
            var registerResult = await _authService.RegisterAsync(registerData);
            if (!registerResult.Succeeded) 
            {
                return BadRequest(new ApiError(400, registerResult.Errors));
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginData)
        {
            var loginResult = await _authService.LoginAsync(loginData);
            if (!loginResult.Succeeded)
            {
                return BadRequest(new ApiError(400, loginResult.Errors));
            }

            return Ok(loginResult.Value);
        }
    }
}
