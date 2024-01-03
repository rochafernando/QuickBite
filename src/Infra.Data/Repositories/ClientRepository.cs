using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Data.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task AddAsync(Client client)
        {
            var query = @"
            insert into Client (Id, Name, Email) values (@Id, @Name, @Email)";

            var param = new { client.Id, client.Name, client.Email };

            await base.AddAsync(query, param);
        }

        public Task DeleteAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Client?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
