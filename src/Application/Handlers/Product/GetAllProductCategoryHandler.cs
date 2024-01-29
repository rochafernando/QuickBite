using Application.Responses.Product;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Product
{
    public class GetAllProductCategoryHandler : IQueryHandler<IEnumerable<ProductCategoryResponse>>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public GetAllProductCategoryHandler(
            IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<IEnumerable<ProductCategoryResponse>?> HandleAsync()
        {
            var productCategoryList = await _productCategoryRepository.GetAllAsync();

            return productCategoryList?.Select(x => new ProductCategoryResponse 
            { 
                Uid = x.Uid, Name = x.Name, Description = x.Description,
            }).ToList();
        }
    }
}
