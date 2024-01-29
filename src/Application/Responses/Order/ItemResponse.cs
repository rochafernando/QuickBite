using Application.Responses.Product;
using Newtonsoft.Json;

namespace Application.Responses.Order
{
    public class ItemResponse
    {
        /// <summary>
        /// O produto
        /// </summary>
        public ProductResponse? Product { get; set; }

        /// <summary>
        /// A quantidade do produto selecionado
        /// </summary>
        public int Quantity { get; set; }
    }
}
