using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Services;
using MongoDB.Driver;

namespace Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderRepository(MongoService mongoService)
        {
            _orderCollection = mongoService.Database.GetCollection<Order>("Orders");
        }

        public async Task AddAsync(Order order)
        {
            await _orderCollection.InsertOneAsync(order);
        }

        public async Task DeleteAsync(Guid uid)
        {
            await _orderCollection.DeleteOneAsync(x => x.Uid == uid);
        }

        public async Task<IEnumerable<Order>?> GetAllAsync()
        {
            return await _orderCollection.Find(x => true).ToListAsync();
        }

        public async Task<Order?> GetByUidAsync(Guid uid)
        {
            return await _orderCollection.Find(x => x.Uid == uid).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _orderCollection.ReplaceOneAsync(x => x.Uid == order.Uid, order);
        }
    }
}
