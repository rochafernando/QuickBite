using Application.Responses;
using System.Net;
using System.Text.Json;

namespace API.Configurations.Middlewares
{
    public class ExceptionsMiddleware
    {
        private const string TitleMessage = "Erro";
        private const string DetailMessage = "Um Erro Inesperado Aconteceu";

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(
            RequestDelegate next,
            ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(
                    DetailMessage, 
                    new 
                    { 
                        Exception = ex, 
                        ClassName = nameof(ExceptionsMiddleware), 
                        MethodName = nameof(InvokeAsync) 
                    });

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(BuildResponse());
            }
        }

        private static string BuildResponse()
        {
            return JsonSerializer.Serialize(
                new ErrorResponse
                {
                    Code = 50000,
                    Title = TitleMessage,
                    Detail = DetailMessage
                });
        }
    }
}
