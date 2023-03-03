using MovieStore_API.Models.DTOs;

namespace MovieStore_API.Services.Abstract
{
    public interface IValidationService
    {
        Task<bool> IsEmailValid(UserRegisterDTO user);
        Task<bool> IsUserNameValid(UserRegisterDTO user);
        bool IsPasswordValid(UserRegisterDTO user);
        bool IsConditionsValid(bool checkEmail, bool checkUserName, bool checkPassword);
    }
}
