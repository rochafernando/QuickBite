using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task AddAsync(Customer client)
        {
            var query = @"
            insert into Client (Uid, Name, Document, Email, CreationDate) values (@Uid, @Name, @Document, @Email, GETDATE())";

            var param = new { client.Uid, client.Name, client.Document, client.Email };

            await base.AddAsync(query, param);
        }

        public Task DeleteAsync(Customer client)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer client)
        {
            throw new NotImplementedException();
        }
    }
}
