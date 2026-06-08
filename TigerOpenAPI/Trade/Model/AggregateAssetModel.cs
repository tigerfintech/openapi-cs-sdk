using System;
using Newtonsoft.Json;
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
