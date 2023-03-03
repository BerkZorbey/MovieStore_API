using Movie_API.Models;

namespace MovieStore_API.Models.DTOs
{
    public class OrderDto
    {
        public string CustomerId { get; set; }
        public string MovieId { get; set; }
    }
}
