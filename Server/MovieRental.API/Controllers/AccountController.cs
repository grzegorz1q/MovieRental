using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Dtos.Client;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Dtos.Person;
using MovieRental.Application.Interfaces;
using MovieRental.Application.Services;
using System.Security.Claims;

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
        /// <summary>
        /// Rejestracja (Klient może sam utworzyć sobie konto)
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateClientDto createClientDto)
        {
            try
            {
                await _accountService.Register(createClientDto);
                return Ok("Successfully created new client account!");
            }
            catch(ArgumentException ex) 
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Resetowanie hasła zalogowanego użytkownika - Admin, Employee, Client
        /// </summary>
        [HttpPost("reset-password")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Person's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                await _accountService.ResetPassword(personId, resetPasswordDto);
                return Ok(resetPasswordDto);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Resetowanie hasła, gdy użytkownik go zapomni. Zostaje wysyłany mail z hasłem tymczasowym
        /// </summary>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                await _accountService.ForgotPassword(email);
                return Ok("Email with temporary password successully sent");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Aktywacja konta przez pracownika
        /// </summary>
        [HttpGet("activate/employees/{employeeId}")]
        public async Task<IActionResult> ActivateEmployeeAccount([FromRoute] int employeeId)
        {
            try
            {
                await _accountService.ActivateEmployeeAccount(employeeId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Aktywacja konta przez klienta
        /// </summary>
        [HttpGet("activate/clients/{clientId}")]
        public async Task<IActionResult> ActivateClientAccount([FromRoute] int clientId)
        {
            try
            {
                await _accountService.ActivateClientAccount(clientId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Aktualizacja emaila zalogowanego użytkownika - Admin, Employee, Client
        /// </summary>
        [HttpPatch("email")]
        [Authorize]
        public async Task<IActionResult> UpdateEmail(UpdateEmailDto emailDto)
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Person's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                await _accountService.UpdateEmail(personId, emailDto);
                return Ok("Email successfully updated!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Zwraca informacje o zalogowanym użytkowniku - Admin, Employee, Client
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetLoggedPerson()
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Employees's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                var person = await _accountService.GetLoggedPersonInfo(personId);
                return Ok(person);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
