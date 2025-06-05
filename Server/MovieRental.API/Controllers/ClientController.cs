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
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> AddClient(CreateClientDto createClientDto)
        {
            try
            {
                await _clientService.AddClient(createClientDto);
                return Ok(createClientDto);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Zwraca wszystkie wypożyczenia danego klienta - Admin, Employee, Client
        /// </summary>
        [HttpGet("{clientId}/rents")]
        [Authorize]
        public async Task<IActionResult> GetClientRents([FromRoute] int clientId)
        {
            try
            {
                var rents = await _clientService.GetClientRents(clientId);
                return Ok(rents);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Zwraca listę wszystkich klientów - Admin, Employee
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllClients();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
