using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace OrderItemsReserverFunctionApp.EmailNotifier;

public class EmailNotifier : IEmailNotifier
{
    private readonly ILogger<EmailNotifier> _logger;
    private readonly HttpClient _client;
    private readonly EmailNotifierOptions _settings;

    public EmailNotifier(IHttpClientFactory httpClientFactory, ILogger<EmailNotifier> logger, IOptions<EmailNotifierOptions> settings)
    {
        if (string.IsNullOrEmpty(settings.Value.HostUrl))
        {
            throw new ArgumentNullException(nameof(settings.Value.HostUrl));
        }

        _logger = logger;
        this._client = httpClientFactory.CreateClient();
        _settings = settings.Value;
    }

    public async Task SendNotificationEmail<T>(T bodyParams)
    {
        _logger.LogInformation("Sending email...");

        var body = new StringContent(JsonConvert.SerializeObject(bodyParams), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(_settings.HostUrl, body);
        if (!response.IsSuccessStatusCode)
        {
            throw new EmailNotSendException();
        }

        _logger.LogInformation("The email was successfully sent.");
    }
}
