using Movie_API.Models.Value_Object;
using Movie_API.Models;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Models.DTOs.Movie;

namespace MovieStore_API.Services.Abstract
{
    public interface IMovieService
    {
        Task<PagingResultModel<Movie>> GetMovies(PagingQuery query);
        Task<List<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(string id);
        Task<Movie> AddMovie(Movie movie);
        void UpdateMovie(string id, MovieUpdateDto updateMovie);
        void DeleteMovie(Movie movie);
    }
}
