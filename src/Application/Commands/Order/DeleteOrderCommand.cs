using System.Diagnostics.CodeAnalysis;

namespace Application.Commands.Order
{
    [ExcludeFromCodeCoverage]
    public class DeleteOrderCommand : Command
    {
        /// <summary>
        /// Uid do pedido.
        /// </summary>
        public string Uid { get; set; } = string.Empty;
    }
}
