using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteCapitalModel : ApiModel
  {

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    public QuoteCapitalModel() : base()
    {
    }
  }
}

