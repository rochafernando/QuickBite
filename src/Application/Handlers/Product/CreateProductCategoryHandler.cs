using Application.Commands.Product;
using Application.Responses.Product;
using Application.Utils;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Product
{
    public class CreateProductCategoryHandler : ICommandHandler<CreateProductCategoryCommand, ProductCategoryResponse>
    {
        private readonly ILogger<CreateProductCategoryHandler> _logger;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly NotificationContext _notificationContext;

        public CreateProductCategoryHandler(
            ILogger<CreateProductCategoryHandler> logger,
            IProductCategoryRepository productCategoryRepository,
            NotificationContext notificationContext)
        {
            _logger = logger;
            _productCategoryRepository = productCategoryRepository;
            _notificationContext = notificationContext;
        }

        public async Task<ProductCategoryResponse?> HandleAsync(CreateProductCategoryCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(CreateProductCategoryHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            var productCategory = Domain.Entities.ProductCategory.Create(command.Name, command.Description);

            _notificationContext.AddNotification(productCategory);

            if (_notificationContext.HasNotifications) return null;

            await _productCategoryRepository.AddAsync(productCategory);

            return BuildProductCategoryResponse.Create(productCategory);
        }
    }
}
