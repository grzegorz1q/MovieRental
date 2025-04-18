namespace MovieRental.Domain.Entities
{
    public class Employee : Person
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
