using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteContractsModel : ApiModel
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "sec_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SecType SecType { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }

    public QuoteContractsModel() : base()
    {
    }
  }
}

