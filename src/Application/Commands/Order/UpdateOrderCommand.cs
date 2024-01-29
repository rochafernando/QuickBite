using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Order
{
    public class UpdateOrderCommand : Command
    {
        /// <summary>
        /// Uid do pedido
        /// </summary>
        [Required]
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// Cliente do pedido (pode ser nulo).
        /// </summary>
        public CustomerFromOrderCommand? Customer { get; set; }

        /// <summary>
        /// A lista de produtos selecionados.
        /// </summary>
        [Required]
        public IEnumerable<ItemFromOrderCommand> Items { get; set; } = new List<ItemFromOrderCommand>();

        /// <summary>
        /// Status do pedido
        /// </summary>
        [Required]
        public OrderStatus Status { get; set; }
    }
}
