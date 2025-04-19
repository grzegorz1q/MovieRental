using MovieRental.Application.Dtos.Actor;

namespace MovieRental.Application.Dtos.Movie
{
    public class CreateMovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<CreateActorDto> Actors { get; set; } = default!;
    }
}
