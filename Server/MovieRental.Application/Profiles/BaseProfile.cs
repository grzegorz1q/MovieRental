using AutoMapper;
using MovieRental.Application.Dtos.Actor;
using MovieRental.Application.Dtos.Client;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Dtos.Movie;
using MovieRental.Application.Dtos.Person;
using MovieRental.Application.Dtos.Rent;
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
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
                /*.ForMember(dest => dest.Title, opts => opts.MapFrom((src, dest) => src.Title ?? dest.Title))
                .ForMember(dest => dest.Description, opts => opts.MapFrom((src, dest) => src.Description ?? dest.Description))
                .ForMember(dest => dest.Director, opts => opts.MapFrom((src, dest) => src.Director ?? dest.Director))
                .ForMember(dest => dest.ReleaseDate, opts => opts.MapFrom((src, dest) => src.ReleaseDate ?? dest.ReleaseDate))
                .ForMember(dest => dest.Count, opts => opts.MapFrom((src, dest) => src.Count ?? dest.Count))
                .ForMember(dest => dest.IsAvailable, opts => opts.MapFrom((src, dest) => src.IsAvailable ?? dest.IsAvailable));*/

            //------REVIEWS------
            CreateMap<CreateReviewDto, Review>();
            CreateMap<Review, ReadReviewDto>()
                .ForMember(dest => dest.MovieTitle, x=> x.MapFrom(src => src.Movie.Title));

            //------EMPLOYEES------
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<Employee, ReadEmployeeDto>();

            //------ACTORS------
            CreateMap<ActorDto, Actor>();
            CreateMap<Actor, ActorDto>();

            //------RENTS------
            CreateMap<CreateRentDto, Rent>();
            CreateMap<Rent, ReadRentDto>()
                .ForMember(dest => dest.MovieTitle, x => x.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.ClientName, x => x.MapFrom(src => $"{src.Client.FirstName} {src.Client.LastName}"));

            //------CLIENTS------
            CreateMap<CreateClientDto, Client>();

            //------PERSONS------
            CreateMap<Client, ReadPersonDto>();
            CreateMap<Employee, ReadPersonDto>();
        }
    }
}
