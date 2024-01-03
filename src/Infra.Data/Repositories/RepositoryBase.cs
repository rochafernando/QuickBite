using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace Infra.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
    {
        protected readonly string _connectionString;

        public RepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(string query, object param)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task DeleteAsync(string query, object param)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(query);
            }
        }

        public async Task<T?> GetByIdAsync(string query, object param)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(query, param);
            }
        }

        public async Task UpdateAsync(string query, object param)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, param);
            }
        }
    }
}
