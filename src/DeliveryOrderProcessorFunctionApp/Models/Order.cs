using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Newtonsoft.Json;

namespace DeliveryOrderProcessorFunctionApp.Models;

public class Order
{
    public Order()
    {
        UniqueIdentifier = Guid.NewGuid();
    }

    public int Id { get; set; }

    public string BuyerId { get; set; }

    public DateTimeOffset OrderDate { get; set; }

    public Address ShipToAddress { get; set; }

    public IList<OrderItem> OrderItems { get; set; }

    [JsonProperty(PropertyName = "id")]
    private Guid UniqueIdentifier { get; set; }
}
