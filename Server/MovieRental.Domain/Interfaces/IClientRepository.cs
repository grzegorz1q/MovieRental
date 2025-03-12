using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task AddClient(Client client);
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client?> GetClient(int id);
        Task UpdateClient(Client client);
        Task DeleteClient(int id);
    }
}
