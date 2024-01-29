using Domain.Interfaces.CQS;
using System.Diagnostics.CodeAnalysis;

namespace Application.Responses.Product
{
    [ExcludeFromCodeCoverage]
    public class ProductResponse : IResult
    {
        /// <summary>
        /// Uid do produto
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// Categoria do produto
        /// </summary>
        public ProductCategoryResponse? Category { get; set; }

        /// <summary>
        /// Características do produto
        /// </summary>
        public ProductCharacteristicsResponse? Characteristics { get; set; }
        
        /// <summary>
        /// Preço do produto
        /// </summary>
        public PriceCompositionResponse? PriceComposition { get; set; }
    }
}
