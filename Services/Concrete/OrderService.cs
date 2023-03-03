using AutoMapper;
using MongoDB.Driver;
using Movie_API.Models;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Models.DTOs.Order;
using MovieStore_API.Services.Abstract;

namespace MovieStore_API.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        public OrderService(IConfiguration configuration, IMapper mapper, IUserService userService, IMovieService movieService)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieStoreDb");
            _orders = database.GetCollection<Order>("Orderes");
            _mapper = mapper;
            _userService = userService;
            _movieService = movieService;
        }
        public async Task<Order> AddCustomerOrder(OrderDto orderDto)
        {
            var getUser = await _userService.GetUserById(orderDto.CustomerId);
            var getMovie = await _movieService.GetMovieById(orderDto.MovieId);
            var mapUser = _mapper.Map<UserOrderDto>(getUser);
            var mapMovie = _mapper.Map<MovieOrderDto>(getMovie);
            var order = new Order
            {
                Movie = mapMovie,
                Customer = mapUser,
                Price = getMovie.Price,
                PurchaseDate = DateTime.Now,
            };
            await _orders.InsertOneAsync(order);
            return order;

        }

        public async Task<List<Order>> GetCustomerOrder(string customerId)
        {
            var order = await _orders.FindAsync(x => x.Customer.Id== customerId).Result.ToListAsync();

            return order;

        }
    }
}
