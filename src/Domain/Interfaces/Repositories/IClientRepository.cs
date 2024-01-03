using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);
        Task<Client?> GetByIdAsync(Guid id);
        Task<IEnumerable<Client>> GetAllAsync();
    }
}
