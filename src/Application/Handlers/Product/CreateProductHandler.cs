using Application.Commands;
using Application.Responses;
using Domain.Interfaces.CQS;

namespace Application.Handlers.Product
{
    public class CreateProductHandler : ICommandHandler<CreateProductCommand, ProductResponse>
    {
        public async Task<ProductResponse> HandleAsync(CreateProductCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
