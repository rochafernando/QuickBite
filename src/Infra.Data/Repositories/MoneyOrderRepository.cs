using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Services;
using MongoDB.Driver;

namespace Infra.Data.Repositories
{
    public class MoneyOrderRepository : IMoneyOrderRepository
    {
        private readonly IMongoCollection<MoneyOrder> _moneyOrderCollection;

        public MoneyOrderRepository(MongoService mongoService)
        {
            _moneyOrderCollection = mongoService.Database.GetCollection<MoneyOrder>(nameof(MoneyOrder));
        }

        public async Task AddAsync(MoneyOrder moneyOrder)
        {
            await _moneyOrderCollection.InsertOneAsync(moneyOrder);
        }

        public async Task<IEnumerable<MoneyOrder>?> GetAllAsync()
        {
            return await _moneyOrderCollection.Find(x => true).ToListAsync();
        }

        public async Task<MoneyOrder?> GetByOrderUidAsync(Guid uid)
        {
            return await _moneyOrderCollection.Find(x => x.OrderUid == uid).FirstOrDefaultAsync();
        }

        public async Task<MoneyOrder?> GetByUidAsync(Guid uid)
        {
            return await _moneyOrderCollection.Find(x => x.Uid == uid).FirstOrDefaultAsync();
        }

        public async Task DeleteByOrderUidAsync(Guid uid)
        {
            await _moneyOrderCollection.DeleteOneAsync(x => x.Uid == uid);
        }

        public async Task UpdateAsync(MoneyOrder moneyOrder)
        {
            await _moneyOrderCollection.ReplaceOneAsync(x => x.Uid == moneyOrder.Uid, moneyOrder);
        }
    }
}
