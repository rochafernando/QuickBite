using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Services;
using MongoDB.Driver;

namespace Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerRepository(MongoService mongoService)
        {
            _customerCollection = mongoService.Database.GetCollection<Customer>(nameof(Customer));
        }

        public async Task AddAsync(Customer client)
        {
            await _customerCollection.InsertOneAsync(client);
        }

        public async Task DeleteAsync(Guid uid)
        {
            await _customerCollection.DeleteOneAsync(x => x.Uid == uid);
        }

        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            return await _customerCollection.Find(x => true).ToListAsync();
        }

        public async Task<Customer?> GetByUidAsync(Guid uid)
        {
            return await _customerCollection.Find(x => x.Uid == uid).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Customer client)
        {
            await _customerCollection.ReplaceOneAsync(x => x.Uid == client.Uid, client);
        }
    }
}
