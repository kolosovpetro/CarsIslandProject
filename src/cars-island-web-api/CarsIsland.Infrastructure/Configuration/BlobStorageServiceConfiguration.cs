using CarsIsland.Infrastructure.Configuration.Interfaces;

namespace CarsIsland.Infrastructure.Configuration;

public class BlobStorageServiceConfiguration : IBlobStorageServiceConfiguration
{
    public string ContainerName { get; set; }
    public string ConnectionString { get; set; }
    public string Key { get; set; }
    public string AccountName { get; set; }
}