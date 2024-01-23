using Domain.Interfaces.CQS;

namespace Application.Responses.Product
{
    public class ProductCategoryResponse : IResult
    {
        /// <summary>
        /// Uid da categoria
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// Nome da categoria
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da categoria
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
