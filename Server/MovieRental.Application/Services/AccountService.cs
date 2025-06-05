using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Dtos.Client;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Dtos.Person;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;

namespace MovieRental.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher<Employee> _passwordHasherEmployee;
        private readonly IPasswordHasher<Client> _passwordHasherClient;
        private readonly IMapper _mapper;
        public AccountService(IEmployeeRepository employeeRepository, IClientRepository clientRepository, IPersonRepository personRepository,IEmailService emailService, IPasswordHasher<Employee> passwordHasherEmployee, IPasswordHasher<Client> passwordHasherClient, IMapper mapper)
        {
            _personRepository = personRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _emailService = emailService;
            _passwordHasherEmployee = passwordHasherEmployee;
            _passwordHasherClient = passwordHasherClient;
            _mapper = mapper;
        }

        public async Task<Person> Login(LoginDto loginDto)
        {
            var person = await _personRepository.GetPersonByEmail(loginDto.Email);
            if (person is Employee employee && employee != null && employee.IsActive)
            {
                var result = _passwordHasherEmployee.VerifyHashedPassword(employee, employee.Password, loginDto.Password);
                if (result == PasswordVerificationResult.Success)
                    return employee;
            }
            if (person is Client client && client != null && client.IsActive)
            {
                var result = _passwordHasherClient.VerifyHashedPassword(client, client.Password, loginDto.Password);
                if (result == PasswordVerificationResult.Success)
                    return client;
            }
            throw new UnauthorizedAccessException("Niepoprawny email lub hasło");
        }
        public async Task ResetEmployeePassword(int employeeId, ResetPasswordDto resetPasswordDto)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if(employee == null)
                throw new KeyNotFoundException("Employee not found!");

            var oldPassword = _passwordHasherEmployee.VerifyHashedPassword(employee, employee.Password, resetPasswordDto.OldPassword);
            if (oldPassword == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Incorrect old password");
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                throw new ArgumentException("New password and confirmation password do not match!");
            employee.Password = _passwordHasherEmployee.HashPassword(employee, resetPasswordDto.NewPassword);
            await _employeeRepository.UpdateEmployee(employee);
        }
        public async Task ResetClientPassword(int clientId, ResetPasswordDto resetPasswordDto)
        {
            var client = await _clientRepository.GetClient(clientId);
            if (client == null)
                throw new KeyNotFoundException("Client not found!");

            var oldPassword = _passwordHasherClient.VerifyHashedPassword(client, client.Password , resetPasswordDto.OldPassword);
            if (oldPassword == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Incorrect old password");
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                throw new ArgumentException("New password and confirmation password do not match!");
            client.Password = _passwordHasherClient.HashPassword(client, resetPasswordDto.NewPassword);
            await _clientRepository.UpdateClient(client);
        }
        public async Task ForgotPassword(string email)
        {
            var person = await _personRepository.GetPersonByEmail(email);
            if (person == null)
                throw new KeyNotFoundException("Person with given email not exist in the database");

            var tempPassword = Guid.NewGuid().ToString().Substring(0, 8);
            if (person is Employee employee)
            {
                employee.Password = _passwordHasherEmployee.HashPassword(employee, tempPassword);
                await _employeeRepository.UpdateEmployee(employee);
            }
            if(person is Client client)
            {
                client.Password = _passwordHasherClient.HashPassword(client, tempPassword);
                await _clientRepository.UpdateClient(client);
            }
            var emailBody = $"Witaj {person.FirstName}, operacja resetowania hasła przebiegła pomyślnie. Twoje tymczasowe hasło to: {tempPassword}";
            await _emailService.SendEmail(email, "Resetowanie hasła", emailBody);
        }
        public async Task UpdateEmployeeEmail(int employeeId, UpdateEmailDto emailDto)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found!");

            employee.Email = emailDto.Email;
            await _employeeRepository.UpdateEmployee(employee);
        }
        public async Task UpdateClientEmail(int clientId, UpdateEmailDto emailDto)
        {
            var client = await _clientRepository.GetClient(clientId);
            if (client == null)
                throw new KeyNotFoundException("Client not found!");

            client.Email = emailDto.Email;
            await _clientRepository.UpdateClient(client);
        }
        public async Task<ReadEmployeeDto> GetLoggedEmployeeInfo(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found!");
            return _mapper.Map<ReadEmployeeDto>(employee);
        }
        public async Task<ReadClientDto> GetLoggedClientInfo(int clientId)
        {
            var client = await _clientRepository.GetClient(clientId);
            if (client == null)
                throw new KeyNotFoundException("Client not found!");
            
            return _mapper.Map<ReadClientDto>(client);
        }
        public async Task Register(CreateClientDto createClientDto)
        {
            bool clientWithPhoneNumberExist = await _clientRepository.IsClientWithPhoneNumber(createClientDto.PhoneNumber);
            bool clientWithEmailExist = await _clientRepository.IsClientWithEmail(createClientDto.Email);
            if (clientWithPhoneNumberExist || clientWithEmailExist)
                throw new ArgumentException("Klient lub pracownik o podanym numerze telefonu lub adresie e-mail jest już w bazie danych!");
            if (createClientDto.Password != createClientDto.ConfirmPassword)
                throw new ArgumentException("Hasło i hasło potwierdzające nie są takie same!");
            var client = _mapper.Map<Client>(createClientDto);
            await _clientRepository.AddClient(client);
            var emailBody = $"Witaj {client.FirstName}, aby aktywować swoje konto naciśnij ten link: http://localhost:5178/account/activate/clients/{client.Id}.";
            await _emailService.SendEmail(client.Email, "Twoje nowe konto", emailBody);
        }
        public async Task ActivateEmployeeAccount(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found!");

            employee.IsActive = true;
            await _employeeRepository.UpdateEmployee(employee);
        }
        public async Task ActivateClientAccount(int clientId)
        {
            var client = await _clientRepository.GetClient(clientId);
            if (client == null)
                throw new KeyNotFoundException("Client not found!");

            client.IsActive = true;
            await _clientRepository.UpdateClient(client);
        }
    }
}
