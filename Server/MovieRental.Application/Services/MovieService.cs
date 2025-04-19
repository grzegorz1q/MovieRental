using AutoMapper;
using MovieRental.Application.Dtos.Actor;
using MovieRental.Application.Dtos.Movie;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository,IActorRepository actorRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadMovieDto>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMovies();
            return _mapper.Map<IEnumerable<ReadMovieDto>>(movies);
        }
        public async Task<ReadMovieDto> GetMovie(int movieId)
        {
            var movie = await _movieRepository.GetMovie(movieId);
            if (movie == null)
                throw new ArgumentNullException("Movie not found!");
            return _mapper.Map<ReadMovieDto>(movie);
        }
        public async Task AddMovie(CreateMovieDto movieDto)
        {
            bool movieExist = await _movieRepository.IsMovieWithTitle(movieDto.Title);
            if (movieExist)
                throw new ArgumentException("Movie with the given title is already in the database!");
            
            var movie = _mapper.Map<Movie>(movieDto);
            movie.Actors = new List<Actor>();
            foreach(var actorDto in movieDto.Actors)
            {
                var existingActor = await _actorRepository.GetActorByName(actorDto.FirstName, actorDto.LastName);
                if (existingActor != null)
                    movie.Actors.Add(existingActor);
                else
                {
                    var newActor = _mapper.Map<Actor>(actorDto);
                    movie.Actors.Add(newActor);
                }
            }
            await _movieRepository.AddMovie(movie);
        }
        public async Task UpdateMovie(int movieId, UpdateMovieDto movieDto)
        {
            var movie = await _movieRepository.GetMovie(movieId);
            if (movie == null)
                throw new KeyNotFoundException("Movie not found!");

            _mapper.Map(movieDto, movie);
            await _movieRepository.UpdateMovie(movie);
        }
        public async Task DeleteMovie(int movieId)
        {
            await _movieRepository.DeleteMovie(movieId);
        }
        public async Task<IEnumerable<ReadActorDto>> GetMovieActors(int movieId)
        {
            var movie = await _movieRepository.GetMovie(movieId);
            if (movie == null)
                throw new KeyNotFoundException("Movie not found!");
            var movieActors = movie.Actors.ToList();
            return _mapper.Map<IEnumerable<ReadActorDto>>(movieActors);
        }
    }
}
