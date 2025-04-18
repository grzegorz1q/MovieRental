using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Dtos.Client
{
    public class CreateClientDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string Address { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
    }
}
