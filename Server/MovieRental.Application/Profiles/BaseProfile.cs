using AutoMapper;
using MovieRental.Application.Dtos.Movie;
using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Profiles
{
    public class BaseProfile : Profile
    {
        public BaseProfile() 
        {
            CreateMap<Movie, ReadMovieDto>();
        }
    }
}
