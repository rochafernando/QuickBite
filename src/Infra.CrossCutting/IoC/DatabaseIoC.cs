using Domain.Configurations;
using Domain.Interfaces.Configurations;
using Infra.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infra.CrossCutting.IoC
{
    public static class DatabaseIoC
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDatabaseConfiguration>(x => x.GetRequiredService<IOptions<DatabaseConfiguration>>().Value);
            services.AddSingleton<MongoService>();
            return services;
        }
    }
}
