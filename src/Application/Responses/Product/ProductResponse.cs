using Domain.Interfaces.CQS;
using System.Diagnostics.CodeAnalysis;

namespace Application.Responses.Product
{
    [ExcludeFromCodeCoverage]
    public class ProductResponse : IResult
    {
        public Guid Uid { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
