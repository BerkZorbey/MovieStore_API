namespace MovieStore_API.Models
{
    public class Token
    {
        public string? AccessToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
