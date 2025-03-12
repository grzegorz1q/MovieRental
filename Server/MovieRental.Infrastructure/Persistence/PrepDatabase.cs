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
/*public int Id { get; set; }
public string Title { get; set; } = string.Empty;
public string Description { get; set; } = string.Empty;
public string Director { get; set; } = string.Empty;
public DateTime ReleaseDate { get; set; }
public int Count { get; set; }
public bool IsAvailable { get; set; } = true;
public ICollection<Actor> Actors { get; set; } = default!;
public ICollection<Rent> Rents { get; set; } = default!;
public ICollection<Review> Reviews { get; set; } = default!;*/