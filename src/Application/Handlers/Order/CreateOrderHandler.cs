using Application.Commands.Order;
using Application.Responses.Order;
using Application.Utils;
using Domain.Entities;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Handlers.Order
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly ILogger<CreateOrderHandler> _logger;
        private readonly NotificationContext _notificationContext;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly OrderUtil _orderUtil;
        private readonly IMoneyOrderRepository _moneyOrderRepository;

        public CreateOrderHandler(
            ILogger<CreateOrderHandler> logger,
            NotificationContext notificationContext,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            OrderUtil orderUtil,
            IMoneyOrderRepository moneyOrderRepository)
        {
            _logger = logger;
            _notificationContext = notificationContext;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderUtil = orderUtil;
            _moneyOrderRepository = moneyOrderRepository;
        }

        public async Task<OrderResponse?> HandleAsync(CreateOrderCommand command)
        {
            _logger.LogInformation(
                JsonSerializer.Serialize(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(CreateOrderHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            var items = await _orderUtil.GetItemsFromOrderAsync(command.Items.ToList());

            if (items is null && _notificationContext.HasNotifications) return null;

            var order = Domain.Entities.Order.Create(await _orderUtil.GetCustomerFromOrderAsync(command.Customer), items!);

            _notificationContext.AddNotification(order);

            if (_notificationContext.HasNotifications) return null;

            var moneyOrder = MoneyOrder.Create(order.Value, order.Uid);
            moneyOrder.SetQrCodeBytes(QrCodeManagement.GenerateImage(moneyOrder.QRCode));

            _notificationContext.AddNotification(order);

            if (_notificationContext.HasNotifications) return null;

            await _orderRepository.AddAsync(order);
            await _moneyOrderRepository.AddAsync(moneyOrder);

            return OrderUtil.CreateResponse(order, items!, moneyOrder);
        }
    }
}
