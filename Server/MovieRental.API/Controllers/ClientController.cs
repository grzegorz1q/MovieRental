using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Application.Dtos.Client;
using MovieRental.Application.Interfaces;

namespace MovieRental.API.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        /// <summary>
        /// Dodaje klienta - Admin, Employee
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddClient(CreateClientDto createClientDto)
        {
            try
            {
                await _clientService.AddClient(createClientDto);
                return Ok(createClientDto);
            }
            catch(ArgumentException ex) 
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
