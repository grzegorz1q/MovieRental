using Microsoft.EntityFrameworkCore;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using MovieRental.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Infrastructure.Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly AppDbContext _appDbContext;
        public RentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddRent(Rent rent)
        {
            await _appDbContext.Rents.AddAsync(rent);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteRent(int id)
        {
            var rent = await GetRent(id);
            if(rent == null)
            {
                throw new KeyNotFoundException("Rent not found!");
            }
            _appDbContext.Rents.Remove(rent);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rent>> GetAllRents()
        {
            return await _appDbContext.Rents.ToListAsync();
        }

        public async Task<Rent?> GetRent(int id)
        {
            return await _appDbContext.Rents.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateRent(Rent rent)
        {
            _appDbContext.Rents.Update(rent);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Rent>> GetClientRents(int clientId)
        {
            return await _appDbContext.Rents.Where(r => r.ClientId == clientId).ToListAsync();
        }
    }
}
