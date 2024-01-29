using Application.Commands.Product;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Product
{
    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductHandler> _logger;
        private readonly NotificationContext _notificationContext;

        public DeleteProductHandler(
            IProductRepository productRepository,
            ILogger<DeleteProductHandler> logger,
            NotificationContext notificationContext)
        {
            _productRepository = productRepository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task HandleAsync(DeleteProductCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(DeleteProductHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(command.Uid, out var result) is false && result == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CommandIsNotValid });
                return;
            }

            await _productRepository.DeleteAsync(Guid.Parse(command.Uid));
        }
    }
}
