using MovieRental.Application.Dtos.Actor;

namespace MovieRental.Application.Dtos.Movie
{
    public class UpdateMovieDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Count { get; set; }
        public bool? IsAvailable { get; set; }
        public ICollection<ActorDto>? Actors { get; set; }
    }
}
