using System.Text.Json;
using Goals.API.Exceptions;

namespace Goals.API.Middlewares
{
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException ex)
            {
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, HttpException ex)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = ex.StatusCode;

            var json = new
            {
                ex.Message,
                ex.StatusCode
            };

            await response.WriteAsync(JsonSerializer.Serialize(json));
        }
    }
}
