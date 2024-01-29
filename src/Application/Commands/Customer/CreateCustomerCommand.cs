using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.Commands.Customer
{
    [ExcludeFromCodeCoverage]
    public class CreateCustomerCommand : Command
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Documento do cliente.
        /// </summary>
        [Required]
        public string Document { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do cliente.
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
