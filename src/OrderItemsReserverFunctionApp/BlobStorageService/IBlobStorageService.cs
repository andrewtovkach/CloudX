using System.Threading.Tasks;

namespace OrderItemsReserverFunctionApp.BlobStorageService;

public interface IBlobStorageService
{
    Task<string> UploadFile(string fileName, byte[] fileData, string fileMimeType);
}
