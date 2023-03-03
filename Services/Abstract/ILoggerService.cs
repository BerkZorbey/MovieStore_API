using MovieStore_API.Models;

namespace MovieStore_API.Services.Abstract
{
    public interface ILoggerService
    {
        void LogWrite(List<Log> logList);
    }
}
