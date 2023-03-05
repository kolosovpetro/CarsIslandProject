using CarsIsland.Infrastructure.Configuration.Interfaces;

namespace CarsIsland.Infrastructure.Configuration;

public class BlobStorageServiceConfiguration : IBlobStorageServiceConfiguration
{
    public string ContainerName { get; }
    public string ConnectionString { get; }
    public string Key { get; }
    public string AccountName { get; }

    public BlobStorageServiceConfiguration(string containerName, string connectionString, string key,
        string accountName)
    {
        ContainerName = containerName;
        ConnectionString = connectionString;
        Key = key;
        AccountName = accountName;
    }
}