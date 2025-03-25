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
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public EmployeeService(IEmailService emailService, IEmployeeRepository employeeRepository, IPasswordHasher<Employee> passwordHasher, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _mapper = mapper;
        }
        public async Task AddEmployee(CreateEmployeeDto employeeDto)
        {
            if (!Enum.IsDefined(typeof(Role), employeeDto.Role))
                throw new ArgumentException("Invalid role value.");
            var tempPassword = Guid.NewGuid().ToString().Substring(0,8);
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddEmployee(employee);
            var emailBody = $"Witaj {employee.FirstName}, aby aktywować swoje konto naciśnij ten link: http://localhost:5178/employees/activate/{employee.Id}. Twoje tymczasowe hasło to: {tempPassword}";
            await _emailService.SendEmail(employeeDto.Email, "Twoje nowe konto", emailBody);
        }
        public async Task<Employee> Login(LoginDto loginDto)
        {
            var employee = await _employeeRepository.GetEmployeeByEmail(loginDto.Email);
            if (employee == null || employee.IsActive == false)
                throw new UnauthorizedAccessException("Invalid email or password");

            var result = _passwordHasher.VerifyHashedPassword(employee, employee.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid email or password");
            return employee;
        }
        public async Task ActivateAccount(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found!");

            employee.IsActive = true;
            await _employeeRepository.UpdateEmployee(employee);
        }
    }
}
