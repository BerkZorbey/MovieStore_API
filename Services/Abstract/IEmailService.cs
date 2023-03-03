using Movie_API.Models;

namespace MovieStore_API.Services.Abstract
{
    public interface IEmailService
    {
        void CreateEmailVerificationToken(string id);
        UserEmailVerificationModel GetEmailVerification(string id);
    }
}
