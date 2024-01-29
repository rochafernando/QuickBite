using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace Infra.Data.Repositories
{
    public class MoneyOrderRepository : IMoneyOrderRepository
    {
        private readonly string _connectionString;

        public MoneyOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(MoneyOrder moneyOrder)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    insert into MoneyOrder (Uid, OrderUid, TxId, QRCode, Status, Value, CreatedAt) 
                    values (@Uid, @OrderUid, @TxId, @QRCode, @Status, @Value, GETDATE())";

                var param = new
                {
                    moneyOrder.Uid,
                    moneyOrder.OrderUid,
                    moneyOrder.TxId,
                    moneyOrder.QRCode,
                    moneyOrder.Value,
                    moneyOrder.Status
                };

                await connection.ExecuteAsync(query, param);
            }
        }

        public Task<IEnumerable<Order>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<MoneyOrder?> GetByOrderUidAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select 
                        mo.Id
                        ,mo.Uid
                        ,mo.OrderUid
                        ,mo.TxId
                        ,mo.QRCode
                        ,mo.Status
                        ,mo.Value
                        ,mo.CreatedAt
                        ,mo.UpdatedAt
                    from MoneyOrder mo with(nolock) 
                    where OrderUid = @uid;";

                var param = new { uid };

                return await connection.QueryFirstOrDefaultAsync<MoneyOrder>(query, param);
            }
        }

        public async Task<MoneyOrder?> GetByUidAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select 
                        mo.Id
                        ,mo.Uid
                        ,mo.OrderUid
                        ,mo.TxId
                        ,mo.QRCode
                        ,mo.Status
                        ,mo.Value
                        ,mo.CreatedAt
                        ,mo.UpdatedAt
                    from MoneyOrder mo with(nolock) 
                    where Uid = @uid;";

                var param = new { uid };

                return await connection.QueryFirstOrDefaultAsync<MoneyOrder>(query, param);
            }
        }

        public async Task DeleteByOrderUidAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    delete from MoneyOrder 
                    where OrderUid = @uid;";

                var param = new { uid };

                await connection.ExecuteAsync(query, param);
            }
        }

        public Task UpdateAsync(MoneyOrder moneyOrder)
        {
            throw new NotImplementedException();
        }
    }
}
