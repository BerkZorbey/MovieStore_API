using MongoDB.Bson.Serialization.Attributes;
using MovieStore_API.Models;

namespace Movie_API.Models
{
    public class UserEmailVerificationModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? UserId { get; set; }
        public Token? EmailVerificationToken { get; set; }
    }
}
