using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Product
{
    public class CreateProductCommand : Command
    {
        /// <summary>
        /// Uid da categoria
        /// </summary>
        [Required]
        public string CategoryUid { get; set; } = string.Empty;

        /// <summary>
        /// Nome do produto
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do produto
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Imagem do produto
        /// </summary>
        public string PathImage { get; set; } = string.Empty;

        /// <summary>
        /// Diz se o produto é apto para ser vendido ou não
        /// </summary>
        [Required]
        public bool Sellable { get; set; }

        /// <summary>
        /// Diz se o produto está ativo ou não
        /// </summary>
        [Required]
        public bool Enable { get; set; }

        /// <summary>
        /// Preço unitário do produto
        /// </summary>
        [Required]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Preço do produto no combo
        /// </summary>
        [Required]
        public decimal ComboPrice { get; set; }
    }
}
