using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer client);
        Task UpdateAsync(Customer client);
        Task DeleteAsync(Guid uid);
        Task<Customer?> GetByUidAsync(Guid uid);
        Task<IEnumerable<Customer>?> GetAllAsync();

    }
}
