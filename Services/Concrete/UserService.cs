using AutoMapper;
using FluentValidation;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Movie_API.Models;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Models.DTOs.Validator;
using MovieStore_API.Services.Abstract;

namespace MovieStore_API.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMapper _mapper;
        public UserService(IConfiguration configuration, IMapper mapper)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieStoreDb");
            _users = database.GetCollection<User>("Users");
            _mapper = mapper;

        }

        public async Task<User> AddUser(UserRegisterDTO user, string user_ıd)
        {
            
            UserRegisterValidator validations = new();
            await validations.ValidateAndThrowAsync(user);
            var newUser = _mapper.Map<User>(user);
            newUser.Id = user_ıd;
            await _users.InsertOneAsync(newUser);
            return newUser;
        }
        public async Task<User> GetUser(UserLoginDTO loginUser)
        {
            UserLoginValidator validations = new();
            await validations.ValidateAndThrowAsync(loginUser);
            var userModel = _mapper.Map<User>(loginUser);
            var user = await _users.FindAsync(x => x.Email == userModel.Email && x.isDeleted == false).Result.FirstOrDefaultAsync();
            return user;
        }
        public async Task<User> GetUserById(string Id)
        {
            var user = await _users.FindAsync(x => x.Id == Id && x.isDeleted == false).Result.FirstOrDefaultAsync();
            return user;
        }
        public void UpdateUser(User user)
        {
            _users.ReplaceOne(x => x.Id == user.Id, user);
        }
        public void DeleteUser(User user)
        {
            _users.GetType().GetProperty("isDeleted").SetValue(user, true);
        }
    }
}
