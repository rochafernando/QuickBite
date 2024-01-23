using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProductCategoryRepository
    {
        Task AddAsync(ProductCategory productCategory);
        Task UpdateAsync(ProductCategory productCategory);
        Task DeleteAsync(Guid uid);
        Task<IEnumerable<ProductCategory>?> GetAllAsync();
        Task<ProductCategory?> GetByUidAsync(Guid uid);
    }
}
