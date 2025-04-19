using MovieRental.Application.Dtos.Movie;

namespace MovieRental.Application.Dtos.Actor
{
    public class ReadActorWithMoviesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<ReadMovieDto> Movies { get; set; } = default!;
    }
}
