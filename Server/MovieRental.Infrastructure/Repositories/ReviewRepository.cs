using Microsoft.EntityFrameworkCore;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using MovieRental.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReviewRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddReview(Review review)
        {
            await _appDbContext.Reviews.AddAsync(review);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteReview(int id)
        {
            var review = await GetReview(id);
            if(review != null)
            {
                _appDbContext.Reviews.Remove(review);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _appDbContext.Reviews.ToListAsync();
        }

        public async Task<Review?> GetReview(int id)
        {
            return await _appDbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateReview(Review review)
        {
            _appDbContext.Reviews.Update(review);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
