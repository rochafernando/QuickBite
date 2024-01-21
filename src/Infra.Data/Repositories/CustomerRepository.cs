﻿using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(Customer client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    insert into Client (Uid, Name, Document, Email, CreationDate) values (@Uid, @Name, @Document, @Email, GETDATE())";

                var param = new { client.Uid, client.Name, client.Document, client.Email };

                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task DeleteAsync(Customer client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    delete from Client where Uid = @Uid";

                var param = new { client.Uid };

                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task<Customer?> GetByDocumentAsync(string document)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select
                        Id
                        ,Uid
                        ,Name
                        ,Document
                        ,Email
                    from Client with(nolock) 
                    where Document = @document";

                var param = new { document };

                return await connection.QueryFirstOrDefaultAsync(query, param);
            }
        }

        public async Task<Customer?> GetByUidAsync(Guid uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    select
                        Id
                        ,Uid
                        ,Name
                        ,Document
                        ,Email
                    from Client with(nolock) 
                    where Uid = @uid"
                ;

                var param = new { uid };

                return await connection.QueryFirstOrDefaultAsync<Customer>(query, param);
            }
        }

        public async Task UpdateAsync(Customer client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    update Client
                        set Name = @Name
                            ,Document = @Document
                            ,Email = @Email
                    where Uid = @Uid"
                ;

                var param = new { client.Uid, client.Name, client.Document, client.Email };

                await connection.ExecuteAsync(query, param);
            }
        }
    }
}