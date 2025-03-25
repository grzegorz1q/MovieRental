using AutoMapper.Configuration.Conventions;
using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task AddEmployee(CreateEmployeeDto employee);
        Task<Employee> Login(LoginDto loginDto);
        Task ActivateAccount(int employeeId);
    }
}
