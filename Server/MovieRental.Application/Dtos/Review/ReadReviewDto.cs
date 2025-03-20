using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Dtos.Review
{
    public class ReadReviewDto
    {
        public string Comment { get; set; } = string.Empty;
        public float Rating { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
    }
}
