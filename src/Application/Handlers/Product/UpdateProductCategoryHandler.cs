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
    public class UpdateProductCategoryHandler : ICommandHandler<UpdateProductCategoryCommand, ProductCategoryResponse>
    {
        private readonly ILogger<UpdateProductCategoryHandler> _logger;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly NotificationContext _notificationContext;

        public UpdateProductCategoryHandler(
            ILogger<UpdateProductCategoryHandler> logger,
            IProductCategoryRepository productCategoryRepository,
            NotificationContext notificationContext)
        {
            _logger = logger;
            _productCategoryRepository = productCategoryRepository;
            _notificationContext = notificationContext;
        }

        public async Task<ProductCategoryResponse?> HandleAsync(UpdateProductCategoryCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(UpdateProductCategoryHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(command.Uid, out var result) is false && result == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CommandIsNotValid });
                return null;
            }

            var productCategory = await _productCategoryRepository.GetByUidAsync(Guid.Parse(command.Uid));

            if (productCategory == null)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CategoryNotFound });
                return null;
            }

            productCategory.Update(command.Name, command.Description);

            _notificationContext.AddNotification(productCategory);

            if (_notificationContext.HasNotifications) return null;

            await _productCategoryRepository.UpdateAsync(productCategory);

            return new ProductCategoryResponse { Uid = productCategory.Uid, Name = productCategory.Name, Description = productCategory.Description };
        }
    }
}
