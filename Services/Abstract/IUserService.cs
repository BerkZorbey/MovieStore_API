using MovieStore_API.Models.DTOs;
using MovieStore_API.Models;

namespace MovieStore_API.Services.Abstract
{
    public interface IUserService
    {
        Task<User> AddUser(UserRegisterDTO user, string user_ıd);
        Task<User> GetUser(UserLoginDTO loginUser);
        Task<User> GetUserById(string Id);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
