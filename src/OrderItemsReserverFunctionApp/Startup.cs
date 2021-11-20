using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderItemsReserverFunctionApp.BlobStorageService;
using OrderItemsReserverFunctionApp.EmailNotifier;

[assembly: FunctionsStartup(typeof(OrderItemsReserverFunctionApp.Startup))]
namespace OrderItemsReserverFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddOptions<EmailNotifierOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(EmailNotifierOptions)).Bind(settings);
                });

            builder.Services.AddOptions<BlobStorageOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(BlobStorageOptions)).Bind(settings);
                });

            builder.Services.AddTransient<IEmailNotifier, EmailNotifier.EmailNotifier>();
            builder.Services.AddTransient<IBlobStorageService, BlobStorageService.BlobStorageService>();
        }
    }
}
