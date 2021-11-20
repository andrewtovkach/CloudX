using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IApiClient
{
    Task Post<T>(T body);
}
