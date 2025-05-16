namespace MovieRental.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Image {get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<Actor> Actors { get; set; } = default!;
        public ICollection<Rent> Rents { get; set; } = default!;
        public ICollection<Review> Reviews { get; set; } = default!;
    }
}
