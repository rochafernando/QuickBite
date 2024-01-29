using System.Text.Json.Serialization;

namespace Application.Commands.Product
{
    public class DeleteProductCommand : Command
    {
        /// <summary>
        /// Uid do produto.
        /// </summary>
        public string Uid { get; set; } = string.Empty;
    }
}
