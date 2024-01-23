using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Application.Queries.Customer
{
    [ExcludeFromCodeCoverage]
    public class FindCustomerByUidQuery : Query
    {
        /// <summary>
        /// Uid do cliente.
        /// </summary>
        //[JsonPropertyName("uid")]
        public string Uid { get; set; } = string.Empty;
    }
}
