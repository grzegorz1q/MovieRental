using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Entities
{
    public class Client : Person
    {
        public string Address { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public ICollection<Rent> Rents { get; set; } = default!;
    }
}
