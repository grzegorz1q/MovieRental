using MovieRental.Application.Dtos.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Interfaces
{
    public interface IActorService
    {
        Task<ReadActorWithMoviesDto> GetActor(int actorId);
        Task<IEnumerable<ReadActorDto>> GetAllActors();
    }
}
