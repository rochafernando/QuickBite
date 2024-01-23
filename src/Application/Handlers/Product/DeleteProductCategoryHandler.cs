using Application.Commands.Product;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Product
{
    public class DeleteProductCategoryHandler : ICommandHandler<DeleteProductCategoryCommand>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ILogger<DeleteProductCategoryHandler> _logger;
        private readonly NotificationContext _notificationContext;

        public DeleteProductCategoryHandler(
            IProductCategoryRepository productCategoryRepository,
            ILogger<DeleteProductCategoryHandler> logger,
            NotificationContext notificationContext)
        {
            _productCategoryRepository = productCategoryRepository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task HandleAsync(DeleteProductCategoryCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(DeleteProductCategoryHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(command.Uid, out var result) is false && result == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CommandIsNotValid });
                return;
            }

            await _productCategoryRepository.DeleteAsync(Guid.Parse(command.Uid));
        }
    }
}
