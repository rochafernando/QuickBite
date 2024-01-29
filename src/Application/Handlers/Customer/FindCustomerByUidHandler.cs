using Application.Commands;
using Application.Queries.Customer;
using Application.Responses.Customer;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Customer
{
    public class FindCustomerByUidHandler : IQueryHandler<FindCustomerByUidQuery, CustomerResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<FindCustomerByUidHandler> _logger;
        private readonly ICustomerRepository _customerRepository;

        public FindCustomerByUidHandler(
            NotificationContext notificationContext,
            ILogger<FindCustomerByUidHandler> logger,
            ICustomerRepository customerRepository)
        {
            _notificationContext = notificationContext;
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponse?> HandleAsync(FindCustomerByUidQuery query)
        {
            _logger.LogInformation(
                 JsonSerializer.Serialize(
                     new
                     {
                         Message = "Request Received",
                         Query = query,
                         ClassName = nameof(FindCustomerByUidHandler),
                         MethodName = nameof(HandleAsync)
                     }));

            if (Guid.TryParse(query.Uid, out var result) is false && result == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.QueryIsNotValid });
                return null;
            }

            var customer = await _customerRepository.GetByUidAsync(Guid.Parse(query.Uid));

            if (customer == null) return null;


            return new CustomerResponse { Uid = customer.Uid, Document = customer.Document, Email = customer.Email, Name = customer.Name };
        }
    }
}
