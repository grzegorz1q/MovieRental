using AutoMapper;
using MovieRental.Application.Dtos.Client;
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
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IEmailService emailService, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _emailService = emailService;
            _mapper = mapper;
        }
        public async Task AddClient(CreateClientDto createClientDto)
        {
            bool clientWithPhoneNumberExist = await _clientRepository.IsClientWithPhoneNumber(createClientDto.PhoneNumber);
            bool clientWithEmailExist = await _clientRepository.IsClientWithEmail(createClientDto.Email);
            if (clientWithPhoneNumberExist || clientWithEmailExist)
                throw new ArgumentException("Client with given phone number or email is already in database!");
            var client = _mapper.Map<Client>(createClientDto);
            var tempPassword = Guid.NewGuid().ToString().Substring(0, 8);
            client.Password = tempPassword;
            await _clientRepository.AddClient(client);
            var emailBody = $"Witaj {client.FirstName}. Jesteś nowym klientem wypożyczalni filmów. Twoje tymczasowe hasło to: {tempPassword}";
            await _emailService.SendEmail(client.Email, "Twoje nowe konto", emailBody);
        }
    }
}
