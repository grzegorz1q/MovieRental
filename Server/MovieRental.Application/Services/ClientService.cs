using AutoMapper;
using MovieRental.Application.Dtos.Client;
using MovieRental.Application.Dtos.Rent;
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
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRentRepository _rentRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IRentRepository rentRepository, IEmailService emailService, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _rentRepository = rentRepository;
            _emailService = emailService;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task AddClient(CreateClientDto createClientDto)
        {
            bool clientWithPhoneNumberExist = await _clientRepository.IsClientWithPhoneNumber(createClientDto.PhoneNumber);
            bool clientWithEmailExist = await _clientRepository.IsClientWithEmail(createClientDto.Email);
            bool employeeWithEmailExist = await _employeeRepository.IsEmployeeWithEmail(createClientDto.Email);
            if (clientWithPhoneNumberExist || clientWithEmailExist || employeeWithEmailExist)
                throw new ArgumentException("W bazie istnieje już klient lub pracownik o podanym numerze telefonu lub adresie email!");
            var client = _mapper.Map<Client>(createClientDto);
            var tempPassword = Guid.NewGuid().ToString().Substring(0, 8);
            client.Password = tempPassword;
            await _clientRepository.AddClient(client);
            var emailBody = $"Witaj {client.FirstName}. Jesteś nowym klientem wypożyczalni filmów. Aktywuj swoje konto: http://localhost:5178/account/activate/clients/{client.Id}, Twoje tymczasowe hasło to: {tempPassword}";
            await _emailService.SendEmail(client.Email, "Twoje nowe konto", emailBody);
        }
        public async Task<IEnumerable<ReadRentDto>> GetClientRents(int clientId)
        {
            var client = await _clientRepository.GetClient(clientId);
            if (client == null)
                throw new KeyNotFoundException("Client not found!");
            var clientRents = await _rentRepository.GetClientRents(clientId);
            return _mapper.Map<IEnumerable<ReadRentDto>>(clientRents);
        }
        public async Task<IEnumerable<ReadClientDto>> GetAllClients()
        {
            var clients = await _clientRepository.GetAllClients();
            return _mapper.Map<IEnumerable<ReadClientDto>>(clients);
        }
        public async Task DeleteClient(int clientId)
        {
            await _clientRepository.DeleteClient(clientId);
        }
    }
}
