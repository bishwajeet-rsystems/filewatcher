using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileWatcher.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var startTime = DateTime.Now;

            await _next(httpContext);

            var endTime = DateTime.Now;
            var elapsedTime = endTime - startTime;

            var logMessage = $"custom log message {httpContext.Request.Method} {httpContext.Request.Path} {httpContext.Response.StatusCode} {elapsedTime.TotalMilliseconds}ms";
            Console.WriteLine(logMessage);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
