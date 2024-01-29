using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace Infra.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly string _connectionString;

        public ProductCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(ProductCategory productCategory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    insert into Category (Uid, Name, Description, CreatedAt) values (@Uid, @Name, @Description, GETDATE())";

                var param = new { productCategory.Uid, productCategory.Name, productCategory.Description };

                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task DeleteAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    delete from Category where Uid = @uid";

                var param = new { uid };

                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task<IEnumerable<ProductCategory>?> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select
                        Id
                        ,Uid
                        ,Name
                        ,Description
                    from Category with(nolock)"
                ;

                return await connection.QueryAsync<ProductCategory>(query);
            }
        }

        public async Task<ProductCategory?> GetByUidAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select
                        Id
                        ,Uid
                        ,Name
                        ,Description
                    from Category with(nolock) 
                    where Uid = @uid"
                ;

                var param = new { uid };

                return await connection.QueryFirstOrDefaultAsync<ProductCategory>(query, param);
            }
        }

        public async Task UpdateAsync(ProductCategory productCategory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    update Category
                        set Name = @Name
                            ,Description = @Description
                            ,UpdatedAt = GETDATE()
                    where Uid = @Uid"
                ;

                var param = new { productCategory.Uid, productCategory.Name, productCategory.Description };

                await connection.ExecuteAsync(query, param);
            }
        }
    }
}
