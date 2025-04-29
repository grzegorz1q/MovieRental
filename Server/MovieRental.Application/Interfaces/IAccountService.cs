using MovieRental.Application.Dtos.Authentication;
using MovieRental.Application.Dtos.Client;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Dtos.Person;
using MovieRental.Domain.Entities;

namespace MovieRental.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Person> Login(LoginDto loginDto);
        Task Register(CreateClientDto createClientDto);
        Task ResetEmployeePassword(int employeeId, ResetPasswordDto resetPasswordDto);
        Task ResetClientPassword(int clientId, ResetPasswordDto resetPasswordDto);
        Task ForgotPassword(string email);
        Task UpdateEmployeeEmail(int employeeId, UpdateEmailDto emailDto);
        Task UpdateClientEmail(int clientId, UpdateEmailDto emailDto);
        Task<ReadEmployeeDto> GetLoggedEmployeeInfo(int employeeId);
        Task<ReadClientDto> GetLoggedClientInfo(int clientId);
        Task ActivateEmployeeAccount(int employeeId);
        Task ActivateClientAccount(int clientId);
    }
}
