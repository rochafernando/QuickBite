using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Application.Commands.Customer
{
    [ExcludeFromCodeCoverage]
    public class DeleteCustomerCommand : Command
    {
        /// <summary>
        /// Uid do cliente.
        /// </summary>
        public string Uid { get; set; } = string.Empty;
    }
}
