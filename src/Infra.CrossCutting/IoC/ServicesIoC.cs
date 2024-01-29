using Application.Utils;
using Domain.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC
{
    public static class ServicesIoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<NotificationContext>();
            services.AddScoped<OrderUtil>();

            return services;
        }
    }
}
