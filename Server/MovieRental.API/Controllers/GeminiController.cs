using Microsoft.AspNetCore.Mvc;
using MovieRental.Domain.Interfaces;
using System.Text.Json;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("gemini")]
    public class GeminiController : ControllerBase
    {
        private readonly IGeminiService _geminiService;
        public GeminiController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }
        [HttpGet("popular-movies")]
        public async Task<IActionResult> GetPopularMovies()
        {
            var prompt = "Zwróć listę 5 najczęściej oglądanych filmów na świecie w formacie JSON. Użyj właściwości: title, year, rating.";
            //var prompt = "Co słychać?";
            var result = await _geminiService.AskGemini(prompt);

            // opcjonalnie: spróbuj zdeserializować
            try
            {
                var json = JsonSerializer.Deserialize<object>(result);
                return Ok(json);
            }
            catch
            {
                //return Ok(new { raw = result }); // fallback jeśli nie da się sparsować
                return BadRequest("Błąd");
            }
        }
    }
}
