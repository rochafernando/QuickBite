using Application.Queries.Order;
using Application.Responses.Customer;
using Application.Responses.Order;
using Application.Utils;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Handlers.Order
{
    public class FindOrderByUidHandler : IQueryHandler<FindOrderByUidQuery, OrderResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<FindOrderByUidHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IMoneyOrderRepository _moneyOrderRepository;

        public FindOrderByUidHandler(
            NotificationContext notificationContext,
            ILogger<FindOrderByUidHandler> logger,
            IOrderRepository orderRepository,
            IMoneyOrderRepository moneyOrderRepository)
        {
            _notificationContext = notificationContext;
            _logger = logger;
            _orderRepository = orderRepository;
            _moneyOrderRepository = moneyOrderRepository;
        }

        public async Task<OrderResponse?> HandleAsync(FindOrderByUidQuery query)
        {
            _logger.LogInformation(
                JsonConvert.SerializeObject(
                    new
                    {
                        Message = "Request Received",
                        Query = query,
                        ClassName = nameof(FindOrderByUidHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(query.Uid, out var uid) is false && uid == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.QueryIsNotValid });
                return null;
            }

            var order = await _orderRepository.GetByUidAsync(uid);

            if (order == null) return null;

            var moneyOrder = await _moneyOrderRepository.GetByOrderUidAsync(order.Uid); 

            return OrderUtil.CreateResponse(order, moneyOrder!);
        }
    }
}
