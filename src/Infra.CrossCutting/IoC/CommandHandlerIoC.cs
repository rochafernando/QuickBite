using Application.Commands.Customer;
using Application.Handlers.Customer;
using Application.Responses.Customer;
using Domain.Interfaces.CQS;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC
{
    public static class CommandHandlerIoC
    {
        public static IServiceCollection AddCommandHandler(this IServiceCollection services, Type handlerInterface)
        {
            //var handlers = handlerInterface
            //                .Assembly
            //                .GetTypes()
            //                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface));

            //foreach (var handler in handlers)
            //{
            //    services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            //}

            services.AddScoped<ICommandHandler<CreateCustomerCommand, CustomerResponse>, CreateCustomerHandler>();

            return services;
        }
    }
}
