using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Movie_API.Models;
using MovieStore_API.Models.Base;

namespace MovieStore_API.Models
{
    public class User : BaseEntity
    {

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
        public List<Movie>? PurchasedMovies { get; set; }
        public List<Genre>? FavoriteGenres { get; set; }
        public Token? Token { get; set; }
        public bool isActivatedEmail { get; set; } = false;
        public bool isDeleted { get; set; } = false;
    }
}
