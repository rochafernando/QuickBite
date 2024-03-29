﻿using Application.Commands.Customer;
using Application.Commands.Order;
using Application.Commands.Product;
using Application.Handlers.Customer;
using Application.Handlers.Order;
using Application.Handlers.Product;
using Application.Responses.Customer;
using Application.Responses.Order;
using Application.Responses.Product;
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
            services.AddScoped<ICommandHandler<UpdateCustomerCommand, CustomerResponse>, UpdateCustomerHandler>();
            services.AddScoped<ICommandHandler<DeleteCustomerCommand>, DeleteCustomerHandler>();
            services.AddScoped<ICommandHandler<CreateProductCategoryCommand, ProductCategoryResponse>, CreateProductCategoryHandler>();
            services.AddScoped<ICommandHandler<UpdateProductCategoryCommand, ProductCategoryResponse>, UpdateProductCategoryHandler>();
            services.AddScoped<ICommandHandler<DeleteProductCategoryCommand>, DeleteProductCategoryHandler>();
            services.AddScoped<ICommandHandler<CreateProductCommand, ProductResponse>, CreateProductHandler>();
            services.AddScoped<ICommandHandler<UpdateProductCommand, ProductResponse>, UpdateProductHandler>();
            services.AddScoped<ICommandHandler<DeleteProductCommand>, DeleteProductHandler>();
            services.AddScoped<ICommandHandler<CreateOrderCommand, OrderResponse>, CreateOrderHandler>();
            services.AddScoped<ICommandHandler<DeleteOrderCommand>, DeleteOrderHandler>();
            services.AddScoped<ICommandHandler<UpdateOrderCommand, OrderResponse>, UpdateOrderHandler>();

            return services;
        }
    }
}
