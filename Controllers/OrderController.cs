using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_API.Controllers;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Services.Abstract;

namespace MovieStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetCustomerOrder(string CustomerId)
        {
            var order = await _orderService.GetCustomerOrder(CustomerId);
            if(order != null)
            {
                return Ok(order);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomerOrder([FromBody]OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderService.AddCustomerOrder(orderDto);
                return Ok(order);
            }
            return BadRequest();

        }
    }
}
