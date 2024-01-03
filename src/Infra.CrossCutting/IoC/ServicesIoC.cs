using Domain.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC
{
    public static class ServicesIoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();

            return services;
        }
    }
}
