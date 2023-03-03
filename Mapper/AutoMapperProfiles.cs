using AutoMapper;
using Movie_API.Models;
using Movie_API.Models.Value_Object;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Models.DTOs.Cast;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Models.DTOs.Movie;
using MovieStore_API.Models.DTOs.Order;

namespace MovieStore_API.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieUpdateDto>();
            CreateMap<MovieUpdateDto, Movie>();
            CreateMap<MovieDirector, Director>();
            CreateMap<Director, MovieDirector>();
            CreateMap<MovieCast, Cast>();
            CreateMap<Cast, MovieCast>();

            CreateMap<DirectorDto, Director>();
            CreateMap<Director, DirectorDto>();

            CreateMap<DirectorMovieDto, MoviesDirectedByDto>();
            CreateMap<MoviesDirectedByDto, DirectorMovieDto>();
            CreateMap<CastMovieDto, StarringMoviesDto>();
            CreateMap<StarringMoviesDto, CastMovieDto>();

            CreateMap<CastDto, Cast>();
            CreateMap<Cast, CastDto>();

            CreateMap<Movie, MovieDto>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();

            CreateMap<MoviesDirectedByDto, Movie>();
            CreateMap<Movie, MoviesDirectedByDto>();

            CreateMap<StarringMoviesDto, Movie>();
            CreateMap<Movie, StarringMoviesDto>();


            CreateMap<User, UserOrderDto>();
            CreateMap<UserOrderDto, User>();

            CreateMap<Movie, MovieOrderDto>();
            CreateMap<MovieOrderDto, Movie>();
        }
    }
}
