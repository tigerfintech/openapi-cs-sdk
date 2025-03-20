using System;
using Newtonsoft.Json;
using Org.BouncyCastle.Math.EC.Multiplier;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote.Response;

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
    public Double Strike { get; set; } = double.NaN;
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public Double Multiplier { get; set; }
    [JsonProperty(PropertyName = "lotSize")]
    public Double LotSize { get; set; }
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
    public Double MinTick { get; set; } = double.NaN;
    [JsonProperty(PropertyName = "marginable")]
    public Boolean Marginable { get; set; }

    [JsonProperty(PropertyName = "shortInitialMargin")]
    public Double ShortInitialMargin { get; set; } = double.NaN;
    [JsonProperty(PropertyName = "shortMaintenanceMargin")]
    public Double ShortMaintenanceMargin { get; set; } = double.NaN;
    [JsonProperty(PropertyName = "shortFeeRate")]
    public Double ShortFeeRate { get; set; } = double.NaN;
    [JsonProperty(PropertyName = "shortable")]
    public Boolean Shortable { get; set; }
    [JsonProperty(PropertyName = "shortableCount")]
    public long ShortableCount { get; set; }
    [JsonProperty(PropertyName = "longInitialMargin")]
    public Double LongInitialMargin { get; set; } = double.NaN;
    [JsonProperty(PropertyName = "longMaintenanceMargin")]
    public Double LongMaintenanceMargin { get; set; } = double.NaN;
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

    /** Intraday initial margin discount */
    [JsonProperty(PropertyName = "discountedDayInitialMargin")]
    public Double DiscountedDayInitialMargin { get; set; } = double.NaN;
    /** Intraday maintenance margin discount */
    [JsonProperty(PropertyName = "discountedDayMaintenanceMargin")]
    public Double DiscountedDayMaintenanceMargin { get; set; } = double.NaN;
    /** Intraday margin discount period time zone  */
    [JsonProperty(PropertyName = "discountedTimeZoneCode")]
    public string DiscountedTimeZoneCode { get; set; }
    [JsonProperty(PropertyName = "discountedStartAt")]
    public string DiscountedStartAt { get; set; }
    [JsonProperty(PropertyName = "discountedEndAt")]
    public string DiscountedEndAt { get; set; }

    [JsonProperty(PropertyName = "isEtf")]
    public Boolean IsEtf { get; set; }
    [JsonProperty(PropertyName = "etfLeverage")]
    public Int32 EtfLeverage { get; set; }
    [JsonProperty(PropertyName = "supportOvernightTrading")]
    public Boolean SupportOvernightTrading { get; set; }

    public ContractItem()
    {
    }

    public static ContractItem Convert(FutureContractItem futureContractItem)
    {
      ContractItem contractItem = new ContractItem();
      contractItem.SecType = TigerOpenAPI.Common.Enum.SecType.FUT.ToString();
      contractItem.Symbol = futureContractItem.ContractCode;
      contractItem.Type = futureContractItem.Type;
      contractItem.IbCode = futureContractItem.IbCode;
      contractItem.Name = futureContractItem.Name;
      contractItem.ContractMonth = futureContractItem.ContractMonth;
      contractItem.Exchange = futureContractItem.Exchange;
      contractItem.Multiplier = decimal.ToDouble(futureContractItem.Multiplier);
      contractItem.MinTick = decimal.ToDouble(futureContractItem.MinTick);

      contractItem.Expiry = futureContractItem.LastTradingDate;
      contractItem.FirstNoticeDate = futureContractItem.FirstNoticeDate;
      contractItem.LastBiddingCloseTime = futureContractItem.LastBiddingCloseTime;
      contractItem.Currency = futureContractItem.Currency;
      contractItem.Tradeable = futureContractItem.Trade;
      contractItem.Continuous = futureContractItem.Continuous;
      return contractItem;
    }

    public static ContractItem Convert(FundContractItem fundContractItem)
    {
      ContractItem contractItem = new ContractItem();
      contractItem.SecType = TigerOpenAPI.Common.Enum.SecType.FUND.ToString();
      contractItem.Symbol = fundContractItem.Symbol;
      contractItem.Market = fundContractItem.Market;
      contractItem.Currency = fundContractItem.Currency;
      return contractItem;
    }

    public static ContractItem BuildStockContract(string symbol, string currency)
    {
      ContractItem contractItem = new ContractItem()
      {
        SecType = TigerOpenAPI.Common.Enum.SecType.STK.ToString(),
        Symbol = symbol,
        Currency = currency
      };
      return contractItem;
    }

    public static ContractItem BuildOptionContract(string identifier)
    {
      ContractItem contractItem = new ContractItem();
      contractItem.SecType = TigerOpenAPI.Common.Enum.SecType.OPT.ToString();
      // identifier='AAPL  190118P00160000'
      OptionSymbol optionSymbol = SymbolUtil.convertToOptionSymbol(identifier);
      contractItem.Symbol = optionSymbol.Symbol;
      contractItem.Expiry = optionSymbol.Expiry;
      contractItem.Strike = Double.Parse(optionSymbol.Strike);
      contractItem.Right = optionSymbol.Right;
      return contractItem;
    }

    public static ContractItem BuildOptionContract(string symbol, string expiry, double strike, string right)
    {
      ContractItem contractItem = new ContractItem()
      {
        SecType = TigerOpenAPI.Common.Enum.SecType.OPT.ToString(),
        Symbol = symbol,
        Expiry = expiry,
        Strike = strike,
        Right = right
      };
      return contractItem;
    }

    public static ContractItem BuildWarrantContract(string symbol, string expiry, double strike, string right)
    {
      ContractItem contractItem = new ContractItem()
      {
        SecType = TigerOpenAPI.Common.Enum.SecType.WAR.ToString(),
        Symbol = symbol,
        Expiry = expiry,
        Strike = strike,
        Right = right,
        LocalSymbol = symbol,
        Currency = TigerOpenAPI.Common.Enum.Currency.HKD.ToString(),
        Market = TigerOpenAPI.Common.Enum.Market.HK.ToString()
      };
      return contractItem;

    }

    public static ContractItem BuildCbbcContract(string symbol, string expiry, double strike, string right)
    {
      ContractItem contractItem = new ContractItem()
      {
        SecType = TigerOpenAPI.Common.Enum.SecType.IOPT.ToString(),
        Symbol = symbol,
        Expiry = expiry,
        Strike = strike,
        Right = right,
        LocalSymbol = symbol,
        Currency = TigerOpenAPI.Common.Enum.Currency.HKD.ToString(),
        Market = TigerOpenAPI.Common.Enum.Market.HK.ToString()
      };
      return contractItem;
    }

    public static ContractItem BuildFutureContract(string symbol, string currency, string exchange,
        string expiry, double multiplier)
    {
      ContractItem contractItem = new ContractItem()
      {
        SecType = TigerOpenAPI.Common.Enum.SecType.FUT.ToString(),
        Symbol = symbol,
        Expiry = expiry,
        Currency = currency,
        Exchange = exchange,
        Multiplier = multiplier
      };
      return contractItem;
    }

    public static ContractItem BuildFutureContract(string symbol, string currency)
    {
      ContractItem contractItem = new ContractItem()
      {
        SecType = TigerOpenAPI.Common.Enum.SecType.FUT.ToString(),
        Symbol = symbol,
        Currency = currency
      };
      return contractItem;
    }

    public static ContractItem BuildFundContract(string symbol, string currency)
    {
      ContractItem contractItem = new ContractItem()
      {
        SecType = TigerOpenAPI.Common.Enum.SecType.FUND.ToString(),
        Symbol = symbol,
        Currency = currency
      };
      return contractItem;
    }
  }
}

