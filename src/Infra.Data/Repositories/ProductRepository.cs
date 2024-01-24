using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    insert into Product (Uid, CategoryUid, Name, Description, Sellable, Enable, UnitPrice, ComboPrice, CreatedAt) 
                    values (@Uid, @CategoryUid, @Name, @Description, @Sellable, @Enable, @UnitPrice, @ComboPrice, GETDATE())";

                var param = new 
                { 
                    product.Uid,
                    CategoryUid = product.Category!.Uid, 
                    product.Characteristics!.Name, 
                    product.Characteristics!.Description,
                    product.Sellable,
                    product.Enable,
                    product.PriceComposition!.UnitPrice,
                    product.PriceComposition!.ComboPrice,
                };

                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task DeleteAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    delete from Product where Uid = @uid";

                var param = new { uid };

                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task<IEnumerable<Product>?> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select
                        Id
                        ,Uid
                        ,CategoryUid
                        ,Sellable
                        ,Enable
                    from Product with(nolock);
                    select
                        Id
                        ,Uid
                        ,Name
                        ,Description
                    from Category with(nolock);"
                ;

                var result = await connection.QueryMultipleAsync(query);
                var product = result.Read<Product>();
                var category = result.Read<ProductCategory>();

                return new List<Product>();
            }
        }

        public Task<Product?> GetByUidAsync(Guid uid)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    update Product
                        set CategoryUid = @CategoryUid
                        ,Name = @Name
                        ,Description = @Description
                        ,Sellable = @Sellable
                        ,Enable = @Enable
                        ,UnitPrice = @UnitPrice
                        ,ComboPrice = @ComboPrice
                        ,UpdatedAt = GETDATE()
                    where Uid = @Uid"
                ;

                var param = new
                {
                    product.Uid,
                    CategoryUid = product.Category!.Uid,
                    product.Characteristics!.Name,
                    product.Characteristics!.Description,
                    product.Sellable,
                    product.Enable,
                    product.PriceComposition!.UnitPrice,
                    product.PriceComposition!.ComboPrice,
                };

                await connection.ExecuteAsync(query, param);
            }
        }
    }
}
