using MovieRental.Domain.Entities;

namespace MovieRental.Application.Dtos.Person
{
    public class ReadPersonDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role? Role { get; set; }
        public string? Address { get; set; }
        public int? PhoneNumber { get; set; }
    }
}
