namespace Application.Commands.Product
{
    public class UpdateProductCategoryCommand : Command
    {
        /// <summary>
        /// Uid da categoria
        /// </summary>
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// Nome da categoria
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da categoria
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
