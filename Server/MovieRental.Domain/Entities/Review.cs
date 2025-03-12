using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public float Rating { get; set; }
        public Movie Movie { get; set; } = default!;
        public int MovieId { get; set; }
    }
}
