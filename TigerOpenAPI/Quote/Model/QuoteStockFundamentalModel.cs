using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteStockFundamentalModel : ApiModel
  {

    [JsonProperty(PropertyName = "symbols")]
    public List<string> Symbols { get; set; }

    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    public QuoteStockFundamentalModel() : base()
    {
    }
  }
}

