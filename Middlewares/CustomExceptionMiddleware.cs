using MovieStore_API.Models;
using MovieStore_API.Services.Abstract;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MovieStore_API.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var watch = Stopwatch.StartNew();
            List<Log> logList = new();
            Log log = new Log();

            try
            {
                string message = "[Request] HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path;
                await _next(httpContext);
                string exceptionMessage = "Error with " + httpContext.Response.StatusCode + " status code received. ";
                if (httpContext.Response.StatusCode != (int)HttpStatusCode.OK && httpContext.Response.StatusCode != (int)HttpStatusCode.Created)
                {
                    throw new Exception(exceptionMessage);
                }
                watch.Stop();
                message = "[Response] HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path + " responsed " + httpContext.Response.StatusCode + " in " + watch.ElapsedMilliseconds + " ms";
                log.Id = logList.Count() + 1;
                log.LogMessage = message;
                logList.Add(log);

                _logger.LogWrite(logList);
            }
            catch (Exception ex)
            {

                watch.Stop();
                await HandleException(httpContext, ex, log, logList);
            }

        }
        private Task HandleException(HttpContext context, Exception ex, Log log, List<Log> logList)
        {
            string message = "[Error] Http " + context.Request.Method + " - " + context.Request.Path + " - " + " Error Message: " + ex.Message;
            log.Id = logList.Count() + 1;
            log.LogMessage = message;
            logList.Add(log);
            _logger.LogWrite(logList);
            var result = JsonSerializer.Serialize(new
            {
                error = ex.Message,
            });

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
