using Application.Commands.Product;
using Application.Responses.Product;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Product
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly ILogger<CreateProductHandler> _logger;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly NotificationContext _notificationContext;

        public UpdateProductHandler(
            ILogger<CreateProductHandler> logger,
            IProductCategoryRepository productCategoryRepository,
            IProductRepository productRepository,
            NotificationContext notificationContext)
        {
            _logger = logger;
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _notificationContext = notificationContext;
        }

        public async Task<ProductResponse?> HandleAsync(UpdateProductCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(UpdateProductHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(command.Uid, out var uid) is false && uid == Guid.Empty ||
                Guid.TryParse(command.CategoryUid, out var categoryUid) is false && categoryUid == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CommandIsNotValid });
                return null;
            }

            var category = await _productCategoryRepository.GetByUidAsync(Guid.Parse(command.CategoryUid));

            if (category == null)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CategoryNotFound });
                return null;
            }

            var product = await _productRepository.GetByUidAsync(Guid.Parse(command.Uid));

            if (product == null)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.ProductNotFound });
                return null;
            }

            product.Update(
                category,
                Domain.ValuesObjects.ProductCharacteristics.Create(command.Name, command.Description, command.PathImage),
                command.Sellable,
                command.Enable,
                Domain.ValuesObjects.PriceComposition.Create(command.UnitPrice, command.ComboPrice),
                product.AuditDate!);

            _notificationContext.AddNotification(product);

            if (_notificationContext.HasNotifications) return null;

            await _productRepository.UpdateAsync(product);

            return new ProductResponse { Uid = product.Uid, Name = product.Characteristics!.Name };
        }
    }
}
