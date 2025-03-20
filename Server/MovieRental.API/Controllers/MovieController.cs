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
        /// <summary>
        /// Zwraca listę wszystkich filmów
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _movieService.GetAllMovies();
                return Ok(movies);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        /// <summary>
        /// Zwraca film o podanym id
        /// </summary>
        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovie([FromRoute] int movieId)
        {
            try
            {
                var movie = await _movieService.GetMovie(movieId);
                return Ok(movie);
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
