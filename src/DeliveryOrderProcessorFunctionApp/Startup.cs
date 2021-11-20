using DeliveryOrderProcessorFunctionApp.CosmosTableService;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(DeliveryOrderProcessorFunctionApp.Startup))]
namespace DeliveryOrderProcessorFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<CosmosTableOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(CosmosTableOptions)).Bind(settings);
                });

            builder.Services.AddTransient<ICosmosTableService, CosmosTableService.CosmosTableService>();
        }
    }
}
