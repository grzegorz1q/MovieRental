using AutoMapper;
using MovieRental.Application.Dtos.Rent;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Services
{
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public RentService(IRentRepository rentRepository, IMovieRepository movieRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _rentRepository = rentRepository;
            _movieRepository = movieRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task AddRent(CreateRentDto createRentDto)
        {
            var movie = await _movieRepository.GetMovie(createRentDto.MovieId);
            if (movie == null)
                throw new KeyNotFoundException("Movie not found!");

            var client = await _clientRepository.GetClient(createRentDto.ClientId);
            if (client == null)
                throw new KeyNotFoundException("Client not found!");

            var rent = _mapper.Map<Rent>(createRentDto);

            //Sprawdzanie czy ma już wypożyczony ten film
            var clientRents = await _rentRepository.GetClientRents(client.Id);
            if(clientRents.Any(r => r.MovieId == movie.Id && (r.ReturnDate == null || r.ReturnDate > DateTime.Now)))
                throw new ArgumentException("Selected client currently has this movie on rent!");

            await _rentRepository.AddRent(rent);

            movie.Count--;
            await _movieRepository.UpdateMovie(movie);
        }
        public async Task ReturnMovie(int rentId)
        {
            await _rentRepository.DeleteRent(rentId);
        }
    }
}
