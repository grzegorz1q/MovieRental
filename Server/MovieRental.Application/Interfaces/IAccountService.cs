using MovieRental.Application.Dtos.Authentication;
using MovieRental.Domain.Entities;

namespace MovieRental.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Person> Login(LoginDto loginDto);
    }
}
