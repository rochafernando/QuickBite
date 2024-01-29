using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid uid);
        Task<Product?> GetByUidAsync(Guid uid);
        Task<IEnumerable<Product>?> GetAllAsync();
    }
}
