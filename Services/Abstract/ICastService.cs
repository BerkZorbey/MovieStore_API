using Movie_API.Models;
using Movie_API.Models.Value_Object;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs.Cast;

namespace MovieStore_API.Services.Abstract
{
    public interface ICastService
    {
        PagingResultModel<Cast> GetCasts(PagingQuery query);
        Task<List<Cast>> GetAllCasts();
        Task<Cast> GetCastById(string id);
        Task<Cast> AddCast(CastDto cast);
        void UpdateCast(string id, CastDto updateCast);
        void DeleteCast(string id);
    }
}
