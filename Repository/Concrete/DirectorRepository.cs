using AutoMapper;
using MongoDB.Driver;
using Movie_API.Models.Value_Object;
using Movie_API.Models;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Models;
using MovieStore_API.Repository.Abstract;
using MovieStore_API.Services.Abstract;

namespace MovieStore_API.Repository.Concrete
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly IMongoCollection<Director> _directors;
        private readonly IMapper _mapper;
        public DirectorRepository(IConfiguration configuration, IMapper mapper)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieStoreDb");
            _directors = database.GetCollection<Director>("Directors");
            _mapper = mapper;
        }
        public PagingResultModel<Director> GetDirectors(PagingQuery query)
        {
            PagingResultModel<Director> directors = new PagingResultModel<Director>(query);
            directors.GetData(_directors.AsQueryable());
            return directors;
        }
        public async Task<List<Director>> GetAllDirectors()
        {
            var directors = await _directors.FindAsync(x => x.Id != null).Result.ToListAsync();

            return directors;
        }
        public async Task<Director> GetDirectorById(string id)
        {
            return await _directors.FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
        }
        public async Task<Director> AddDirector(Director director)
        {
            
            await _directors.InsertOneAsync(director);
            return director;
        }

        public async void UpdateDirector(Director director)
        {
            await _directors.ReplaceOneAsync(x => x.Id == director.Id, director);
        }
        public async void DeleteDirector(string id)
        {
            await _directors.DeleteOneAsync(x => x.Id == id);
        }
    }
}
