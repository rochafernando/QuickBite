using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IMoneyOrderRepository
    {
        Task AddAsync(MoneyOrder moneyOrder);
        Task UpdateAsync(MoneyOrder moneyOrder);
        Task<MoneyOrder?> GetByUidAsync(Guid uid);
        Task<MoneyOrder?> GetByOrderUidAsync(Guid uid);
        Task DeleteByOrderUidAsync(Guid uid);
        Task<IEnumerable<MoneyOrder>?> GetAllAsync();
    }
}
