using AutoMapper;
using MongoDB.Driver;
using Movie_API.Models;
using MovieStore_API.Services.Abstract;

namespace MovieStore_API.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly IMongoCollection<UserEmailVerificationModel> _userEmailVerification;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public EmailService(IConfiguration configuration, ITokenGeneratorService tokenGeneratorService)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieStoreDb");
            _userEmailVerification = database.GetCollection<UserEmailVerificationModel>("UserEmailVerification");
            _tokenGeneratorService = tokenGeneratorService;
        }
        public async void CreateEmailVerificationToken(string id)
        {
            var emailModel = new UserEmailVerificationModel();
            var token = _tokenGeneratorService.GenerateToken();
            emailModel.EmailVerificationToken = token;
            emailModel.UserId = id;
            await _userEmailVerification.InsertOneAsync(emailModel);
        }
        public UserEmailVerificationModel GetEmailVerification(string id)
        {
            var emailVerification = _userEmailVerification.Find(x => x.UserId == id).FirstOrDefault();
            return emailVerification;
        }
    }
}
