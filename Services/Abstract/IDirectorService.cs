using Movie_API.Models.Value_Object;
using Movie_API.Models;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs.Director;

namespace MovieStore_API.Services.Abstract
{
    public interface IDirectorService
    {
        PagingResultModel<Director> GetDirectors(PagingQuery query);
        Task<Director> GetDirectorById(string id);
        Task<Director> AddDirector(DirectorDto director);
        Task<List<Director>> GetAllDirectors();
        void UpdateDirector(string id, DirectorDto director);
        void DeleteDirector(string id);
    }
}
