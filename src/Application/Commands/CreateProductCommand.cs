namespace Application.Commands
{
    public class CreateProductCommand : Command
    {
        /// <summary>
        /// Nome do Produto.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
