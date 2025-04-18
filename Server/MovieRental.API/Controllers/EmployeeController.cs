using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Dtos.Person;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
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
        /// Dodaje pracownika - Admin
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
        /// Aktywacja konta przez pracownika
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
        /// Zwraca listę wszystkich pracowników - Admin
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees();
                return Ok(employees);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Zwraca pracownika o podanym id - Admin
        /// </summary>
        [HttpGet("{employeeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                var employee = await _employeeService.GetEmployee(employeeId);
                return Ok(employee);
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Zmiana roli pracownika - Admin
        /// </summary>
        [HttpPatch("roles/{employeeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole([FromRoute] int employeeId, [FromBody] ChangeRoleDto changeRoleDto)
        {
            try
            {
                await _employeeService.ChangeRole(employeeId, changeRoleDto.Role);
                return Ok($"Soccessfully changed selected employee's role to {changeRoleDto.Role.ToString()}");
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
