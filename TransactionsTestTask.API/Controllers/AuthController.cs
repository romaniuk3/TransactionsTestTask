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

        /// <summary>
        /// Register action to create new user
        /// </summary>
        /// <param name="registerData">The register data containing username, password and email</param>
        /// <returns>Returns success or an error response on failure</returns>
        /// <response code="400">Returns an error response when the register fails</response>
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

        /// <summary>
        /// Login action to get an access token
        /// </summary>
        /// <param name="loginData">The login data containing email and password</param>
        /// <returns>Returns the access token on successful login or an error response on failure</returns>
        /// <response code="200">Returns the access token on successful login</response>
        /// <response code="400">Returns an error response when the login fails</response>
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
