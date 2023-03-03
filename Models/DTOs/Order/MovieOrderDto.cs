namespace MovieStore_API.Models.DTOs.Order
{
    public class MovieOrderDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public List<Genre>? Genre { get; set; }
    }
}
