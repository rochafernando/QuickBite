using Application.Responses;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace API.Configurations.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class ExceptionsMiddleware
    {
        private const string TitleMessage = "Erro";
        private const string DetailMessage = "Um Erro Inesperado Aconteceu";
        private const string ContentType = "application/json";
        private const int ErrorCode = 50000;

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
                    ex.Message, 
                    new 
                    { 
                        Exception = ex, 
                        ClassName = nameof(ExceptionsMiddleware), 
                        MethodName = nameof(InvokeAsync) 
                    });

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = ContentType;
                await context.Response.WriteAsync(BuildResponse());
            }
        }

        private static string BuildResponse()
        {
            return JsonSerializer.Serialize(
                new ErrorResponse
                {
                    Code = ErrorCode,
                    Title = TitleMessage,
                    Detail = DetailMessage
                });
        }
    }
}
