using Domain.Interfaces.Configurations;

namespace Domain.Configurations
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
