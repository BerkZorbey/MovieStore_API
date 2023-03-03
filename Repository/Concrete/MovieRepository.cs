using AutoMapper;
using MongoDB.Driver;
using Movie_API.Models.Value_Object;
using Movie_API.Models;
using MovieStore_API.Models.DTOs.Cast;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Models.DTOs.Movie;
using MovieStore_API.Models;
using MovieStore_API.Repository.Abstract;
using MovieStore_API.Services.Abstract;

namespace MovieStore_API.Repository.Concrete
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _movies;
        private readonly IMapper _mapper;
        public MovieRepository(IConfiguration configuration, IMapper mapper)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieStoreDb");
            _movies = database.GetCollection<Movie>("Movies");
            _mapper = mapper;
        }

        public async Task<PagingResultModel<Movie>> GetMovies(PagingQuery query)
        {
            PagingResultModel<Movie> movies = new PagingResultModel<Movie>(query);
            var getAllMovies = await _movies.FindAsync(x => x.isDeleted == false).Result.ToListAsync();
            movies.GetData(getAllMovies.AsQueryable());
            return movies;
        }
        public async Task<List<Movie>> GetAllMovies()
        {
            var movies = await _movies.FindAsync(x => x.Id != null).Result.ToListAsync();

            return movies;
        }
        public async Task<Movie> GetMovieById(string id)
        {
            return await _movies.FindAsync(x => x.Id == id && x.isDeleted == false).Result.FirstOrDefaultAsync();
        }
        public async Task<Movie> AddMovie(Movie movie)
        {
            await _movies.InsertOneAsync(movie);
            return movie;
        }

        public async void UpdateMovie(string id, Movie updateMovie)
        {
            await _movies.ReplaceOneAsync(x => x.Id == id, updateMovie);
        }
        public void DeleteMovie(Movie movie)
        {
            _movies.GetType().GetProperty("isDeleted").SetValue(movie, true);
        }
    }
}
