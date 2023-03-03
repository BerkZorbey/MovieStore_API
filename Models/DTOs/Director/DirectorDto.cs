using Movie_API.Models;

namespace MovieStore_API.Models.DTOs.Director
{
    public class DirectorDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public virtual List<DirectorMovieDto>? MoviesDirectedBy { get; set; }
    }
}
