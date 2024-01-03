using System.Diagnostics.CodeAnalysis;

namespace Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}
