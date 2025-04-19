using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Dtos.Rent
{
    public class ReadRentDto
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public string MovieTitle { get; set; } = default!;
        public string ClientName { get; set; } = default!;
    }
}
