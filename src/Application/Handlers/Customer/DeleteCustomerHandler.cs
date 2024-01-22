using Application.Commands.Customer;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Customer
{
    public class DeleteCustomerHandler : ICommandHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly NotificationContext _notificationContext;

        public DeleteCustomerHandler(
            ICustomerRepository customerRepository,
            ILogger<CreateCustomerHandler> logger,
            NotificationContext notificationContext)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task HandleAsync(DeleteCustomerCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(DeleteCustomerHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(command.Uid, out var result) is false && result == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CommandIsNotValid });
                return;
            }

            await _customerRepository.DeleteAsync(Guid.Parse(command.Uid));
        }
    }
}
