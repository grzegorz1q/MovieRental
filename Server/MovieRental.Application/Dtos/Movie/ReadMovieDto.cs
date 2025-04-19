using MovieRental.Application.Dtos.Actor;
using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Dtos.Movie
{
    public class ReadMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<ReadActorDto> Actors { get; set; } = default!;
        /*public ICollection<Rent> Rents { get; set; } = default!;
        public ICollection<Review> Reviews { get; set; } = default!;*/
    }
}
