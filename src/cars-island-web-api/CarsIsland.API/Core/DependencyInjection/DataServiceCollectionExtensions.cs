using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        cosmosClient.SeedDatabase(cosmoDbConfiguration).GetAwaiter().GetResult();

        services.AddSingleton(cosmosClient);

        services.AddSingleton<IDataRepository<Car>, CarRepository>();
        services.AddSingleton<IDataRepository<Enquiry>, EnquiryRepository>();

        return services;
    }

    private static async Task SeedDatabase(this CosmosClient client, ICosmosDbConfiguration configuration)
    {
        var database = client.GetDatabase(configuration.DatabaseName);
        var container = database.GetContainer(configuration.CarContainerName);

        try
        {
            var tasks = new List<Task>(CarSeed.Count);

            foreach (var car in CarSeed)
            {
                tasks.Add(container.CreateItemAsync(car, new PartitionKey(car.Id)));
            }

            await Task.WhenAll(tasks);
        }
        catch (Exception)
        {
            Console.WriteLine("Database already seeded.");
        }
    }

    private static readonly List<Car> CarSeed = new()
    {
        new Car
        {
            Id = Guid.NewGuid().ToString(),
            Brand = "BNW",
            ImageUrl = "bmw-car-image.jpg",
            Location = "Warsaw, Poland",
            Model = "320",
            PricePerDay = 350
        },
        new Car
        {
            Id = Guid.NewGuid().ToString(),
            Brand = "Audi",
            ImageUrl = "audi-car-image.jpg",
            Location = "Berlin, Germany",
            Model = "A6",
            PricePerDay = 225m
        },
        new Car
        {
            Id = Guid.NewGuid().ToString(),
            Brand = "Fiat",
            ImageUrl = "fiat-car-image.png",
            Location = "Milano, Italy",
            Model = "500XL",
            PricePerDay = 150m
        },
        new Car
        {
            Id = Guid.NewGuid().ToString(),
            Brand = "Smart car",
            ImageUrl = "smart-car-image.jpg",
            Location = "Poznan, Poland",
            Model = "228X",
            PricePerDay = 260m
        },
        new Car
        {
            Id = Guid.NewGuid().ToString(),
            Brand = "Mercedes-Benz",
            ImageUrl = "mercedes-car-image.jpg",
            Location = "Poznan, Poland",
            Model = "SLS",
            PricePerDay = 640m
        },
        new Car
        {
            Id = Guid.NewGuid().ToString(),
            Brand = "Mercedes-Benz",
            ImageUrl = "mercedes-car-img.png",
            Location = "Lublin, Poland",
            Model = "E320",
            PricePerDay = 440m
        },
    };
}