using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Rent;
using MovieRental.Application.Interfaces;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("rents")]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;
        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        /// <summary>
        /// Dodaje wypożyczenie
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddRent(CreateRentDto createRentDto)
        {
            try
            {
                await _rentService.AddRent(createRentDto);
                return Ok("Rent successfully added!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
