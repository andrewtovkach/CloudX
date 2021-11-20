using System;

namespace OrderItemsReserverFunctionApp.Models;

public class Order
{
    public int Id { get; set; }

    public string BuyerId { get; set; }

    public DateTimeOffset OrderDate { get; set; }
}
