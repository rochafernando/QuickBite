namespace Application.Commands.Product
{
    public class UpdateProductCommand : Command
    {
        /// <summary>
        /// Uid do produto
        /// </summary>
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// Uid da categoria
        /// </summary>
        public string CategoryUid { get; set; } = string.Empty;

        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do produto
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Imagem do produto
        /// </summary>
        public string PathImage { get; set; } = string.Empty;

        /// <summary>
        /// Diz se o produto é apto para ser vendido ou não
        /// </summary>
        public bool Sellable { get; set; }

        /// <summary>
        /// Diz se o produto está ativo ou não
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Preço unitário do produto
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Preço do produto no combo
        /// </summary>
        public decimal ComboPrice { get; set; }
    }
}
