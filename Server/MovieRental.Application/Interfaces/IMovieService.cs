using MovieRental.Application.Dtos.Movie;
using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<ReadMovieDto>> GetAllMovies();
        Task<ReadMovieDto> GetMovie(int movieId);
        Task AddMovie(CreateMovieDto movieDto);
    }
}
