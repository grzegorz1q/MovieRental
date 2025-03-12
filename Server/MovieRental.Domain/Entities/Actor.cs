using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Entities
{
    public class Actor : Person
    {
        public ICollection<Movie> Movies { get; set; } = default!;

    }
}
