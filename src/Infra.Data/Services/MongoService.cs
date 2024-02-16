using Domain.Interfaces.Configurations;
using MongoDB.Driver;

namespace Infra.Data.Services
{
    public class MongoService
    {
        public IMongoDatabase Database { get; }

        public MongoService(IDatabaseConfiguration databaseConfiguration)
        {
            try
            {
                var client = new MongoClient(databaseConfiguration.ConnectionString);
                Database = client.GetDatabase(databaseConfiguration.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new MongoException("", ex);
            }
        }
    }
}
