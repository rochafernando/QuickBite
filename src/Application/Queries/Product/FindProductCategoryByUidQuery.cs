using System.Diagnostics.CodeAnalysis;

namespace Application.Queries.Product
{
    [ExcludeFromCodeCoverage]
    public class FindProductCategoryByUidQuery : Query
    {
        /// <summary>
        /// Uid da categoria.
        /// </summary>
        public string Uid { get; set; } = string.Empty;
    }
}
