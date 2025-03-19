﻿using Microsoft.AspNetCore.Mvc;
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
        /// Dodaje pracownika
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDto employeeDto)
        {
            try
            {
                await _employeeService.AddEmployee(employeeDto);
                return Created();
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
    }
}
