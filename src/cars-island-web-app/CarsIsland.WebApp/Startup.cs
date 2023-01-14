using CarsIsland.WebApp.Data;
using CarsIsland.WebApp.Services;
using CarsIsland.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace CarsIsland.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddHttpClient<ICarsIslandApiService, CarsIslandApiService>(configureClient =>
                {
                    var apiAddress = Configuration.GetValue<string>("API_ADDRESS");

                    configureClient.BaseAddress = new Uri(apiAddress);
                })
                .AddPolicyHandler(GetRetryPolicy(services))
                .AddPolicyHandler(GetCircuitBreakerPolicy(services));


            services.AddScoped<CarDataService>();
            services.AddScoped<EnquiryDataService>();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IServiceCollection services)
        {
            return HttpPolicyExtensions
                // Handle HttpRequestExceptions, 408 and 5xx status codes:
                .HandleTransientHttpError()
                // Handle 404 not found
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                // Retry 3 times, each time wait 2, 4 and 8 seconds before retrying:
                .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(4),
                        TimeSpan.FromSeconds(8)
                    },
                    onRetry: (_, timespan, retryAttempt, _) =>
                    {
                        services.BuildServiceProvider()
                            .GetRequiredService<ILogger<CarsIslandApiService>>()?
                            .LogError("Delaying for {delay}ms, then making retry: {retry}.", timespan.TotalMilliseconds,
                                retryAttempt);
                    });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(IServiceCollection services)
        {
            // Handle HttpRequestExceptions, 408 and 5xx status codes:
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(10),
                    onBreak: (_, timeSpan, _) =>
                    {
                        services.BuildServiceProvider()
                            .GetRequiredService<ILogger<CarsIslandApiService>>()?
                            .LogError("CircuitBreaker onBreak for {delay}ms", timeSpan.TotalMilliseconds);
                    },
                    onReset: _ =>
                    {
                        services.BuildServiceProvider()
                            .GetRequiredService<ILogger<CarsIslandApiService>>()?
                            .LogError("CircuitBreaker closed again");
                    },
                    onHalfOpen: () =>
                    {
                        services.BuildServiceProvider()
                            .GetRequiredService<ILogger<CarsIslandApiService>>()?
                            .LogError("CircuitBreaker onHalfOpen");
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}