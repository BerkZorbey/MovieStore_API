using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Movie_API.Models;
using Movie_API.Models.Value_Object;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Models.DTOs.Cast;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Models.DTOs.Movie;
using MovieStore_API.Repository.Abstract;
using MovieStore_API.Services.Abstract;
using System.IO;

namespace MovieStore_API.Services.Concrete
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IDirectorRepository _directorRepository;
        private readonly ICastRepository _castRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IDirectorRepository directorRepository, ICastRepository castRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _directorRepository = directorRepository;
            _castRepository = castRepository;
            _mapper = mapper;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            foreach (var item in movie.Cast)
            {
                var movieCastMapper = _mapper.Map<StarringMoviesDto>(movie);
                var cast = _castRepository.GetAllCasts().Result.Where(x => x.FullName == item.FullName).FirstOrDefault();
                if (cast != null)
                {
                    if (cast.StarringMovies == null) cast.StarringMovies = new List<StarringMoviesDto>() { movieCastMapper };
                    else
                    {
                        cast.StarringMovies.Add(movieCastMapper);

                    }
                    _castRepository.UpdateCast(cast);
                }
                else
                {
                    item.StarringMovies = new List<StarringMoviesDto>() { movieCastMapper };
                    await _castRepository.AddCast(item);
                }
            }
            foreach (var item in movie.Director)
            {
                var movieDirectorMapper = _mapper.Map<MoviesDirectedByDto>(movie);
                var director = _directorRepository.GetAllDirectors().Result.Where(x => x.FullName == item.FullName).FirstOrDefault();
                if (director != null)
                {
                    if (director.MoviesDirectedBy == null) director.MoviesDirectedBy = new List<MoviesDirectedByDto>() { movieDirectorMapper };
                    else
                    {
                        director.MoviesDirectedBy.Add(movieDirectorMapper);

                    }
                    _directorRepository.UpdateDirector(director);
                }
                else
                {
                    item.MoviesDirectedBy = new List<MoviesDirectedByDto>() { movieDirectorMapper };
                    await _directorRepository.AddDirector(item);
                }
            }
            await _movieRepository.AddMovie(movie);
            return movie;
        }

        public void DeleteMovie(Movie movie)
        {
            _movieRepository.DeleteMovie(movie);
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMovies();
            return movies;
        }

        public async Task<Movie> GetMovieById(string id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            return movie;
        }

        public async Task<PagingResultModel<Movie>> GetMovies(PagingQuery query)
        {
            var response = await _movieRepository.GetMovies(query);
            return response;
        }

        public async void UpdateMovie(string id, MovieUpdateDto updateMovie)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
            {

            }
            if (movie.Genre == null) movie.Genre = new List<Genre>();
            var movieMapper = _mapper.Map<Movie>(updateMovie);
            movie.Genre = movieMapper.Genre != default ? movieMapper.Genre : movie.Genre;
            movie.Release_Year = movieMapper.Release_Year != default ? movieMapper.Release_Year : movie.Release_Year;
            movie.Price = movieMapper.Price != default ? movieMapper.Price : movie.Price;
            foreach (var item in movie.Director)
            {
                var movieDirectorMapper = _mapper.Map<MoviesDirectedByDto>(movie);
                var director = _directorRepository.GetAllDirectors().Result.Where(x => x.FullName == item.FullName).FirstOrDefault();
                var directorHasMovie = director.MoviesDirectedBy.Where(x => x.Name == movieDirectorMapper.Name).FirstOrDefault();
                if (director != null && directorHasMovie == null)
                {
                    if (director.MoviesDirectedBy == null) director.MoviesDirectedBy = new List<MoviesDirectedByDto>() { movieDirectorMapper };
                    else
                    {
                        director.MoviesDirectedBy.Add(movieDirectorMapper);

                    }
                    _directorRepository.UpdateDirector(director);
                }
                else if (director != null && directorHasMovie != null)
                {
                    director.MoviesDirectedBy.Remove(directorHasMovie);
                    director.MoviesDirectedBy.Add(movieDirectorMapper);
                    _directorRepository.UpdateDirector(director);
                }
                else
                {
                    item.MoviesDirectedBy = new List<MoviesDirectedByDto>() { movieDirectorMapper };
                    await _directorRepository.AddDirector(item);
                }
            }
            foreach (var item in movie.Cast)
            {
                var movieCastMapper = _mapper.Map<StarringMoviesDto>(movie);
                var cast = _castRepository.GetAllCasts().Result.Where(x => x.FullName == item.FullName).FirstOrDefault();
                var castHasMovie = cast.StarringMovies.Where(x => x.Name == movieCastMapper.Name).FirstOrDefault();
                if (cast != null && castHasMovie == null)
                {
                    if (cast.StarringMovies == null) cast.StarringMovies = new List<StarringMoviesDto>() { movieCastMapper };
                    else
                    {
                        cast.StarringMovies.Add(movieCastMapper);

                    }
                    _castRepository.UpdateCast(cast);
                }
                else if (cast != null && castHasMovie != null)
                {
                    cast.StarringMovies.Remove(castHasMovie);
                    cast.StarringMovies.Add(movieCastMapper);
                    _castRepository.UpdateCast(cast);
                }
                else
                {
                    item.StarringMovies = new List<StarringMoviesDto>() { movieCastMapper };
                    await _castRepository.AddCast(item);
                }
            }
            _movieRepository.UpdateMovie(id, movie);
        }
    }
}
