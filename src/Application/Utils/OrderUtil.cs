using Application.Commands.Order;
using Application.Responses.Order;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Domain.ValuesObjects;
using System.Diagnostics.CodeAnalysis;

namespace Application.Utils
{
    [ExcludeFromCodeCoverage]
    public class OrderUtil
    {
        private readonly IProductRepository _productRepository;
        private readonly NotificationContext _notificationContext;
        private readonly ICustomerRepository _customerRepository;

        public OrderUtil(
            IProductRepository productRepository,
            NotificationContext notificationContext,
            ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _notificationContext = notificationContext;
            _customerRepository = customerRepository;
        }

        public async Task<List<Item>?> CreateItemsFromOrder(List<ItemFromOrderCommand> items)
        {
            var result = new List<Item>();

            foreach (var item in items)
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

        public async Task<Customer?> CreateCustomerFromOrder(CustomerFromOrderCommand? customer)
        {
            if (customer == null) return null;

            return await _customerRepository.GetByUidAsync(Guid.Parse(customer.Uid));
        }

        public static OrderResponse CreateResponse(Order order, List<Item> items)
        {
            return new OrderResponse
            {
                Uid = order.Uid,
                Value = order.Value,
                Status = Domain.Enums.OrderStatus.PendingPayment,
                Items = items.Select(i => new ItemResponse
                {
                    Product = BuildProductResponse.CreateRespose(i.Product!),
                    Quantity = i.Quantity
                }),
                Customer = BuildCustomerResponse.Create(order.Customer)
            };
        }

        public static OrderResponse CreateResponse(Order order, List<ItemResponse> items)
        {
            return new OrderResponse
            {
                Uid = order.Uid,
                Value = order.Value,
                Status = Domain.Enums.OrderStatus.PendingPayment,
                Items = items,
                Customer = BuildCustomerResponse.Create(order.Customer)
            };
        }
    }
}
