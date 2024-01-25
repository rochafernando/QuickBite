using Application.Responses.Product;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Product
{
    public class GetAllProductHandler : IQueryHandler<IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductHandler(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductResponse>?> HandleAsync()
        {
            var productList = await _productRepository.GetAllAsync();

            return productList?.Select(x => new ProductResponse
            {
                Uid = x.Uid,
                Name = x.Characteristics?.Name!,
                
            }).ToList();
        }
    }
}
