using Application.Commands.Order;
using Application.Handlers.Customer;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Handlers.Order
{
    public class DeleteOrderHandler : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteOrderHandler> _logger;
        private readonly NotificationContext _notificationContext;

        public DeleteOrderHandler(
            IOrderRepository orderRepository,
            ILogger<DeleteOrderHandler> logger,
            NotificationContext notificationContext)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task HandleAsync(DeleteOrderCommand command)
        {
            _logger.LogInformation(
                JsonConvert.SerializeObject(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(DeleteOrderHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(command.Uid, out var uid) is false && uid == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CommandIsNotValid });
                return;
            }

            await _orderRepository.DeleteAsync(uid);
        }
    }
}
