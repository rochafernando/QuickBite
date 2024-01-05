using System.Diagnostics.CodeAnalysis;

namespace Application.Commands.Customer
{
    [ExcludeFromCodeCoverage]
    public class UpdateCustomerCommand : Command
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Documento do cliente.
        /// </summary>
        public string Document { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do cliente.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
