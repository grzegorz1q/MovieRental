using MovieRental.Application.Dtos.Actor;

namespace MovieRental.Application.Dtos.Movie
{
    public class UpdateMovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; }
    }
}
