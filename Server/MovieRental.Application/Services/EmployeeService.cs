using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;

namespace MovieRental.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IPasswordHasher<Employee> passwordHasher, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public async Task AddEmployee(CreateEmployeeDto employeeDto)
        {
            if (!Enum.IsDefined(typeof(Role), employeeDto.Role))
                throw new ArgumentException("Invalid role value.");

            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddEmployee(employee);
        }
        public async Task<Employee> Login(LoginDto loginDto)
        {
            var employee = await _employeeRepository.GetEmployeeByEmail(loginDto.Email);
            if (employee == null)
                throw new ArgumentNullException("Invalid email or password");

            var result = _passwordHasher.VerifyHashedPassword(employee, employee.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new ArgumentNullException("Invalid email or password");
            return employee;
        }
    }
}
