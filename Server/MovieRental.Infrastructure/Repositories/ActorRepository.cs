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
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _appDbContext;
        public ActorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddActor(Actor actor)
        {
            await _appDbContext.Actors.AddAsync(actor);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteActor(int id)
        {
            var actor = await GetActor(id);
            if (actor != null)
            {
                _appDbContext.Actors.Remove(actor);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Actor?> GetActor(int id)
        {
            return await _appDbContext.Actors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Actor>> GetAllActors()
        {
            return await _appDbContext.Actors.ToListAsync();
        }

        public async Task UpdateActor(Actor actor)
        {
            _appDbContext.Actors.Update(actor);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
