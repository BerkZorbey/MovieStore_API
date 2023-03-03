using Movie_API.Models.Value_Object;
using Movie_API.Models;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Models;

namespace MovieStore_API.Repository.Abstract
{
    public interface IDirectorRepository
    {
        PagingResultModel<Director> GetDirectors(PagingQuery query);
        Task<Director> GetDirectorById(string id);
        Task<Director> AddDirector(Director director);
        Task<List<Director>> GetAllDirectors();
        void UpdateDirector(Director director);
        void DeleteDirector(string id);
    }
}
