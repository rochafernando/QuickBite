using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Order
{
    public class ItemFromOrderCommand
    {
        /// <summary>
        /// Uid do produto
        /// </summary>
        [Required]
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Quantidade do produto
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}
