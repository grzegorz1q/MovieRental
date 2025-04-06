using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Interfaces
{
    public interface IRentRepository
    {
        Task AddRent(Rent rent);
        Task<IEnumerable<Rent>> GetAllRents();
        Task<Rent?> GetRent(int id);
        Task UpdateRent(Rent rent);
        Task DeleteRent(int id);
        Task<IEnumerable<Rent>> GetClientRents(int clientId);
    }
}
