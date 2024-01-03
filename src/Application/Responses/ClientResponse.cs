using Domain.Interfaces.CQS;
using System.Diagnostics.CodeAnalysis;

namespace Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class ClientResponse : IResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
