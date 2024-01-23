using Application.Handlers.Customer;
using Application.Handlers.Product;
using Application.Queries.Customer;
using Application.Responses.Customer;
using Application.Responses.Product;
using Domain.Interfaces.CQS;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC
{
    public static class QueryHandlerIoC
    {
        public static IServiceCollection AddQueryHandler(this IServiceCollection services, Type handlerInterface)
        {
            //var handlers = handlerInterface
            //                .Assembly
            //                .GetTypes()
            //                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface));

            //foreach (var handler in handlers)
            //{
            //    services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            //}

            services.AddScoped<IQueryHandler<FindCustomerByUidQuery, CustomerResponse>, FindCustomerByUidHandler>();
            services.AddScoped<IQueryHandler<IEnumerable<ProductCategoryResponse>>, GetAllProductCategoryHandler>();

            return services;
        }
    }
}
