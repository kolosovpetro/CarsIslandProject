using CarsIsland.Infrastructure.Configuration.Interfaces;

namespace CarsIsland.Infrastructure.Configuration;

public class CosmosDbConfiguration : ICosmosDbConfiguration
{
    public string ConnectionString { get; }
    public string DatabaseName { get; }
    public string CarContainerName { get; }
    public string EnquiryContainerName { get; }
    public string PartitionKeyPath { get; }

    public CosmosDbConfiguration(string connectionString, string databaseName, string carContainerName,
        string enquiryContainerName, string partitionKeyPath)
    {
        ConnectionString = connectionString;
        DatabaseName = databaseName;
        CarContainerName = carContainerName;
        EnquiryContainerName = enquiryContainerName;
        PartitionKeyPath = partitionKeyPath;
    }
}