using Application.Commands.Customer;
using Application.Responses.Customer;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Customer
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly NotificationContext _notificationContext;
        private readonly ICustomerRepository _clientRepository;

        public CreateCustomerHandler(
            ILogger<CreateCustomerHandler> logger,
            NotificationContext notificationContext,
            ICustomerRepository clientRepository)
        {
            _logger = logger;
            _notificationContext = notificationContext;
            _clientRepository = clientRepository;
        }

        public async Task<CustomerResponse?> HandleAsync(CreateCustomerCommand command)
        {
            _logger.LogInformation(
                "Request Received",
                new
                {
                    Command = command,
                    ClassName = nameof(CreateCustomerHandler),
                    MethodName = nameof(HandleAsync)
                });

            var client = Domain.Entities.Customer.Create(command.Name, command.Document, command.Email);

            _notificationContext.AddNotification(client);

            if (_notificationContext.HasNotifications) return null;

            await _clientRepository.AddAsync(client);

            return new CustomerResponse { Id = client.Id, Uid = client.Uid, Name = client.Name, Document = client.Document, Email = client.Email };
        }
    }
}
