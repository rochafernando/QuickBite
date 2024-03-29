﻿using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Order
{
    public class CreateOrderCommand : Command
    {
        /// <summary>
        /// Cliente do pedido (pode ser nulo).
        /// </summary>
        public CustomerFromOrderCommand? Customer { get; set; }

        /// <summary>
        /// A lista de produtos selecionados.
        /// </summary>
        [Required]
        public IEnumerable<ItemFromOrderCommand> Items { get; set; } = new List<ItemFromOrderCommand>();
    }
}
