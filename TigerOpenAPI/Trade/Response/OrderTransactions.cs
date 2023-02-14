using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Trade.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class OrderTransactions
  {

    /**
     * order transaction id
     */
    [JsonProperty(PropertyName = "id")]
    public Int64 Id { get; set; }
    // ’order_id‘ is the 'id' returned after placing an order
    [JsonProperty(PropertyName = "orderId")]
    public Int64 OrderId { get; set; }
    [JsonProperty(PropertyName = "accountId")]
    public string AccountId { get; set; }
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }

    [JsonProperty(PropertyName = "action")]
    public string Action { get; set; }
    [JsonProperty(PropertyName = "filledQuantity")]
    public Int64 FilledQuantity { get; set; }
    [JsonProperty(PropertyName = "filledPrice")]
    public Double FilledPrice { get; set; }
    [JsonProperty(PropertyName = "filledAmount")]
    public Double FilledAmount { get; set; }

    [JsonProperty(PropertyName = "transactedAt")]
    public string TransactedAt { get; set; }
    [JsonProperty(PropertyName = "transactionTime")]
    public Int64 TransactionTime { get; set; }

    public OrderTransactions()
    {
    }
  }
}

