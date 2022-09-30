using CarsIsland.Infrastructure.Configuration;
using CarsIsland.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CarsIsland.API.Core.DependencyInjection;

public static class ConfigurationServiceCollectionExtensions
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
    {
        var blobSettings = config.GetSection("BlobStorageSettings");
        services.Configure<BlobStorageServiceConfiguration>(blobSettings);

        services
            .AddSingleton<IValidateOptions<BlobStorageServiceConfiguration>,
                BlobStorageServiceConfigurationValidation>();

        var blobStorageServiceConfiguration = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<BlobStorageServiceConfiguration>>().Value;

        services.AddSingleton<IBlobStorageServiceConfiguration>(blobStorageServiceConfiguration);

        var cosmosDbSettings = config.GetSection("CosmosDbSettings");
        services.Configure<CosmosDbConfiguration>(cosmosDbSettings);

        services.AddSingleton<IValidateOptions<CosmosDbConfiguration>, CosmosDbConfigurationValidation>();

        var cosmosDbConfiguration = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<CosmosDbConfiguration>>().Value;

        services.AddSingleton<ICosmosDbConfiguration>(cosmosDbConfiguration);

        return services;
    }
}