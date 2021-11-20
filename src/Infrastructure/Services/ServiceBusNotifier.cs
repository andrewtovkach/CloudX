using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public class ServiceBusNotifier : IServiceBusNotifier
{
    private readonly ServiceBusOptions _settings;

    public ServiceBusNotifier(IOptions<ServiceBusOptions> settings)
    {
        if (string.IsNullOrEmpty(settings.Value.ConnectionString))
        {
            throw new ArgumentNullException(nameof(settings.Value.ConnectionString));
        }

        if (string.IsNullOrEmpty(settings.Value.QueueName))
        {
            throw new ArgumentNullException(nameof(settings.Value.QueueName));
        }

        _settings = settings.Value;
    }

    public async Task SendMessage<T>(T message)
    {
        ServiceBusClient client = new ServiceBusClient(_settings.ConnectionString);
        ServiceBusSender sender = client.CreateSender(_settings.QueueName);

        var messageBody = JsonConvert.SerializeObject(message);

        await sender.SendMessageAsync(new ServiceBusMessage(messageBody));
    }
}
