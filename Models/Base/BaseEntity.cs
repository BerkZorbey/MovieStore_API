using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore_API.Models.Base
{
    public class BaseEntity 
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CreatedBy { get; set; } = "Admin";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
