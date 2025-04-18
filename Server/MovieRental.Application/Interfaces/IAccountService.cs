using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Dtos.Person;
using MovieRental.Domain.Entities;

namespace MovieRental.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Person> Login(LoginDto loginDto);
        Task ResetPassword(int personId, ResetPasswordDto resetPasswordDto);
        Task ForgotPassword(string email);
        Task UpdateEmail(int personId, UpdateEmailDto emailDto);
        Task<ReadPersonDto> GetLoggedPersonInfo(int personId);
    }
}
