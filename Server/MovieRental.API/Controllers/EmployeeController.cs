using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Interfaces;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Dodaje pracownika (Admin)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDto employeeDto)
        {
            try
            {
                await _employeeService.AddEmployee(employeeDto);
                return Ok(employeeDto);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Aktywacja konta
        /// </summary>
        [HttpGet("activate/{employeeId}")]
        public async Task<IActionResult> ActivateAccount([FromRoute] int employeeId)
        {
            try
            {
                await _employeeService.ActivateAccount(employeeId);
                return Ok();
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
            
        }
        /// <summary>
        /// Resetowanie hasła zalogowanego użytkownika (Admin, Employee)
        /// </summary>
        [HttpPost("reset-password")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var employeeIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (employeeIdClaim == null)
                {
                    return Unauthorized("Employees's ID is missing in the token.");
                }
                var employeeId = int.Parse(employeeIdClaim);
                await _employeeService.ResetPassword(employeeId, resetPasswordDto);
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
            catch(ArgumentException ex)
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
                await _employeeService.ForgotPassword(email);
                return Ok("Email with temporary password successully sent");
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
