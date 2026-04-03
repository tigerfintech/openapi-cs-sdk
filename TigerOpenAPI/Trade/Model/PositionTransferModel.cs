using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  /// <summary>
  /// Request model for submitting a position transfer between accounts
  /// </summary>
  public class PositionTransferModel : TradeModel
  {
    [JsonProperty(PropertyName = "from_account")]
    public string FromAccount { get; set; }

    [JsonProperty(PropertyName = "to_account")]
    public string ToAccount { get; set; }

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }

    [JsonProperty(PropertyName = "transfers")]
    public List<PositionTransferDetail> Transfers { get; set; }

    public PositionTransferModel() : base()
    {
    }

    public PositionTransferModel(string fromAccount, string toAccount, List<PositionTransferDetail> transfers, string market) : base()
    {
      FromAccount = fromAccount;
      ToAccount = toAccount;
      Transfers = transfers;
      Market = market;
    }
  }

  public class PositionTransferDetail
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "sec_type")]
    public string SecType { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }

    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }

    [JsonProperty(PropertyName = "quantity")]
    public long Quantity { get; set; }
  }
}
