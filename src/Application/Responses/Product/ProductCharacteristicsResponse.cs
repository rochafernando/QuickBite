namespace Application.Responses.Product
{
    public class ProductCharacteristicsResponse
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do produto
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Caminho da imagem do produto
        /// </summary>
        public string PathImage { get; set; } = string.Empty;

    }
}
