using Application.Queries.Product;
using Application.Responses.Product;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Product
{
    public class FindProductByUidHandler : IQueryHandler<FindProductByUidQuery, ProductResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<FindProductByUidHandler> _logger;
        private readonly IProductRepository _productRepository;

        public FindProductByUidHandler(
            NotificationContext notificationContext,
            ILogger<FindProductByUidHandler> logger,
            IProductRepository productRepository)
        {
            _notificationContext = notificationContext;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<ProductResponse?> HandleAsync(FindProductByUidQuery query)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Query = query,
                        ClassName = nameof(FindProductByUidHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(query.Uid, out var result) is false && result == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.QueryIsNotValid });
                return null;
            }

            var product = await _productRepository.GetByUidAsync(Guid.Parse(query.Uid));

            if (product == null) return null;

            return new ProductResponse { Uid = product.Uid, Name = product.Characteristics?.Name! };
        }
    }
}
