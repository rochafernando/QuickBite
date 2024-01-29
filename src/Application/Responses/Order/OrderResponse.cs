using Application.Responses.Customer;
using Domain.Enums;
using Domain.Interfaces.CQS;

namespace Application.Responses.Order
{
    public class OrderResponse : IResult
    {
        public Guid Uid { get; set; }

        public CustomerResponse? Customer { get; set; }

        public IEnumerable<ItemResponse> Items { get; set; } = new List<ItemResponse>();

        public OrderStatus Status { get; set; }

        public decimal Value { get; set; }
    }
}
