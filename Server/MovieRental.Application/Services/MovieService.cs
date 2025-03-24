using AutoMapper;
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
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
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
        public async Task AddMovie(CreateMovieDto movieDto) // chyba mozna dodac sprawdzanie czy podany aktor jest juz w bazie
        {
            bool movieExist = await _movieRepository.IsMovieWithTitle(movieDto.Title);
            if (movieExist)
                throw new ArgumentException("Movie with the given title is already in the database");

            var movie = _mapper.Map<Movie>(movieDto);
            await _movieRepository.AddMovie(movie);
        }
    }
}
