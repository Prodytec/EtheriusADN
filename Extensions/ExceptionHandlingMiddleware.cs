#pragma warning disable

namespace EtheriusWebAPI.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger; // Inject the ILogger

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger; // Inject the logger
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Continue processing the request
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An unhandled exception occurred.");

                // Set the response status code based on the exception type
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Return a JSON response with the error message
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync($"{{ \"error\": \"{ex.InnerException.Message}\" }}");
            }
        }
    }
}
