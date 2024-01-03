namespace API.Configurations.Middlewares
{
    public static class ExceptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsMiddleware>();
        }
    }
}
