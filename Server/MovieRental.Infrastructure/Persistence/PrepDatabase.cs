using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Infrastructure.Persistence
{
    public class PrepDatabase
    {
        private readonly AppDbContext _appDbContext;
        public PrepDatabase(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public void Seed()
        {
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Movies.Any())
                {
                    var movies = AddMovies();
                    _appDbContext.Movies.AddRange(movies);
                    _appDbContext.SaveChanges();
                }
            }
            
        }
        private IEnumerable<Movie> AddMovies()
        {
            var movies = new List<Movie>()
            {
                new Movie(){Title = "Skazani na Shawshank", Description="Adaptacja opowiadania Stephena Kinga. Niesłusznie skazany na dożywocie bankier, stara się przetrwać w brutalnym, więziennym świecie.", Director="Frank Darabont", ReleaseDate=DateTime.Parse("1995-04-16"), Count=20, IsAvailable=true},
                new Movie(){Title = "Irlandczyk", Description="Płatny zabójca Frank Sheeran powraca do sekretów, których strzegł jako lojalny członek rodziny przestępczej Bufalino.", Director="Martin Scorsese", ReleaseDate=DateTime.Parse("2019-11-01"), Count=15, IsAvailable=true}
            };
            return movies;
        }
    }
}