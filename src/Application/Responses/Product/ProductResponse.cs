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
        /// Nome do produto
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
