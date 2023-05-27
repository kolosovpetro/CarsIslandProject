using CarsIsland.Infrastructure.Configuration;
using CarsIsland.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static CarsIsland.API.Constants.ConfigConstants;

namespace CarsIsland.API.DependencyInjection;

public static class ConfigurationServiceCollectionExtensions
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var blobAccountName = configuration[BlobAccountName];
        var blobKey = configuration[BlobKey];
        var blobContainerName = configuration[BlobContainerName];
        var blobConnectionString = configuration[BlobConnectionString];
        var blobSettings =
            new BlobStorageServiceConfiguration(blobContainerName, blobConnectionString, blobKey, blobAccountName);

        services.AddSingleton<IBlobStorageServiceConfiguration>(blobSettings);

        var cosmosConnectionString = configuration[CosmosConnectionString];
        var cosmosDatabaseName = configuration[CosmosDatabaseName];
        var cosmosCarContainerName = configuration[CosmosCarContainerName];
        var cosmosEnquiryContainerName = configuration[CosmosEnquiryContainerName];
        var cosmosPartitionKeyPath = configuration[CosmosPartitionKeyPath];

        var cosmosDbSettings = new CosmosDbConfiguration(cosmosConnectionString, cosmosDatabaseName,
            cosmosCarContainerName, cosmosEnquiryContainerName, cosmosPartitionKeyPath);

        services.AddSingleton<ICosmosDbConfiguration>(cosmosDbSettings);

        return services;
    }
}