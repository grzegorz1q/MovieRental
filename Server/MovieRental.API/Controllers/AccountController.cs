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
        /// Resetowanie hasła zalogowanego pracownika - Admin, Employee
        /// </summary>
        [HttpPost("reset-employee-password")]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> ResetEmployeePassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Employee's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                await _accountService.ResetEmployeePassword(personId, resetPasswordDto);
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
        /// Resetowanie hasła zalogowanego klienta - Client
        /// </summary>
        [HttpPost("reset-client-password")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ResetClientPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Client's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                await _accountService.ResetClientPassword(personId, resetPasswordDto);
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
        /// Aktualizacja emaila zalogowanego pracownika - Admin, Employee
        /// </summary>
        [HttpPatch("employees/email")]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> UpdateEmployeeEmail(UpdateEmailDto emailDto)
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Employee's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                await _accountService.UpdateEmployeeEmail(personId, emailDto);
                return Ok("Email successfully updated!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Aktualizacja emaila zalogowanego klienta - Client
        /// </summary>
        [HttpPatch("clients/email")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> UpdateClientEmail(UpdateEmailDto emailDto)
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Client's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                await _accountService.UpdateClientEmail(personId, emailDto);
                return Ok("Email successfully updated!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Zwraca informacje o zalogowanym pracowniku - Admin, Employee
        /// </summary>
        [HttpGet("employees/me")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<IActionResult> GetLoggedEmployee()
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Employees's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                var person = await _accountService.GetLoggedEmployeeInfo(personId);
                return Ok(person);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Zwraca informacje o zalogowanym kliencie - Client
        /// </summary>
        [HttpGet("clients/me")]
        [Authorize(Roles ="Client")]
        public async Task<IActionResult> GetLoggedClient()
        {
            try
            {
                var personIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (personIdClaim == null)
                {
                    return Unauthorized("Client's ID is missing in the token.");
                }
                var personId = int.Parse(personIdClaim);
                var person = await _accountService.GetLoggedClientInfo(personId);
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
