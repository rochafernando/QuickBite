namespace Application.Commands.Order
{
    public class ItemFromOrderCommand
    {
        /// <summary>
        /// Uid do produto
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Quantidade do produto
        /// </summary>
        public int Quantity { get; set; }
    }
}
