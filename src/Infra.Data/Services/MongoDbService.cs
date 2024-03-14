using Infra.Data.Configurations;
using Infra.Data.Interfaces;
using MongoDB.Driver;

namespace Infra.Data.Services
{
    public sealed class MongoDbService : IMongoDbService
    {
        private const string ExceptionErrorMessage = "Erro para se conectar no MongoDb";

        public IMongoDatabase Database { get; }

        public MongoDbService(MongoDbConfiguration databaseConfiguration)
        {
            try
            {
                var client = new MongoClient(databaseConfiguration.ConnectionString);
                Database = client.GetDatabase(databaseConfiguration.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new MongoException(ExceptionErrorMessage, ex);
            }
        }
    }
}
