namespace MovieStore_API.Models.DTOs
{
    public class UserLoginDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool isActivatedEmail { get; set; }
    }
}
