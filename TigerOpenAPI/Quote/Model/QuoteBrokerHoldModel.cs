using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteBrokerHoldModel : ApiModel
  {
    [JsonProperty(PropertyName = "market"), JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit { get; set; }

    [JsonProperty(PropertyName = "page")]
    public Int32 Page { get; set; }

    [JsonProperty(PropertyName = "order_by")]
    public string OrderBy { get; set; }

    [JsonProperty(PropertyName = "direction")]
    public string Direction { get; set; }

    public QuoteBrokerHoldModel() : base()
    {
    }

    public QuoteBrokerHoldModel(Market market, int limit) : base()
    {
      Market = market;
      Limit = limit;
    }

    public QuoteBrokerHoldModel(Market market, int limit, int page) : base()
    {
      Market = market;
      Limit = limit;
      Page = page;
    }

    public QuoteBrokerHoldModel(Market market, int limit, int page, string orderBy, string direction) : base()
    {
      Market = market;
      Limit = limit;
      Page = page;
      OrderBy = orderBy;
      Direction = direction;
    }
  }
}
