namespace Application.Commands.Product
{
    public class DeleteProductCategoryCommand : Command
    {
        /// <summary>
        /// Uid da categoria.
        /// </summary>
        public string Uid { get; set; } = string.Empty;
    }
}
