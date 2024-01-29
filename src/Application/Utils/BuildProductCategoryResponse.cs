using Application.Responses.Product;
using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Application.Utils
{
    [ExcludeFromCodeCoverage]
    public static class BuildProductCategoryResponse
    {
        public static ProductCategoryResponse Create(ProductCategory productCategory)
        {
            return new ProductCategoryResponse 
            { 
                Uid = productCategory.Uid, 
                Name = productCategory.Name, 
                Description = productCategory.Description 
            };
        }
    }
}
