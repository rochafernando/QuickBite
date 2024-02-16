namespace Domain.Interfaces.Configurations
{
    public interface IDatabaseConfiguration
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
