using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace OrderItemsReserverFunctionApp.BlobStorageService;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobStorageOptions _settings;

    public BlobStorageService(IOptions<BlobStorageOptions> settings)
    {
        if (string.IsNullOrEmpty(settings.Value.ConnectionString))
        {
            throw new ArgumentNullException(nameof(settings.Value.ConnectionString));
        }

        if (string.IsNullOrEmpty(settings.Value.ContainerName))
        {
            throw new ArgumentNullException(nameof(settings.Value.ContainerName));
        }

        _settings = settings.Value;
    }

    public async Task<string> UploadFile(string fileName, byte[] fileData, string fileMimeType)
    {
        try
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_settings.ConnectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_settings.ContainerName);

            if (fileName == null || fileData == null)
            {
                return "";
            }

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            cloudBlockBlob.Properties.ContentType = fileMimeType;
            await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);

            return cloudBlockBlob.Uri.AbsoluteUri;
        }
        catch (Exception ex)
        {
            throw new FileNotUploadedException(ex);
        }
    }
}
