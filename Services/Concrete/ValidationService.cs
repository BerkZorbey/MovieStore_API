using Movie_API.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Services.Abstract;

namespace MovieStore_API.Services.Concrete
{
    public class ValidationService : IValidationService
    {
        private readonly IMongoCollection<User> _users;
        public ValidationService(IConfiguration configuration)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieStoreDb");
            _users = database.GetCollection<User>("Users");

        }
        public async Task<bool> IsEmailValid(UserRegisterDTO user)
        {
            var email = await _users.FindAsync(x => x.Email == user.Email).Result.FirstOrDefaultAsync();
            if (email == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> IsUserNameValid(UserRegisterDTO user)
        {
            var userName = await _users.FindAsync(x => x.UserName == user.UserName).Result.FirstOrDefaultAsync();
            if (userName == null)
            {
                return false;
            }
            return true;
        }
        public bool IsPasswordValid(UserRegisterDTO user)
        {
            var password = user.Password;
            var passwordLength = password.Length;
            var passwordLower = password.Any(char.IsLower);
            var passwordUpper = password.Any(char.IsUpper);
            var passwordDigit = password.Any(char.IsDigit);

            if (passwordLength >= 8 && passwordLower == true && passwordUpper == true && passwordDigit == true)
            {
                return true;
            }
            return false;

        }
        public bool IsConditionsValid(bool checkEmail, bool checkUserName, bool checkPassword)
        {
            if (checkEmail == false && checkUserName == false && checkPassword == true) return true;

            return false;
        }
    }
}
