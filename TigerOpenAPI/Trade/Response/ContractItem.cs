using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class ContractItem
  {
    [JsonProperty(PropertyName = "contractId")]
    public Int32 ContractId { get; set; }
    [JsonProperty(PropertyName = "identifier")]
    public string Identifier { get; set; }
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }
    [JsonProperty(PropertyName = "contractMonth")]
    public string ContractMonth { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public Double Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public Double Multiplier { get; set; }
    [JsonProperty(PropertyName = "exchange")]
    public string Exchange { get; set; }

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "primaryExchange")]
    public string PrimaryExchange { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "localSymbol")]
    public string LocalSymbol { get; set; }
    [JsonProperty(PropertyName = "tradingClass")]
    public string TradingClass { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "tradeable")]
    public Boolean Tradeable { get; set; }
    [JsonProperty(PropertyName = "closeOnly")]
    public Boolean CloseOnly { get; set; }
    [JsonProperty(PropertyName = "minTick")]
    public Double MinTick { get; set; }
    [JsonProperty(PropertyName = "marginable")]
    public Boolean Marginable { get; set; }

    [JsonProperty(PropertyName = "shortInitialMargin")]
    public Double ShortInitialMargin { get; set; }
    [JsonProperty(PropertyName = "shortMaintenanceMargin")]
    public Double ShortMaintenanceMargin { get; set; }
    [JsonProperty(PropertyName = "shortFeeRate")]
    public Double ShortFeeRate { get; set; }
    [JsonProperty(PropertyName = "shortable")]
    public Boolean Shortable { get; set; }
    [JsonProperty(PropertyName = "shortableCount")]
    public long ShortableCount { get; set; }
    [JsonProperty(PropertyName = "longInitialMargin")]
    public Double LongInitialMargin { get; set; }
    [JsonProperty(PropertyName = "longMaintenanceMargin")]
    public Double LongMaintenanceMargin { get; set; }
    [JsonProperty(PropertyName = "lastTradingDate")]
    public string LastTradingDate { get; set; }
    [JsonProperty(PropertyName = "firstNoticeDate")]
    public string FirstNoticeDate { get; set; }
    [JsonProperty(PropertyName = "lastBiddingCloseTime")]
    public long LastBiddingCloseTime { get; set; }

    [JsonProperty(PropertyName = "continuous")]
    public Boolean Continuous { get; set; }
    /** future contract fields */
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }
    [JsonProperty(PropertyName = "ibCode")]
    public string IbCode { get; set; }
    [JsonProperty(PropertyName = "tickSizes")]
    public List<TickSizeItem> TickSizes { get; set; }
    [JsonProperty(PropertyName = "isEtf")]
    public Boolean IsEtf { get; set; }
    [JsonProperty(PropertyName = "etfLeverage")]
    public Int16 EtfLeverage { get; set; }

    public ContractItem()
    {
    }
  }
}

