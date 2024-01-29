namespace Application.Responses.Product
{
    public class PriceCompositionResponse
    {
        /// <summary>
        /// Preço unitário do produto
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Preço do produto no combo
        /// </summary>
        public decimal ComboPrice { get; set; }
    }
}
