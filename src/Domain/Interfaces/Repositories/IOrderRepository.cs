using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid uid);
        Task<Order?> GetByUidAsync(Guid uid);
        Task<IEnumerable<Order>?> GetAllAsync();
        Task<string> GetItemsSerializedAsync(Guid uid);
        Task<string?> GetCustomerSerializedAsync(Guid uid);

    }
}
