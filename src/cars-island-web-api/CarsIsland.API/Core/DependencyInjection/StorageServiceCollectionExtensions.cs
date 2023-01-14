using Azure;
using Azure.Storage.Blobs;
using CarsIsland.Infrastructure.Configuration.Interfaces;
using CarsIsland.Infrastructure.Services;
using CarsIsland.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarsIsland.API.Core.DependencyInjection;

public static class StorageServiceCollectionExtensions
{
    public static IServiceCollection AddStorageServices(this IServiceCollection services, string containerName)
    {
        var serviceProvider = services.BuildServiceProvider();

        var storageConfiguration = serviceProvider.GetRequiredService<IBlobStorageServiceConfiguration>();

        var client = new BlobServiceClient(storageConfiguration.ConnectionString);

        var containerClient = client.GetBlobContainerClient(containerName);

        var containerExists = containerClient.Exists().Value;

        if (!containerExists)
        {
            containerClient.Create();
        }

        services.AddSingleton(_ => client);
        services.AddSingleton<IBlobStorageService, BlobStorageService>();

        return services;
    }
}