using MovieStore_API.Models;
using MovieStore_API.Models.DTOs;

namespace MovieStore_API.Services.Abstract
{
    public interface IOrderService
    {
        Task<Order> AddCustomerOrder(OrderDto order);
        Task<List<Order>> GetCustomerOrder(string customerId);
    }
}
