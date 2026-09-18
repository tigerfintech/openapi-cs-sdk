using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class PrimeAssetsModel : TradeModel
  {
    [JsonProperty(PropertyName = "base_currency")]
    public string BaseCurrency { get; set; }

    [JsonProperty(PropertyName = "consolidated")]
    public Boolean Consolidated { get; set; }

    /// <summary>
    /// Asset quote type: ETH / RTH / OVERNIGHT. Optional, omitted from the request when null.
    /// Nullable on purpose: the client serializer uses <c>DefaultValueHandling.Ignore</c>, so a
    /// non-nullable enum would silently drop <c>ETH</c> (ordinal 0) even when set explicitly.
    /// </summary>
    [JsonProperty(PropertyName = "asset_quote_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public AssetQuoteType? AssetQuoteType { get; set; }
  }
}

