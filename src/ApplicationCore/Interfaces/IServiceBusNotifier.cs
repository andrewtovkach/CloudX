using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IServiceBusNotifier
{
    Task SendMessage<T>(T message);
}
