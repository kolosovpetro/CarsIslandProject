namespace CarsIsland.Infrastructure.Configuration.Interfaces;

public interface ICosmosDbConfiguration
{
    public string ConnectionString { get; }
    public string DatabaseName { get; }
    public string CarContainerName { get; }
    public string EnquiryContainerName { get; }
    public string PartitionKeyPath { get; }
}