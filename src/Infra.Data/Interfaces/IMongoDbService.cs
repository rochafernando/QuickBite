using MongoDB.Driver;

namespace Infra.Data.Interfaces
{
    public interface IMongoDbService
    {
        public IMongoDatabase Database { get; }
    }
}