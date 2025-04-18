using Microsoft.AspNetCore.Identity;
using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasher<Employee> _passwordHasherEmployee;
        private readonly IPasswordHasher<Client> _passwordHasherClient;
        public AccountService(IEmployeeRepository employeeRepository, IClientRepository clientRepository, IPasswordHasher<Employee> passwordHasherEmployee, IPasswordHasher<Client> passwordHasherClient)
        {
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _passwordHasherEmployee = passwordHasherEmployee;
            _passwordHasherClient = passwordHasherClient;
        }

        public async Task<Person> Login(LoginDto loginDto)
        {
            var employee = await _employeeRepository.GetEmployeeByEmail(loginDto.Email);
            if (employee != null && employee.IsActive)
            {
                var result = _passwordHasherEmployee.VerifyHashedPassword(employee, employee.Password, loginDto.Password);
                if (result == PasswordVerificationResult.Success)
                    return employee;
            }

            var client = await _clientRepository.GetClientByEmail(loginDto.Email);
            if (client != null)
            {
                var result = _passwordHasherClient.VerifyHashedPassword(client, client.Password, loginDto.Password);
                if (result == PasswordVerificationResult.Success)
                    return client;
            }

            throw new UnauthorizedAccessException("Invalid email or password");
        }

    }
}
