using MovieStore_API.Models;

namespace MovieStore_API.Services.Abstract
{
    public interface ITokenGeneratorService
    {
        Token GenerateToken();
    }
}
