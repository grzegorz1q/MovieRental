using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person?> GetPerson(int id);
        Task<Person?> GetPersonByEmail(string email);
    }
}
