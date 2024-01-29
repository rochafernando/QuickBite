using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Product
{
    public class CreateProductCategoryCommand : Command
    {
        /// <summary>
        /// Nome da categoria
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da categoria
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
