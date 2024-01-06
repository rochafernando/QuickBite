using Application.Commands.Customer;
using Application.Responses.Customer;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Customer
{
    public class UpdateCustomerHandler : ICommandHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly NotificationContext _notificationContext;

        public UpdateCustomerHandler(
            ICustomerRepository customerRepository,
            ILogger<CreateCustomerHandler> logger,
            NotificationContext notificationContext)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task<CustomerResponse?> HandleAsync(UpdateCustomerCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(UpdateCustomerHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (command == null || command.Uid == Guid.Empty) 
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CommandIsNotValid });   
                return null;
            }

            var customer = await _customerRepository.GetByUidAsync(command.Uid);

            if (customer == null)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CustomerNotFound });
                return null;
            }

            customer.Update(command.Name, command.Document, command.Email);

            _notificationContext.AddNotification(customer);

            if (_notificationContext.HasNotifications) return null;

            await _customerRepository.UpdateAsync(customer);

            return new CustomerResponse { Id = customer.Id, Uid = customer.Uid, Name = customer.Name, Document = customer.Document, Email = customer.Email };
        }
    }
}
