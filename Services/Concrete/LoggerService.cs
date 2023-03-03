using MovieStore_API.Models;
using MovieStore_API.Services.Abstract;
using System.Text.Json;

namespace MovieStore_API.Services.Concrete
{
    public class LoggerService : ILoggerService
    {

        public void LogWrite(List<Log> logList)
        {
            Console.WriteLine(logList);
        }
    }
}
