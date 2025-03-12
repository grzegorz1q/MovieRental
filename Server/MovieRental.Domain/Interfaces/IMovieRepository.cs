using MovieRental.Domain.Entities;

namespace MovieRental.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task AddMovie(Movie movie);
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovie(int id);
        Task UpdateMovie(Movie movie);
        Task DeleteMovie(int id);
    }
}
