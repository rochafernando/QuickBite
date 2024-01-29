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
                var query1 = @"
                    select
                        p.Id
                        ,p.Uid
                        ,p.Sellable
                        ,p.Enable
                        ,c.Id
                        ,c.Uid
                        ,c.Name
                        ,c.Description
                    from Product p with(nolock)
                    inner join Category c with(nolock) 
                        on c.Uid = p.CategoryUid
                    order by 1 desc;
                ";

                var product = (await connection.QueryAsync<Product, ProductCategory, Product>(query1, (p, c) =>
                {
                    p.SetCategory(c);
                    return p;

                }, splitOn: "Id")).ToList();

                var query2 = @"
                    select
                        Id
                        ,Name
                        ,Description
                    from Product with(nolock)
                    order by 1 desc;
                    select
                        Id
                        ,UnitPrice
                        ,ComboPrice
                    from Product with(nolock)
                    order by 1 desc;"
                ;

                var result = await connection.QueryMultipleAsync(query2);
                var characteristics = result.Read<ProductCharacteristics>().ToList();
                var priceComposition = result.Read<PriceComposition>().ToList();

                for (int i = 0; i < product.Count(); i++)
                {
                    product[i].SetCharacteristics(characteristics[i]);
                    product[i].SetPriceComposition(priceComposition[i]);
                }
                
                return product;
            }
        }

        public async Task<Product?> GetByUidAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var param = new { uid };

                var query1 = @"
                    select
                        p.Id
                        ,p.Uid
                        ,p.Sellable
                        ,p.Enable
                        ,c.Id
                        ,c.Uid
                        ,c.Name
                        ,c.Description
                    from Product p with(nolock)
                    inner join Category c with(nolock) 
                        on c.Uid = p.CategoryUid
                    where p.Uid = @uid;
                ";

                var product = (await connection.QueryAsync<Product, ProductCategory, Product>(query1, (p, c) =>
                {
                    p.SetCategory(c);
                    return p;

                }, param, splitOn: "Id")).FirstOrDefault();

                var query2 = @"
                    select
                        Id
                        ,Name
                        ,Description
                    from Product with(nolock)
                    where Uid = @uid;
                    select
                        Id
                        ,UnitPrice
                        ,ComboPrice
                    from Product with(nolock)
                    where Uid = @uid;"
                ;
                
                var result = await connection.QueryMultipleAsync(query2, param);
                var characteristics = result.ReadFirstOrDefault<ProductCharacteristics>();
                var priceComposition = result.ReadFirst<PriceComposition>();

                product?.SetCharacteristics(characteristics);
                product?.SetPriceComposition(priceComposition);

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
