using Domain.Interfaces.CQS;
using System.Diagnostics.CodeAnalysis;

namespace Application.Responses.Customer
{
    [ExcludeFromCodeCoverage]
    public class CustomerResponse : IResult
    {
        /// <summary>
        /// Uid do Cliente
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// Nome do Cliente
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Documento do cliente.
        /// </summary>
        public string Document { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do Cliente
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
