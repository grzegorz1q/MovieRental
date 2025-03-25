using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Interfaces;

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
    }
}
