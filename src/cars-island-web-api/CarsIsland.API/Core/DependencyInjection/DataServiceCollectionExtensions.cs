using Azure.Cosmos;
using CarsIsland.Core.Entities;
using CarsIsland.Core.Interfaces;
using CarsIsland.Infrastructure.Configuration.Interfaces;
using CarsIsland.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CarsIsland.API.Core.DependencyInjection;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        var cosmoDbConfiguration = serviceProvider.GetRequiredService<ICosmosDbConfiguration>();

        var cosmosClient = new CosmosClient(cosmoDbConfiguration.ConnectionString);

        CosmosDatabase database = cosmosClient
            .CreateDatabaseIfNotExistsAsync(cosmoDbConfiguration.DatabaseName)
            .GetAwaiter()
            .GetResult();

        database.CreateContainerIfNotExistsAsync(
            id: cosmoDbConfiguration.CarContainerName,
            cosmoDbConfiguration.PartitionKeyPath,
            throughput: 400).GetAwaiter().GetResult();

        database.CreateContainerIfNotExistsAsync(
            id: cosmoDbConfiguration.EnquiryContainerName,
            cosmoDbConfiguration.PartitionKeyPath,
            throughput: 400).GetAwaiter().GetResult();

        services.AddSingleton(cosmosClient);

        services.AddSingleton<IDataRepository<Car>, CarRepository>();
        services.AddSingleton<IDataRepository<Enquiry>, EnquiryRepository>();

        return services;
    }
}