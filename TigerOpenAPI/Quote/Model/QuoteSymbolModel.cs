using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteSymbolModel : ApiModel
  {

    [JsonProperty(PropertyName = "symbols")]
    public List<string> Symbols { get; set; }

    [JsonProperty(PropertyName = "market", NullValueHandling = NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market? Market { get; set; }

    // 'QUOTE_DELAY'/'TIMELINE'/'HISTORY_TIMELINE' don't have 'include_hour_trading' param
    [JsonProperty(PropertyName = "include_hour_trading")]
    public Boolean IncludeHourTrading { get; set; }

    [JsonProperty(PropertyName = "trade_session")]
    public string TradeSession { get; set; }

    public QuoteSymbolModel() : base()
    {
    }
  }
}

