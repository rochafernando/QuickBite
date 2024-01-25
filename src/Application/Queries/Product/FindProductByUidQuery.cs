using System.Diagnostics.CodeAnalysis;

namespace Application.Queries.Product
{
    [ExcludeFromCodeCoverage]
    public class FindProductByUidQuery : Query
    {
        /// <summary>
        /// Uid do produto.
        /// </summary>
        public string Uid { get; set; } = string.Empty;
    }
}
