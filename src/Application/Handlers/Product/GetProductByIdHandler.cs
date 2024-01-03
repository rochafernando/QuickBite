using Application.Queries;
using Application.Responses;
using Domain.Interfaces.CQS;

namespace Application.Handlers.Product
{
    public class GetProductByIdHandler : IQueryHandler<FindProductByIdQuery, ProductResponse>
    {
        public async Task<ProductResponse> HandleAsync(FindProductByIdQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
