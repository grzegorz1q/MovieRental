using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Dtos.Rent
{
    public class CreateRentDto
    {
        public int MovieId {  get; set; }
        public int ClientId { get; set; }
    }
}
