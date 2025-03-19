using AutoMapper;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Dtos.Movie;
using MovieRental.Application.Dtos.Review;
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
            //------MOVIES------
            CreateMap<Movie, ReadMovieDto>();

            //------REVIEWS------
            CreateMap<CreateReviewDto, Review>();
            CreateMap<Review, ReadReviewDto>()
                .ForMember(dest => dest.MovieTitle, x=> x.MapFrom(src => src.Movie.Title));

            //------EMPLOYEES------
            CreateMap<CreateEmployeeDto, Employee>();
        }
    }
}
