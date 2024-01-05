using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer client);
        Task UpdateAsync(Customer client);
        Task DeleteAsync(Customer client);
        Task<Customer?> GetByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
