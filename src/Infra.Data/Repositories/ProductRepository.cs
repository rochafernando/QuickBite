using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.ValuesObjects;
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
                        ,Sellable
                        ,Enable
                    from Product with(nolock)
                    order by 1 desc;
                    select
                        Id
                        ,Name
                        ,Description
                    from Product with(nolock)
                    order by 1 desc;"
                ;

                var result = await connection.QueryMultipleAsync(query);
                var product = result.Read<Product>().ToList();
                var characteristics = result.Read<ProductCharacteristics>().ToList();

                for (int i = 0; i < product.Count(); i++)
                {
                    product[i].SetCharacteristics(characteristics[i]);
                }
                
                return product;
            }
        }

        public async Task<Product?> GetByUidAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select
                        Id
                        ,Uid
                        ,Sellable
                        ,Enable
                    from Product with(nolock)
                    where Uid = @uid;
                    select
                        CreatedAt
                    from Product with(nolock)
                    where Uid = @uid;
                    select
                        Id
                        ,Name
                        ,Description
                    from Product with(nolock)
                    where Uid = @uid;"
                ;

                var param = new { uid }; 
                
                var result = await connection.QueryMultipleAsync(query, param);
                
                var product = result.ReadFirstOrDefault<Product>();
                var auditDate = result.ReadFirstOrDefault<AuditDate>();
                var characteristics = result.ReadFirstOrDefault<ProductCharacteristics>();

                product?.SetAuditDate(auditDate);
                product?.SetCharacteristics(characteristics);

                return product;
            }
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
