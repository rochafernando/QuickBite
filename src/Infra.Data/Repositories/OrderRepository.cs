using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    insert into Orders (Uid, Customer, Items, Status, Value, CreatedAt) 
                    values (@Uid, @Customer, @Items, @Status, @Value, GETDATE())";

                var param = new
                {
                    order.Uid,
                    Customer = (order.Customer is null) ? null : JsonConvert.SerializeObject(order.Customer),
                    Items = (order.Items.Any()) ? JsonConvert.SerializeObject(order.Items) : null,
                    order.Value,
                    order.Status
                };

                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task DeleteAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    delete from Orders where Uid = @uid";

                var param = new { uid };

                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task<IEnumerable<Order>?> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select 
                        o.Id
                        ,o.Uid
                        ,o.Status
                        ,o.Value
                    from Orders o with(nolock) 
                    order by 1 desc;";

                return await connection.QueryAsync<Order>(query);
            }
        }

        public async Task<Order?> GetByUidAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select 
                        o.Id
                        ,o.Uid
                        ,o.Status
                        ,o.Value
                        ,o.CreatedAt
                        ,o.UpdatedAt
                    from Orders o with(nolock) 
                    where Uid = @uid;";

                var param = new { uid };

                return await connection.QueryFirstOrDefaultAsync<Order>(query, param);
            }
        }

        public async Task<string> GetItemsSerializedAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select 
                        o.Items
                    from Orders o with(nolock) 
                    where Uid = @uid;";

                var param = new { uid };

                return await connection.QueryFirstAsync<string>(query, param);
            }
        }

        public async Task UpdateAsync(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    update Orders
                        set Customer = @Customer
                        ,Items = @Items
                        ,Status = @Status
                        ,Value = @Value
                        ,UpdatedAt = GETDATE()
                    where Uid = @Uid"
                ;

                var param = new
                {
                    order.Uid,
                    Customer = (order.Customer is null) ? null : JsonConvert.SerializeObject(order.Customer),
                    Items = (order.Items.Any()) ? JsonConvert.SerializeObject(order.Items) : null,
                    order.Value,
                    order.Status
                };

                await connection.ExecuteAsync(query, param);
            }
        }
    }
}
