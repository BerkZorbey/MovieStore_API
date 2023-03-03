using Movie_API.Models;
using MovieStore_API.Models.Base;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Models.DTOs.Movie;

namespace MovieStore_API.Models
{
    public class Director : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? FullName => Name + " " + Surname;
        public virtual List<MoviesDirectedByDto>? MoviesDirectedBy { get; set; }
    }
}
