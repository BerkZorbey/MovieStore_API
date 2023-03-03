using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Movie_API.Models;
using Movie_API.Models.Value_Object;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs.Cast;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Repository.Abstract;
using MovieStore_API.Repository.Concrete;
using MovieStore_API.Services.Abstract;
using System.IO;

namespace MovieStore_API.Services.Concrete
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public CastService(ICastRepository castRepository, IMovieRepository movieRepository, IMapper mapper)
        {
            _castRepository = castRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }


        public async Task<Cast> AddCast(CastDto cast)
        {
            List<StarringMoviesDto> addMovie = new();
            
            var getMovies = await _movieRepository.GetAllMovies();
            foreach (var item in cast.StarringMovies)
            {
                var movie = getMovies.Where(x => x.Name == item.Name).FirstOrDefault();
                if (movie != null)
                {
                    var mapMovie = _mapper.Map<StarringMoviesDto>(movie);
                    var removeMovie = _mapper.Map<StarringMoviesDto>(item);
                    addMovie.Add(mapMovie);
                }
            }
            var mapCast = _mapper.Map<Cast>(cast);
            if (mapCast.StarringMovies == null) mapCast.StarringMovies = new List<StarringMoviesDto>();
            mapCast.StarringMovies = addMovie;

            await _castRepository.AddCast(mapCast);
            return mapCast;
        }

        public void DeleteCast(string id)
        {
            _castRepository.DeleteCast(id);
           
        }

        public async Task<List<Cast>> GetAllCasts()
        {
            var casts = await _castRepository.GetAllCasts();
            return casts;
        }

        public async Task<Cast> GetCastById(string id)
        {
            var cast = await _castRepository.GetCastById(id);
            return cast;
        }

        public PagingResultModel<Cast> GetCasts(PagingQuery query)
        {
            var response = _castRepository.GetCasts(query);
            return response;
        }

        public async void UpdateCast(string id,CastDto updateCast)
        {
            List<StarringMoviesDto> addMovie = new();
            var cast = await _castRepository.GetCastById(id);
            var getMovies = await _movieRepository.GetAllMovies();
            if (cast == null)
            {
                throw new Exception("Data is not found");
            }
            cast.Name = updateCast.Name != default ? updateCast.Name : cast.Name;
            cast.Surname = updateCast.Surname != default ? updateCast.Surname : cast.Surname;
            foreach(var item in updateCast.StarringMovies)
            {
                var movie = getMovies.Where(x => x.Name == item.Name).FirstOrDefault();
                var castMovie = cast.StarringMovies.Where(x=>x.Name == item.Name).FirstOrDefault();
                if (movie != null)
                {
                    if (castMovie == null)
                    {
                        var movieMapper = _mapper.Map<StarringMoviesDto>(movie);
                        addMovie.Add(movieMapper);
                    }

                }
            }
            cast.StarringMovies.AddRange(addMovie);
            _castRepository.UpdateCast(cast);


        }
    }
}
