namespace DeliveryOrderProcessorFunctionApp.CosmosTableService;

public class CosmosTableOptions
{
    public string EndpointUri { get; set; }

    public string PrimaryKey { get; set; }

    public string DatabaseName { get; set; }

    public string ContainerName { get; set; }
}
