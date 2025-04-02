using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Entities
{
    public class Rent
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
        public Movie Movie { get; set; } = default!;
        public int MovieId { get; set; }
        public Client Client { get; set; } = default!;
        public int ClientId { get; set; }
    }
}
