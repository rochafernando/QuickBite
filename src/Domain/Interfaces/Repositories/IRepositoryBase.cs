using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : Entity
    {
        Task AddAsync(string query, object param);
        Task UpdateAsync(string query, object param);
        Task DeleteAsync(string query, object param);
        Task<T?> GetByIdAsync(string query, object param);
        Task<IEnumerable<T>> GetAllAsync(string query);
    }
}
