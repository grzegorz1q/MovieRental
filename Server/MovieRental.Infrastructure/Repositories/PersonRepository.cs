using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using MovieRental.Infrastructure.Persistence;

namespace MovieRental.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _appDbContext;
        public PersonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Person?> GetPerson(int id)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
                return employee;

            return await _appDbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Person?> GetPersonByEmail(string email)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
            if (employee != null) 
                return employee;

            return await _appDbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
