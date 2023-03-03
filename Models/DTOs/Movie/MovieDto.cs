namespace MovieStore_API.Models.DTOs.Movie
{
    public class MovieDto
    {
        public string? Name { get; set; }
        public List<Genre>? Genre { get; set; }
        public DateTime? Release_Year { get; set; }
        public List<MovieDirector>? Director { get; set; }
        public List<MovieCast>? Cast { get; set; }
        public float? Price { get; set; }
    }
}
