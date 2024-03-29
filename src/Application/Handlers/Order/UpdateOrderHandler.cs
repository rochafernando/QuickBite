﻿using Application.Commands.Order;
using Application.Responses.Order;
using Application.Utils;
using Domain.Entities;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Handlers.Order
{
    public class UpdateOrderHandler : ICommandHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<UpdateOrderHandler> _logger;
        private readonly NotificationContext _notificationContext;
        private readonly OrderUtil _orderUtil;
        private readonly IMoneyOrderRepository _moneyOrderRepository;

        public UpdateOrderHandler(
            IOrderRepository orderRepository,
            ILogger<UpdateOrderHandler> logger,
            NotificationContext notificationContext,
            OrderUtil orderUtil,
            IMoneyOrderRepository moneyOrderRepository)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _notificationContext = notificationContext;
            _orderUtil = orderUtil;
            _moneyOrderRepository = moneyOrderRepository;
        }

        public async Task<OrderResponse?> HandleAsync(UpdateOrderCommand command)
        {
            _logger.LogInformation(
                JsonConvert.SerializeObject(
                    new
                    {
                        Message = "Request Received",
                        Command = command,
                        ClassName = nameof(UpdateOrderHandler),
                        MethodName = nameof(HandleAsync)
                    }));

            if (Guid.TryParse(command.Uid, out var uid) is false && uid == Guid.Empty)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CommandIsNotValid });
                return null;
            }

            var order = await _orderRepository.GetByUidAsync(uid);

            if (order == null)
            {
                _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.OrderNotFound });
                return null;
            }

            var items = await _orderUtil.GetItemsFromOrderAsync(command.Items.ToList());

            if (items is null && _notificationContext.HasNotifications) return null;
            
            order.Update(await _orderUtil.GetCustomerFromOrderAsync(command.Customer), items!, command.Status);

            _notificationContext.AddNotification(order);

            if (_notificationContext.HasNotifications) return null;

            var moneyOrder = MoneyOrder.Create(order.Value, order.Uid);
            moneyOrder.SetQrCodeBytes(QrCodeManagement.GenerateImage(moneyOrder.QRCode));

            _notificationContext.AddNotification(order);

            if (_notificationContext.HasNotifications) return null;

            await _orderRepository.UpdateAsync(order);
            await _moneyOrderRepository.DeleteByOrderUidAsync(order.Uid);
            await _moneyOrderRepository.AddAsync(moneyOrder);

            return OrderUtil.CreateResponse(order, items!, moneyOrder);
        }
    }
}
