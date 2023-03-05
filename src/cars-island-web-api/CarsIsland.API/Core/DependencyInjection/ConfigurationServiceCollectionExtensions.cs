using CarsIsland.API.Constants;
using CarsIsland.Infrastructure.Configuration;
using CarsIsland.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarsIsland.API.Core.DependencyInjection;

public static class ConfigurationServiceCollectionExtensions
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
    {
        var blobAccountName = config.TryGetFromEnv(ConfigConstants.BlobAccountName);
        var blobKey = config.TryGetFromEnv(ConfigConstants.BlobKey);
        var blobContainerName = config.TryGetFromEnv(ConfigConstants.BlobContainerName);
        var blobConnectionString = config.TryGetFromEnv(ConfigConstants.BlobConnectionString);
        var blobSettings =
            new BlobStorageServiceConfiguration(blobContainerName, blobConnectionString, blobKey, blobAccountName);

        services.AddSingleton<IBlobStorageServiceConfiguration>(blobSettings);

        var cosmosConnectionString = config.TryGetFromEnv(ConfigConstants.CosmosConnectionString);
        var cosmosDatabaseName = config.TryGetFromEnv(ConfigConstants.CosmosDatabaseName);
        var cosmosCarContainerName = config.TryGetFromEnv(ConfigConstants.CosmosCarContainerName);
        var cosmosEnquiryContainerName = config.TryGetFromEnv(ConfigConstants.CosmosEnquiryContainerName);
        var cosmosPartitionKeyPath = config.TryGetFromEnv(ConfigConstants.CosmosPartitionKeyPath);

        var cosmosDbSettings = new CosmosDbConfiguration(cosmosConnectionString, cosmosDatabaseName,
            cosmosCarContainerName, cosmosEnquiryContainerName, cosmosPartitionKeyPath);

        services.AddSingleton<ICosmosDbConfiguration>(cosmosDbSettings);

        return services;
    }
}