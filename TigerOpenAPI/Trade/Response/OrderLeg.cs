using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class OrderLeg
  {
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public Double Multiplier { get; set; }
    [JsonProperty(PropertyName = "totalQuantity")]
    public Double TotalQuantity { get; set; }
    [JsonProperty(PropertyName = "filledQuantity")]
    public Double FilledQuantity { get; set; }
    [JsonProperty(PropertyName = "avgFilledPrice")]
    public Double AvgFilledPrice { get; set; }
    [JsonProperty(PropertyName = "createdAt")]
    public Int64 CreatedAt { get; set; }
    [JsonProperty(PropertyName = "updatedAt")]
    public Int64 UpdatedAt { get; set; }

    public OrderLeg()
    {
    }
  }
}
