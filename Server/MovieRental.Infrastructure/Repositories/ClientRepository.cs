using Microsoft.AspNetCore.Identity;
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
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<Client> _passwordHasher;
        public ClientRepository(AppDbContext appDbContext, IPasswordHasher<Client> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task AddClient(Client client)
        {
            client.Password = _passwordHasher.HashPassword(client, client.Password);
            await _appDbContext.Clients.AddAsync(client);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteClient(int id)
        {
            var client = await GetClient(id);
            if(client != null) {
                _appDbContext.Clients.Remove(client);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _appDbContext.Clients.ToListAsync();
        }

        public async Task<Client?> GetClient(int id)
        {
            return await _appDbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateClient(Client client)
        {
            _appDbContext.Clients.Update(client);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<bool> IsClientWithPhoneNumber(int phoneNumber)
        {
            return await _appDbContext.Clients.AnyAsync(c => c.PhoneNumber == phoneNumber);
        }
        public async Task<Client?> GetClientByEmail(string email)
        {
            return await _appDbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
