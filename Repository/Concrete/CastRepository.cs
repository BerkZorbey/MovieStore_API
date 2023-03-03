using AutoMapper;
using MongoDB.Driver;
using Movie_API.Models.Value_Object;
using Movie_API.Models;
using MovieStore_API.Models;
using MovieStore_API.Repository.Abstract;

namespace MovieStore_API.Repository.Concrete
{
    public class CastRepository : ICastRepository
    {
        private readonly IMongoCollection<Cast> _casts;
        private readonly IMapper _mapper;
        public CastRepository(IConfiguration configuration, IMapper mapper)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieStoreDb");
            _casts = database.GetCollection<Cast>("Casts");
            _mapper = mapper;
        }
        public async Task<Cast> AddCast(Cast cast)
        {

            await _casts.InsertOneAsync(cast);
            return cast;
        }

        public async void DeleteCast(string id)
        {
           await _casts.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Cast> GetCastById(string id)
        {
            return await _casts.FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
        }

        public PagingResultModel<Cast> GetCasts(PagingQuery query)
        {
            PagingResultModel<Cast> casts = new PagingResultModel<Cast>(query);
            casts.GetData(_casts.AsQueryable());
            return casts;
        }
        public async Task<List<Cast>> GetAllCasts()
        {
            var casts = await _casts.FindAsync(x => x.Id != null).Result.ToListAsync();

            return casts;
        }

        public async void UpdateCast(Cast cast)
        {
            await _casts.ReplaceOneAsync(x => x.Id == cast.Id, cast);
        }
    }
}
