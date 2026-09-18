using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  /// <summary>
  /// Request model for aggregate asset query (multi-account asset aggregation)
  /// </summary>
  public class AggregateAssetModel : TradeModel
  {
    [JsonProperty(PropertyName = "seg_type")]
    public string SegType { get; set; }

    [JsonProperty(PropertyName = "base_currency")]
    public string BaseCurrency { get; set; }

    /// <summary>
    /// Asset quote type: ETH / RTH / OVERNIGHT. Optional, omitted from the request when null.
    /// Nullable on purpose: the client serializer uses <c>DefaultValueHandling.Ignore</c>, so a
    /// non-nullable enum would silently drop <c>ETH</c> (ordinal 0) even when set explicitly.
    /// </summary>
    [JsonProperty(PropertyName = "asset_quote_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public AssetQuoteType? AssetQuoteType { get; set; }

    public AggregateAssetModel() : base()
    {
    }

    public AggregateAssetModel(string account, string segType) : base()
    {
      Account = account;
      SegType = segType;
    }

    public AggregateAssetModel(string account, string segType, string secretKey) : this(account, segType)
    {
      SecretKey = secretKey;
    }

    public AggregateAssetModel(string account, string segType, string baseCurrency, string secretKey) : this(account, segType, secretKey)
    {
      BaseCurrency = baseCurrency;
    }
  }
}
