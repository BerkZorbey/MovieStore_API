using Movie_API.Models.Value_Object;
using Movie_API.Models;
using MovieStore_API.Models.DTOs.Movie;

namespace MovieStore_API.Repository.Abstract
{
    public interface IMovieRepository
    {
        Task<PagingResultModel<Movie>> GetMovies(PagingQuery query);
        Task<List<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(string id);
        Task<Movie> AddMovie(Movie movie);
        void UpdateMovie(string id, Movie updateMovie);
        void DeleteMovie(Movie movie);
    }
}
