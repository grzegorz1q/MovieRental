using MovieRental.Application.Dtos.Client;
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
        //Task Register()
    }
}
