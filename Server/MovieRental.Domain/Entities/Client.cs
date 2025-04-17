namespace MovieRental.Domain.Entities
{
    public class Client : Person
    {
        public string Address { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Rent> Rents { get; set; } = default!;
    }
}
