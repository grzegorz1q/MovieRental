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
            if (person is Client client && client != null)
            {
                var result = _passwordHasherClient.VerifyHashedPassword(client, client.Password, loginDto.Password);
                if (result == PasswordVerificationResult.Success)
                    return client;
            }
            throw new UnauthorizedAccessException("Invalid email or password");
        }
        public async Task ResetPassword(int personId, ResetPasswordDto resetPasswordDto)
        {
            var person = await _personRepository.GetPerson(personId);
            if(person == null)
                throw new KeyNotFoundException("Person not found!");

            if (person is Employee employee)
            {
                var oldPassword = _passwordHasherEmployee.VerifyHashedPassword(employee, employee.Password, resetPasswordDto.OldPassword);
                if (oldPassword == PasswordVerificationResult.Failed)
                    throw new UnauthorizedAccessException("Incorrect old password");
                if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                    throw new ArgumentException("New password and confirmation password do not match!");
                employee.Password = _passwordHasherEmployee.HashPassword(employee, resetPasswordDto.NewPassword);
                await _employeeRepository.UpdateEmployee(employee);
            }
            if(person is Client client) 
            {
                var oldPassword = _passwordHasherClient.VerifyHashedPassword(client, client.Password, resetPasswordDto.OldPassword);
                if (oldPassword == PasswordVerificationResult.Failed)
                    throw new UnauthorizedAccessException("Incorrect old password");
                if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                    throw new ArgumentException("New password and confirmation password do not match!");
                client.Password = _passwordHasherClient.HashPassword(client, resetPasswordDto.NewPassword);
                await _clientRepository.UpdateClient(client);
            }
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
        public async Task UpdateEmail(int personId, UpdateEmailDto emailDto)
        {
            var person = await _personRepository.GetPerson(personId);
            if (person == null)
                throw new KeyNotFoundException("Person not found!");

            if (person is Employee employee)
            {
                employee.Email = emailDto.Email;
                await _employeeRepository.UpdateEmployee(employee);
            }
            if (person is Client client)
            {
                client.Email = emailDto.Email;
                await _clientRepository.UpdateClient(client);
            }
        }
        public async Task<ReadPersonDto> GetLoggedPersonInfo(int personId)
        {
            var person = await _personRepository.GetPerson(personId);
            if (person is Employee employee)
                return _mapper.Map<ReadPersonDto>(employee);
            if (person is Client client)
                return _mapper.Map<ReadPersonDto>(client);

            throw new KeyNotFoundException("Person not found!");
        }
        public async Task Register(CreateClientDto createClientDto)
        {
            bool clientWithPhoneNumberExist = await _clientRepository.IsClientWithPhoneNumber(createClientDto.PhoneNumber);
            bool clientWithEmailExist = await _clientRepository.IsClientWithEmail(createClientDto.Email);
            if (clientWithPhoneNumberExist || clientWithEmailExist)
                throw new ArgumentException("Client with given phone number or email is already in database!");
            if (createClientDto.Password != createClientDto.ConfirmPassword)
                throw new ArgumentException("Password and confirmation password do not match!");
            var client = _mapper.Map<Client>(createClientDto);
            await _clientRepository.AddClient(client);
        }
    }
}
