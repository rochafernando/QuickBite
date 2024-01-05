using Domain.Interfaces.CQS;
using System.Diagnostics.CodeAnalysis;

namespace Application.Responses.Customer
{
    [ExcludeFromCodeCoverage]
    public class CustomerResponse : IResult
    {
        /// <summary>
        /// Id do Cliente
        /// </summary>
        public int Id { get; set; }

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
