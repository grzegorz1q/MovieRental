using MovieRental.Application.Dtos.Client;
using MovieRental.Application.Dtos.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Interfaces
{
    public interface IClientService
    {
        Task AddClient(CreateClientDto createClientDto);
        Task<IEnumerable<ReadRentDto>> GetClientRents(int clientId);
    }
}
