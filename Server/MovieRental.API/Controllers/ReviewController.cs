using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Review;
using MovieRental.Application.Interfaces;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("movies/{movieId}/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost]
        public async Task<IActionResult> AddReview([FromRoute] int movieId, [FromBody] CreateReviewDto reviewDto)
        {
            try 
            {
                await _reviewService.AddReview(movieId, reviewDto);
                return Ok(reviewDto);
            }
            catch(ArgumentNullException ex) 
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMovieReviews([FromRoute] int movieId)
        {
            try
            {
                var reviews = await _reviewService.GetAllMovieReviews(movieId);
                return Ok(reviews);
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
