using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infra.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly IMongoCollection<ProductCategory> _productCategoryCollection;

        public ProductCategoryRepository(IMongoDbService mongoService)
        {
            _productCategoryCollection = mongoService.Database.GetCollection<ProductCategory>("Category");
        }

        public async Task AddAsync(ProductCategory productCategory)
        {
            await _productCategoryCollection.InsertOneAsync(productCategory);
        }

        public async Task DeleteAsync(Guid uid)
        {
            await _productCategoryCollection.DeleteOneAsync(x => x.Uid == uid);
        }

        public async Task<IEnumerable<ProductCategory>?> GetAllAsync()
        {
            return await _productCategoryCollection.Find(x => true).ToListAsync();
        }

        public async Task<ProductCategory?> GetByUidAsync(Guid uid)
        {
            return await _productCategoryCollection.Find(x => x.Uid == uid).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ProductCategory productCategory)
        {
            await _productCategoryCollection.ReplaceOneAsync(x => x.Uid == productCategory.Uid, productCategory);
        }
    }
}
