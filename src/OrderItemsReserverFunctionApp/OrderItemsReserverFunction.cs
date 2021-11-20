using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderItemsReserverFunctionApp.BlobStorageService;
using OrderItemsReserverFunctionApp.EmailNotifier;

namespace OrderItemsReserverFunctionApp
{
    public class OrderItemsReserverFunction
    {
        private readonly IEmailNotifier _emailNotifier;
        private readonly IBlobStorageService _blobStorageService;

        public OrderItemsReserverFunction(IEmailNotifier emailNotifier, IBlobStorageService blobStorageService)
        {
            _emailNotifier = emailNotifier;
            _blobStorageService = blobStorageService;
        }

        [FunctionName("OrderItemsReserverFunction")]
        public async Task Run([ServiceBusTrigger("%QueueName%", Connection = "ServiceBusConnectionString")]string message,
            ILogger log)
        {
            log.LogInformation($"ServiceBus queue trigger function processed message: {message}");

            var order = JsonConvert.DeserializeObject<Order>(message);

            try
            {
                var dataToUpload = Encoding.UTF8.GetBytes(message);

                await _blobStorageService.UploadFile($"{order.Id}.json", dataToUpload,
                    System.Net.Mime.MediaTypeNames.Application.Json);

                log.LogInformation("The order details was sent to Blob storage.");
            }
            catch (Exception e)
            {
                log.LogError(e, "The exception occurred while processing the message.");
                await _emailNotifier.SendNotificationEmail(order);
                throw;
            }
        }
    }
}
