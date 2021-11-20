using System;

namespace OrderItemsReserverFunctionApp.BlobStorageService;

public class FileNotUploadedException : Exception
{
    public FileNotUploadedException(Exception innerException) : base("The file was not uploaded to blob.", innerException)
    {
        
    }
}
