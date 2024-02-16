using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Services;
using MongoDB.Driver;

namespace Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepository(MongoService mongoService)
        {
            _productCollection = mongoService.Database.GetCollection<Product>(nameof(Product));
        }

        public async Task AddAsync(Product product)
        {
            await _productCollection.InsertOneAsync(product);
        }

        public async Task DeleteAsync(Guid uid)
        {
            await _productCollection.DeleteOneAsync(x => x.Uid == uid);
        }

        public async Task<IEnumerable<Product>?> GetAllAsync()
        {
            return await _productCollection.Find(x => true).ToListAsync();
        }

        public async Task<Product?> GetByUidAsync(Guid uid)
        {
            return await _productCollection.Find(x => x.Uid == uid).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            await _productCollection.ReplaceOneAsync(x => x.Uid == product.Uid, product);
        }
    }
}
