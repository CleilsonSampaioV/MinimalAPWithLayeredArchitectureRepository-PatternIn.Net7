using MyTrainingSheet.Domain;
using Serilog;

namespace MyTrainingSheet.Api.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(ex, httpContext);
            }
        }

        private async Task HandleException(Exception ex, HttpContext httpContext)
        {
            Log.Error(ex, "Error happend!");

            if (ex is InvalidOperationException)
            {
                //httpContext.Response.WriteAsync("Invalid operation");
                //httpContext.Response.WriteAsync("Invalid operation");
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsJsonAsync(new ResponseModel
                {
                    Message = "Invalid operation",
                    StatusCode = 400,
                    Success = false
                });
            }
            else if (ex is ArgumentException)
            {
                await httpContext.Response.WriteAsync("Invalid argument");
            }
            else
            {
                await httpContext.Response.WriteAsync("Unknown error");
            }
        }
    }
    public static class ExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }
}
