using Application.Responses;
using Domain.Notifications;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace API.Configurations.Filters
{
    public class NotificationsFilter : IAsyncResultFilter, IAsyncActionFilter
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<NotificationsFilter> _logger;

        public NotificationsFilter(
            NotificationContext notificationContext,
            ILogger<NotificationsFilter> logger)
        {
            _notificationContext = notificationContext;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _notificationContext.Clean();

            await next();
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(BuildResponse()));
                return;
            }

            await next();
        }

        private IEnumerable<ErrorResponse> BuildResponse()
        {
            var result = new List<ErrorResponse>();

            _notificationContext.Notifications.ToList().ForEach(notification =>
            {
                _logger.LogWarning(
                    JsonSerializer.Serialize(new
                    {
                        notification.Code,
                        notification.Title,
                        notification.Message,
                        ClassName = nameof(NotificationsFilter),
                        MethodName = nameof(OnResultExecutionAsync),

                    }));

                result.Add(new ErrorResponse { Code = notification.Code, Title = notification.Title, Detail = notification.Message });
            });

            return result;
        }
    }
}
