using System.Diagnostics.CodeAnalysis;

namespace Application.Commands.Customer
{
    [ExcludeFromCodeCoverage]
    public class DeleteCustomerCommand : Command
    {
        /// <summary>
        /// Id do cliente.
        /// </summary>
        public Guid Id { get; set; }
    }
}
