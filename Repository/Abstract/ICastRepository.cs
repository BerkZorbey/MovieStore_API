using Movie_API.Models.Value_Object;
using Movie_API.Models;
using MovieStore_API.Models;

namespace MovieStore_API.Repository.Abstract
{
    public interface ICastRepository
    {
        PagingResultModel<Cast> GetCasts(PagingQuery query);
        Task<List<Cast>> GetAllCasts();
        Task<Cast> GetCastById(string id);
        Task<Cast> AddCast(Cast cast);
        void UpdateCast(Cast cast);
        void DeleteCast(string id);
    }
}
