using System;

namespace OrderItemsReserverFunctionApp.EmailNotifier;

public class EmailNotSendException : Exception
{
    public EmailNotSendException() : base("The email was not sent.")
    {
    }
}
