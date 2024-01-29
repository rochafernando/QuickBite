using System.Diagnostics.CodeAnalysis;

namespace Application.Queries.Order
{
    [ExcludeFromCodeCoverage]
    public class FindOrderByUidQuery : Query
    {
        /// <summary>
        /// Uid do pedido.
        /// </summary>
        public string Uid { get; set; } = string.Empty;
    }
}
