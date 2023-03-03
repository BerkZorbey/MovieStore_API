using Movie_API.Models;
using MovieStore_API.Models.Base;
using MovieStore_API.Models.DTOs.Cast;

namespace MovieStore_API.Models
{
    public class Cast : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? FullName => Name + " " + Surname;
        public virtual List<StarringMoviesDto>? StarringMovies { get; set; }
    }
}
