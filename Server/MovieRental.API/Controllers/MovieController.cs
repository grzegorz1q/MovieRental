using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Movie;
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
        /// <summary>
        /// Dodaje film (Admin, Employee)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> AddMovie(CreateMovieDto movieDto)
        {
            try
            {
                await _movieService.AddMovie(movieDto);
                return Ok(movieDto);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Aktualizuje film o podanym id (Admin, Employee)
        /// </summary>
        [HttpPut("{movieId}")]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int movieId, [FromBody] UpdateMovieDto movieDto)
        {
            try
            {
                await _movieService.UpdateMovie(movieId, movieDto);
                return Ok(movieDto);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{movieId}")]
        //[Authorize(Roles ="Admin, Employee")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int movieId)
        {
            try
            {
                await _movieService.DeleteMovie(movieId);
                return Ok();
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
