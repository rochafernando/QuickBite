using Application.Responses.Product;
using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Application.Utils
{
    [ExcludeFromCodeCoverage]
    public static class BuildProductResponse
    {
        public static ProductResponse CreateRespose(Product product)
        {
            return new ProductResponse
            {
                Uid = product.Uid,
                Category = new ProductCategoryResponse
                {
                    Uid = product.Category!.Uid,
                    Name = product.Category!.Name,
                    Description = product.Category!.Description,
                },
                Characteristics = new ProductCharacteristicsResponse
                {
                    Name = product.Characteristics!.Name,
                    Description = product.Characteristics!.Description,
                    PathImage = product.Characteristics!.PathImage
                },
                PriceComposition = new PriceCompositionResponse
                {
                    UnitPrice = product.PriceComposition!.UnitPrice,
                    ComboPrice = product.PriceComposition!.ComboPrice,
                }
            };
        }
    }
}
