using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions;

public class ApiClientException : Exception
{
    public ApiClientException() : base("Error occurred while making a request.")
    {
        
    }
}
