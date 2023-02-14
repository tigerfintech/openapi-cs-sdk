using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteStockBrokerModel : ApiModel
  {

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit { get; set; }

    public QuoteStockBrokerModel() : base()
    {
    }
  }
}

