using System.Threading.Tasks;

namespace DeliveryOrderProcessorFunctionApp.CosmosTableService;

public interface ICosmosTableService
{
    public Task Add<T>(T item);
}
