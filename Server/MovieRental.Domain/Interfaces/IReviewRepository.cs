using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task AddReview(Review review);
        Task<IEnumerable<Review>> GetAllReviews();
        Task<Review?> GetReview(int id);
        Task UpdateReview(Review review);
        Task DeleteReview(int id);
    }
}
