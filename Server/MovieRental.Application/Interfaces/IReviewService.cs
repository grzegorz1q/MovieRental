using MovieRental.Application.Dtos.Review;

namespace MovieRental.Application.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(int movieId, CreateReviewDto reviewDto);
        Task<IEnumerable<ReadReviewDto>> GetAllMovieReviews(int movieId);
    }
}
