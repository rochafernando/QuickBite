using System.Diagnostics.CodeAnalysis;

namespace Application.Commands.Order
{
    [ExcludeFromCodeCoverage]
    public class CustomerFromOrderCommand
    {
        /// <summary>
        /// Uid do Cliente
        /// </summary>
        public string Uid { get; set; } = string.Empty;
    }
}
