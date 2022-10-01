using CarsIsland.Infrastructure.Configuration.Interfaces;

namespace CarsIsland.Infrastructure.Configuration;

public class CosmosDbConfiguration : ICosmosDbConfiguration
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CarContainerName { get; set; }
    public string EnquiryContainerName { get; set; }
    public string PartitionKeyPath { get; set; }
}