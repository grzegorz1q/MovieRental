using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Dtos.Person;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using System.Security.Claims;

namespace MovieRental.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public EmployeeService(IEmailService emailService, IEmployeeRepository employeeRepository, IClientRepository clientRepository, IPasswordHasher<Employee> passwordHasher, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _mapper = mapper;
        }
        public async Task AddEmployee(CreateEmployeeDto employeeDto)
        {
            if (!Enum.IsDefined(typeof(Role), employeeDto.Role))
                throw new ArgumentException("Nieprawidłowa rola!");
            bool isEmployee = await _employeeRepository.IsEmployeeWithEmail(employeeDto.Email);
            bool IsClientWithEmail = await _clientRepository.IsClientWithEmail(employeeDto.Email);
            if (isEmployee || IsClientWithEmail)
                throw new ArgumentException("W bazie istnieje już pracownik lub klient o podanym adresie email!");
            
            var tempPassword = Guid.NewGuid().ToString().Substring(0,8);
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.Password = tempPassword;
            await _employeeRepository.AddEmployee(employee);
            var emailBody = $"Witaj {employee.FirstName}, aby aktywować swoje konto naciśnij ten link: http://localhost:5178/account/activate/employees/{employee.Id}. Twoje tymczasowe hasło to: {tempPassword}";
            await _emailService.SendEmail(employeeDto.Email, "Twoje nowe konto", emailBody);
        }
        public async Task<IEnumerable<ReadEmployeeDto>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return _mapper.Map<IEnumerable<ReadEmployeeDto>>(employees);
        }
        public async Task<ReadEmployeeDto> GetEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found!");
            return _mapper.Map<ReadEmployeeDto>(employee);
        }
        public async Task ChangeRole(int employeeId, Role role)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found!;");
            employee.Role = role;
            await _employeeRepository.UpdateEmployee(employee);
        }
        public async Task DeactivateEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");
            employee.IsActive = false;
            await _employeeRepository.UpdateEmployee(employee);
        }
        public async Task DeleteEmployee(int employeeId)
        {
            await _employeeRepository.DeleteEmployee(employeeId);
        }
    }
}
