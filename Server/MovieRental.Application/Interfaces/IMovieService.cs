using MovieRental.Application.Dtos.Movie;
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
    }
}
