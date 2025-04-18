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
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task AddClient(CreateClientDto createClientDto)
        {
            bool clientExist = await _clientRepository.IsClientWithPhoneNumber(createClientDto.PhoneNumber);
            if (clientExist)
                throw new ArgumentException("Client with given phone number is already in database!");
            var client = _mapper.Map<Client>(createClientDto);
            await _clientRepository.AddClient(client);
        }
    }
}
