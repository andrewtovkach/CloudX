using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace DeliveryOrderProcessorFunctionApp.CosmosTableService;

public class CosmosTableService : ICosmosTableService
{
    private readonly CosmosTableOptions _settings;

    public CosmosTableService(IOptions<CosmosTableOptions> settings)
    {
        if (string.IsNullOrEmpty(settings.Value.EndpointUri))
        {
            throw new ArgumentNullException(nameof(settings.Value.EndpointUri));
        }

        if (string.IsNullOrEmpty(settings.Value.PrimaryKey))
        {
            throw new ArgumentNullException(nameof(settings.Value.PrimaryKey));
        }
        
        if (string.IsNullOrEmpty(settings.Value.DatabaseName))
        {
            throw new ArgumentNullException(nameof(settings.Value.DatabaseName));
        }
        
        if (string.IsNullOrEmpty(settings.Value.ContainerName))
        {
            throw new ArgumentNullException(nameof(settings.Value.ContainerName));
        }

        _settings = settings.Value;
    }

    public async Task Add<T>(T item)
    {
        using CosmosClient client = new CosmosClient(_settings.EndpointUri, _settings.PrimaryKey);
        var database = client.GetDatabase(_settings.DatabaseName);
        var container = database.GetContainer(_settings.ContainerName);

        await container.CreateItemAsync(item);
    }
}
