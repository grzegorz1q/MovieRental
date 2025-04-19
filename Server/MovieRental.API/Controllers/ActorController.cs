using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Interfaces;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("actors")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }
        /// <summary>
        /// Zwraca informacje o danym aktorze
        /// </summary>
        [HttpGet("{actorId}")]
        public async Task<IActionResult> GetActor([FromRoute] int actorId)
        {
            try
            {
                var actor = await _actorService.GetActor(actorId);
                return Ok(actor);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Zwraca wszystkich aktorów
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            try
            {
                var allActors = await _actorService.GetAllActors();
                return Ok(allActors);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
