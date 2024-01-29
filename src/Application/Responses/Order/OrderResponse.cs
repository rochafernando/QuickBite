using Application.Responses.Customer;
using Application.Responses.MoneyOrder;
using Domain.Enums;
using Domain.Interfaces.CQS;

namespace Application.Responses.Order
{
    public class OrderResponse : IResult
    {
        /// <summary>
        /// Uid do pedido.
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// Cliente do pedido.
        /// </summary>
        public CustomerResponse? Customer { get; set; }

        /// <summary>
        /// Itens do pedido.
        /// </summary>
        public IEnumerable<ItemResponse> Items { get; set; } = new List<ItemResponse>();

        /// <summary>
        /// Status do pedido.
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Valor do pedido.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Ordem de pagamento do pedido.
        /// </summary>
        public MoneyOrderResponse? MoneyOrder { get; set; }
    }
}
