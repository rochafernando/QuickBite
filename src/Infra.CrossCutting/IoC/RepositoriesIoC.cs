﻿using Domain.Interfaces.Repositories;
using Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC
{
    public static class RepositoriesIoC
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientRepository>(_ => new ClientRepository(configuration.GetSection("SqlServer").Value));

            return services;
        }
    }
}