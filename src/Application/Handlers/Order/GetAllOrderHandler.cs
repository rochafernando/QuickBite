using Application.Responses.Order;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Order
{
    public class GetAllOrderHandler : IQueryHandler<IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrderHandler(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderResponse>?> HandleAsync()
        {
            var orderList = await _orderRepository.GetAllAsync();

            return orderList?.ToList().Select(o => new OrderResponse
            {
                Uid = o.Uid,
                Value = o.Value,
                Status = o.Status,
            }).ToList();
        }
    }
}
