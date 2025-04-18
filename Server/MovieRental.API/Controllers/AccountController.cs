using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Interfaces;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        public AccountController(IAccountService accountService, IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }
        /// <summary>
        /// Logowanie
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var person = await _accountService.Login(loginDto);
                var token = _jwtService.GenerateToken(person);
                return Ok(token);
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized(ex.Message);
            }
        }
    }
}
