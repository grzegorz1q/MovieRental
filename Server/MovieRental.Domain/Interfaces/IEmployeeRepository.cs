using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployee(int id);
        //Task<Employee?> GetEmployeeByEmail(string email);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int id);
    }
}
