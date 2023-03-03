using Movie_API.Models;
using MovieStore_API.Models.Base;
using MovieStore_API.Models.DTOs.Order;

namespace MovieStore_API.Models
{
    public class Order : BaseEntity
    {
        public UserOrderDto? Customer { get; set; }
        public MovieOrderDto? Movie { get; set; }
        public float? Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
