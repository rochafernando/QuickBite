using Application.Commands.Customer;
using Application.Responses.Customer;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Customer
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly NotificationContext _notificationContext;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(
            ILogger<CreateCustomerHandler> logger,
            NotificationContext notificationContext,
            ICustomerRepository customerRepository)
        {
            _logger = logger;
            _notificationContext = notificationContext;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponse?> HandleAsync(CreateCustomerCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(CreateCustomerHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            var customer = Domain.Entities.Customer.Create(command.Name, command.Document, command.Email);

            _notificationContext.AddNotification(customer);

            if (_notificationContext.HasNotifications) return null;

            await _customerRepository.AddAsync(customer);

            return new CustomerResponse { Id = customer.Id, Uid = customer.Uid, Name = customer.Name, Document = customer.Document, Email = customer.Email };
        }
    }
}
