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
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _appDbContext;
        public MovieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddMovie(Movie movie)
        {
            await _appDbContext.Movies.AddAsync(movie);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteMovie(int id)
        {
            var movie = await GetMovie(id);
            if (movie != null)
            {
                _appDbContext.Movies.Remove(movie);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _appDbContext.Movies.ToListAsync();
        }

        public async Task<Movie?> GetMovie(int id)
        {
            return await _appDbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateMovie(Movie movie)
        {
            _appDbContext.Movies.Update(movie);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<bool> IsMovieWithTitle(string title)
        {
            return await _appDbContext.Movies.AnyAsync(m => m.Title == title);
        }
    }
}
