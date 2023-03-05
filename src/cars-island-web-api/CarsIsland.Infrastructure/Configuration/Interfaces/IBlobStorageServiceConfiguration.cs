namespace CarsIsland.Infrastructure.Configuration.Interfaces;

public interface IBlobStorageServiceConfiguration
{
    string ContainerName { get; }
    string ConnectionString { get; }
    string Key { get; }
    string AccountName { get; }
}