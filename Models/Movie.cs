using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MovieStore_API.Models;
using MovieStore_API.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Movie_API.Models
{
    public class Movie : BaseEntity
    {
        public string? Name { get; set; }
        public List<Genre>? Genre { get; set; }
        public DateTime? Release_Year { get; set; }
        public List<Director>? Director { get; set; }
        public List<Cast>? Cast { get; set; }
        public float? Price { get; set; }
        public bool? isDeleted { get; set; } = false;

        
    }
}
