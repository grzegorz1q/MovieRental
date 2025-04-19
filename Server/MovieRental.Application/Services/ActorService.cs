using AutoMapper;
using MovieRental.Application.Dtos.Actor;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;
        public ActorService(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }
        public async Task<ReadActorWithMoviesDto> GetActor(int actorId)
        {
            var actor = await _actorRepository.GetActor(actorId);
            if (actor == null)
                throw new KeyNotFoundException("Actor not found!");
            return _mapper.Map<ReadActorWithMoviesDto>(actor);
        }
        public async Task<IEnumerable<ReadActorDto>> GetAllActors()
        {
            var allActors = await _actorRepository.GetAllActors();
            return _mapper.Map<IEnumerable<ReadActorDto>>(allActors);
        }
    }
}
