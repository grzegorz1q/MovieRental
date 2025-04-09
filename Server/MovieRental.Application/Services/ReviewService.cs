using AutoMapper;
using MovieRental.Application.Dtos.Review;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository reviewRepository, IMovieRepository movieRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task AddReview(int movieId, CreateReviewDto reviewDto)
        {
            var movie = await _movieRepository.GetMovie(movieId);
            if (movie == null)
                throw new ArgumentNullException("Movie not found");

            var review = _mapper.Map<Review>(reviewDto);
            review.MovieId = movieId;
            await _reviewRepository.AddReview(review);
        }
        public async Task<IEnumerable<ReadReviewDto>> GetAllMovieReviews(int movieId)
        {
            var movie = await _movieRepository.GetMovie(movieId);
            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            var reviews = await _reviewRepository.GetAllMovieReviews(movieId);
            return _mapper.Map<IEnumerable<ReadReviewDto>>(reviews);
        }
    }
}
