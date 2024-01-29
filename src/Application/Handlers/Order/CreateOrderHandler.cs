using Application.Commands.Order;
using Application.Responses.Order;
using Application.Responses.Product;
using Application.Utils;
using Domain.Entities;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Domain.ValuesObjects;
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

        public CreateOrderHandler(
            ILogger<CreateOrderHandler> logger,
            NotificationContext notificationContext,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _logger = logger;
            _notificationContext = notificationContext;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
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

            var items = await BuildItemsFromOrder(command);

            if (items is null && _notificationContext.HasNotifications) return null;
            
            var order = Domain.Entities.Order.Create(null, items!);

            _notificationContext.AddNotification(order);

            if (_notificationContext.HasNotifications) return null;

            await _orderRepository.AddAsync(order);

            //var charge = new Cobranca(_chave: order.Uid.ToString())
            //{
            //    SolicitacaoPagador = $"Pagamento do Pedido {order.Uid}",
            //    Valor = new Valor
            //    {
            //        Original = order.Value.ToString()
            //    }
            //};

            //var payload = charge.ToPayload(GenerateTxId(), new Merchant("QuickBite", "São Paulo"));
            //var qrCodeString = payload.GenerateStringToQrCode();

            return OrderUtil.CreateResponse(order, items!);
        }

        private async Task<List<Item>?> BuildItemsFromOrder(CreateOrderCommand command)
        {
            var result = new List<Item>();

            foreach (var item in command.Items)
            {
                var product = await _productRepository.GetByUidAsync(Guid.Parse(item.Product));

                if (product is null)
                {
                    _notificationContext.AddNotification(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.ProductNotFound });
                    return null;
                }

                result.Add(Item.Create(product, item.Quantity));
            }

            return result;
        }

        private static string GenerateTxId()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //TXID deve ter no mínimo 26 caracteres e no máximo 35 caracteres.
            var txId = new char[35];
            var random = new Random();

            for (int i = 0; i < txId.Length; i++)
            {
                txId[i] = chars[random.Next(chars.Length)];
            }

            return new String(txId);
        }
    }
}
