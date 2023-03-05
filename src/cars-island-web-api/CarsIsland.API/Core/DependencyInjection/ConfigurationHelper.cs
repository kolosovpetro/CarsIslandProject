using Microsoft.Extensions.Configuration;
using System;

namespace CarsIsland.API.Core.DependencyInjection;

public static class ConfigurationHelper
{
    public static string TryGetFromEnv(this IConfiguration configuration, string key)
    {
        var value = Environment.GetEnvironmentVariable(key) ?? configuration[key];

        return value;
    }
}