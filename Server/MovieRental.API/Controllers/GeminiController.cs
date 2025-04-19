using Microsoft.AspNetCore.Mvc;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using System.Text.Json;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("gemini")]
    public class GeminiController : ControllerBase
    {
        private readonly IGeminiService _geminiService;
        private static IEnumerable<GeminiMovie> _cachedMovies = new List<GeminiMovie>();
        public GeminiController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }
        [HttpGet("popular-movies")]
        public async Task<IActionResult> GetPopularMovies()
        {
            try
            {
                var prompt = "Zwróć listę 10 najczęściej oglądanych filmów na świecie w formacie JSON. Użyj właściwości: title, year, rating.";
                _cachedMovies = await _geminiService.GetMoviesFromGemini(prompt);
                return Ok(_cachedMovies);
            }
            catch
            {
                return BadRequest("Error while getting a movies");
            }
        }
        [HttpGet("popular-movies/{id}")]
        public IActionResult GetPopularMovieById([FromRoute] int id)
        {
            try
            {
                return Ok(_cachedMovies.ElementAt(id));
            }
            catch
            {
                return BadRequest("Error while getting a movie");
            }
        }
    }
}
