using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Interfaces;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _movieService.GetAllMovies();
                return Ok(movies);
            }catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
