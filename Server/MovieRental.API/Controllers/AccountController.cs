using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Interfaces;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IJwtService _jwtService;
        public AccountController(IEmployeeService employeeService, IJwtService jwtService)
        {
            _employeeService = employeeService;
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
                var employee = await _employeeService.Login(loginDto);
                var token = _jwtService.GenerateToken(employee);
                return Ok(token);
            }
            catch(ArgumentNullException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
