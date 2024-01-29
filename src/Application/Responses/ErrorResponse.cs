using System.Diagnostics.CodeAnalysis;

namespace Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class ErrorResponse
    {
        /// <summary>
        /// Código do erro
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Título do erro
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Detalhe do erro
        /// </summary>
        public string Detail { get; set; } = string.Empty;
    }
}
