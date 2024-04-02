using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Product
{
    public class CreateProductCategoryCommand : Command
    {
        /// <summary>
        /// Nome da categoria
        /// </summary>
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "Pelo menos 3 caracteres")]
        [DefaultValue("Bebidas")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da categoria
        /// </summary>
        [Required(ErrorMessage = "Descrição é obrigatório.")]
        [DefaultValue("As melhores bebidas para beber.")]
        public string Description { get; set; } = string.Empty;
    }
}
