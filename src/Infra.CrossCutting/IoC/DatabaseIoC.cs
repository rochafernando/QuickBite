using Infra.Data.Configurations;
using Infra.Data.Interfaces;
using Infra.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC
{
    public static class DatabaseIoC
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbConfiguration = configuration.GetSection("Mongo").Get<MongoDbConfiguration>();
            services.AddSingleton(mongoDbConfiguration!);
            services.AddSingleton<IMongoDbService, MongoDbService>();
            return services;
        }
    }
}
