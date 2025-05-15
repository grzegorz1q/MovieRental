using Microsoft.AspNetCore.Identity;
using MovieRental.Domain.Entities;

namespace MovieRental.Infrastructure.Persistence
{
    public class PrepDatabase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<Employee> _passwordHasherEmployee;
        private readonly IPasswordHasher<Client> _passwordHasherClient;
        public PrepDatabase(AppDbContext appDbContext, IPasswordHasher<Employee> passwordHasherEmployee, IPasswordHasher<Client> passwordHasherClient) 
        {
            _appDbContext = appDbContext;
            _passwordHasherEmployee = passwordHasherEmployee;
            _passwordHasherClient = passwordHasherClient;
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
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Clients.Any())
                {
                    var clients = AddClients();
                    _appDbContext.Clients.AddRange(clients);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Employees.Any())
                {
                    var employees = AddEmployees();
                    _appDbContext.Employees.AddRange(employees);
                    _appDbContext.SaveChanges();
                }
            }

        }
        private IEnumerable<Movie> AddMovies()
        {
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", "movies");

            var movies = new List<Movie>()
            {
                new Movie(){Title = "Skazani na Shawshank", Image=$"images\\movies\\skazani_na_shawshank.jpg", Genre="Dramat", Description="Adaptacja opowiadania Stephena Kinga. Niesłusznie skazany na dożywocie bankier, stara się przetrwać w brutalnym, więziennym świecie.", Director="Frank Darabont", ReleaseDate=DateTime.Parse("1995-04-16"), Count=20, IsAvailable=true, Actors = new List<Actor>(){ new Actor() { FirstName = "Tim", LastName = "Robbins" }, new Actor(){FirstName="Morgan", LastName = "Freeman" } } },
                new Movie(){Title = "Irlandczyk", Image=$"images\\movies\\irlandczyk.jpg", Genre="Kryminał", Description="Płatny zabójca Frank Sheeran powraca do sekretów, których strzegł jako lojalny członek rodziny przestępczej Bufalino.", Director="Martin Scorsese", ReleaseDate=DateTime.Parse("2019-11-01"), Count=15, IsAvailable=true, Actors = new List<Actor>(){ new Actor() { FirstName = "Robert", LastName = "De Niro" }, new Actor(){FirstName="Al", LastName = "Pacino" } }}
            };
            return movies;
        }
        private IEnumerable<Client> AddClients()
        {
            var clients = new List<Client>()
            {
                new Client(){FirstName = "Jan", LastName="Kowalski", Email="jan.kowalski@test.pl", Address="Czestochowa", PhoneNumber=987899765, Password="password", IsActive = true},
                new Client(){FirstName = "Adam", LastName="Nowak", Email="adam.nowak@test.pl", Address="Warszawa", PhoneNumber=223454321, Password="password", IsActive = true}
            };
            foreach (var e in clients)
            {
                e.Password = _passwordHasherClient.HashPassword(e, e.Password);
            }
            return clients;
        }
        private IEnumerable<Employee> AddEmployees()
        {
            var employees = new List<Employee>()
            {
                new Employee(){FirstName = "Karol", LastName="Nowakowski", Email="admin@test.pl", Password="admin", IsActive=true, Role=Role.Admin},
                new Employee(){FirstName = "Kamil", LastName="Malinowski", Email="kamil.malinowski@test.pl", Password="password", IsActive=true, Role=Role.Employee}
            };
            foreach (var e in employees)
            {
                e.Password = _passwordHasherEmployee.HashPassword(e, e.Password);
            }
            return employees;
        }
    }
}