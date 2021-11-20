using System.Threading.Tasks;

namespace OrderItemsReserverFunctionApp.EmailNotifier;

public interface IEmailNotifier
{
    Task SendNotificationEmail<T>(T bodyParams);
}
