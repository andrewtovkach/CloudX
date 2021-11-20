using System;
using System.Threading.Tasks;
using DeliveryOrderProcessorFunctionApp.CosmosTableService;
using DeliveryOrderProcessorFunctionApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DeliveryOrderProcessorFunctionApp
{
    public class DeliveryOrderProcessorFunction
    {
        private readonly ICosmosTableService _cosmosTableService;

        public DeliveryOrderProcessorFunction(ICosmosTableService cosmosTableService)
        {
            _cosmosTableService = cosmosTableService;
        }

        [FunctionName("DeliveryOrderProcessorFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP trigger function processed a request.");

            try
            {
                var json = await req.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<Order>(json);
                await _cosmosTableService.Add(order);

                log.LogInformation("The order was send to Delivery database.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Something went wrong.");

                return new ObjectResult(new { error = ex.Message }) { StatusCode = 500 };
            }

            return new OkObjectResult(new { success = true });
        }
    }
}
