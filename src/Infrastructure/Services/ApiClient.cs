using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public class ApiClient : IApiClient
{
    private readonly ApiClientOptions _settings;
    private readonly ILogger<ApiClient> _logger;
    private readonly HttpClient _client;

    public ApiClient(IOptions<ApiClientOptions> settings, ILogger<ApiClient> logger, IHttpClientFactory httpClientFactory)
    {
        if (string.IsNullOrEmpty(settings.Value.HostUrl))
        {
            throw new ArgumentNullException(nameof(settings.Value.HostUrl));
        }

        _settings = settings.Value;
        _logger = logger;
        _client = httpClientFactory.CreateClient();
    }

    public async Task Post<T>(T body)
    {
        _logger.LogInformation("Making a POST request...");

        var bodyData = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        var response = await _client.PostAsync(_settings.HostUrl, bodyData);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiClientException();
        }

        _logger.LogInformation("The POST request was successfully sent.");
    }
}
