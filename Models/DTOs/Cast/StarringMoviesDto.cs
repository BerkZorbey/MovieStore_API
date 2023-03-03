using MovieStore_API.Models.DTOs.Movie;

namespace MovieStore_API.Models.DTOs.Cast
{
    public class StarringMoviesDto
    {
        public string? Name { get; set; }
        public List<Genre>? Genre { get; set; }
        public DateTime? Release_Year { get; set; }
        public List<MovieDirector>? Director { get; set; }
        public float? Price { get; set; }
    }
}
