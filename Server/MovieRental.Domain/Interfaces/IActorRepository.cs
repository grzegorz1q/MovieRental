using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Interfaces
{
    public interface IActorRepository
    {
        Task AddActor(Actor actor);
        Task<IEnumerable<Actor>> GetAllActors();
        Task<Actor?> GetActor(int id);
        Task<Actor?> GetActorByName(string firstName, string lastName);
        Task UpdateActor(Actor actor);
        Task DeleteActor(int id);
    }
}
