using Application.Queries.Product;
using Application.Responses.Product;
using Application.Utils;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Handlers.Product
{
    public class FindProductCategoryByUidHandler : IQueryHandler<FindProductCategoryByUidQuery, ProductCategoryResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<FindProductCategoryByUidHandler> _logger;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public FindProductCategoryByUidHandler(
            NotificationContext notificationContext,
            ILogger<FindProductCategoryByUidHandler> logger,
            IProductCategoryRepository productCategoryRepository)
        {
            _notificationContext = notificationContext;
            _logger = logger;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategoryResponse?> HandleAsync(FindProductCategoryByUidQuery query)
        {
            _logger.LogInformation(
                JsonConvert.SerializeObject(
                    new
                    {
                        Message = "Request Received",
                        Query = query,
                        ClassName = nameof(FindProductCategoryByUidHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(query.Uid, out var uid) is false && uid == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.QueryIsNotValid });
                return null;
            }

            var category = await _productCategoryRepository.GetByUidAsync(uid);

            if (category == null) return null;

            return BuildProductCategoryResponse.Create(category);
        }
    }
}
