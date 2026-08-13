using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  /// <summary>
  /// Request model for querying detail of a single position transfer
  /// </summary>
  public class PositionTransferDetailModel : TradeModel
  {
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }

    [JsonProperty(PropertyName = "account_id")]
    public string AccountId { get; set; }

    public PositionTransferDetailModel() : base()
    {
    }

    public PositionTransferDetailModel(long id, string accountId) : base()
    {
      Id = id;
      AccountId = accountId;
    }
  }
}
