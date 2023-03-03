using Movie_API.Models;

namespace MovieStore_API.Models.DTOs.Cast
{
    public class CastDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public virtual List<CastMovieDto>? StarringMovies { get; set; }
    }
}
