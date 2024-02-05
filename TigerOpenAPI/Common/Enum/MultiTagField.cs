using System;
namespace TigerOpenAPI.Common.Enum
{
  public class MultiTagField
  {
    /** Industry */
    public static readonly MultiTagField MultiTagField_Industry = new MultiTagField(1, "industry");
    /** Concept */
    public static readonly MultiTagField MultiTagField_Concept = new MultiTagField(2, "concept");
    /** OTC stock. 1=yes, 0=no */
    public static readonly MultiTagField MultiTagField_isOTC = new MultiTagField(3, "isOTC");
    public static readonly MultiTagField MultiTagField_StockCode = new MultiTagField(4, "symbol");
    /** Stock type: stock or etf; non-zero value indicates ETF, 1 indicates non-leveraged ETF, 2 indicates 2x leveraged ETF, 3 indicates 3x leveraged ETF, negative value indicates inverse ETF */
    public static readonly MultiTagField MultiTagField_Type = new MultiTagField(5, "type");
    /** Volume spike. 1=yes, 0=no; real-time volume > 5 * average volume of the past year */
    public static readonly MultiTagField MultiTagField_Volume_Spike = new MultiTagField(6, "volSpike");
    /** Net broken stock; PB ratio < 1 */
    public static readonly MultiTagField MultiTagField_Net_Broken = new MultiTagField(7, "netBroken");
    /** Broken issue stock; latest price < issue price */
    public static readonly MultiTagField MultiTagField_Issue_Price_Broken = new MultiTagField(8, "issuePriceBroken");
    /** Tracking index/asset - ETF */
    public static readonly MultiTagField MultiTagField_PrimaryBenchmark = new MultiTagField(9, "primaryBenchmark");
    /** Issuer - ETF */
    public static readonly MultiTagField MultiTagField_Issuer = new MultiTagField(10, "issuer");
    /** Custodian - ETF */
    public static readonly MultiTagField MultiTagField_Custodian = new MultiTagField(11, "custodian");
    /** Distribution frequency - ETF */
    public static readonly MultiTagField MultiTagField_DistributionFrequency = new MultiTagField(12, "distributionFrequency");
    /** Options available - ETF; 1=yes, 0=no */
    public static readonly MultiTagField MultiTagField_OptionsAvailable = new MultiTagField(13, "optionsAvailable");
    /** Today's historical high - ETF; 1=yes, 0=no */
    public static readonly MultiTagField MultiTagField_Today_HistoryHigh = new MultiTagField(14, "todayHistoryHigh");
    /** Today's historical low - ETF; 1=yes, 0=no */
    public static readonly MultiTagField MultiTagField_Today_HistoryLow = new MultiTagField(15, "todayHistoryLow");
    /** Stock package */
    public static readonly MultiTagField MultiTagField_Stock_Package = new MultiTagField(16, "StockPkg");
    /** 52-week high flag; 0=no, 1=yes */
    public static readonly MultiTagField MultiTagField_Week52HighFlag = new MultiTagField(17, "week52HighFlag");
    /** 52-week low flag; 0=no, 1=yes */
    public static readonly MultiTagField MultiTagField_Week52LowFlag = new MultiTagField(18, "week52LowFlag");
    /** Trading currency; specific currency required */
    public static readonly MultiTagField MultiTagField_TradeCurrency = new MultiTagField(19, "tradeCurrency");
    /** ETF type; specific type required */
    public static readonly MultiTagField MultiTagField_ETF_TYPE = new MultiTagField(20, "etfType");
    /** Stock market; multiple markets supported; specific type required QotMarket stock market, pass the value inside */
    public static readonly MultiTagField MultiTagField_Market_Name = new MultiTagField(21, "marketName");
    /** First-level industry level; specific sectorId required */
    public static readonly MultiTagField MultiTagField_One_Sectors_Level = new MultiTagField(22, "oneSectorsLevel");

    private readonly int index;
    public int Index { get { return index; } }
    private readonly string value;
    public string Value { get { return value; } }

    public MultiTagField(int index, string value)
    {
      this.index = index;
      this.value = value;
    }

    public static IEnumerable<MultiTagField> Values
    {
      get
      {

        yield return MultiTagField_Industry;
        yield return MultiTagField_Concept;
        yield return MultiTagField_isOTC;
        yield return MultiTagField_StockCode;
        yield return MultiTagField_Type;
        yield return MultiTagField_Volume_Spike;
        yield return MultiTagField_Net_Broken;
        yield return MultiTagField_Issue_Price_Broken;
        yield return MultiTagField_PrimaryBenchmark;
        yield return MultiTagField_Issuer;
        yield return MultiTagField_Custodian;
        yield return MultiTagField_DistributionFrequency;
        yield return MultiTagField_OptionsAvailable;
        yield return MultiTagField_Today_HistoryHigh;
        yield return MultiTagField_Today_HistoryLow;
        yield return MultiTagField_Stock_Package;
        yield return MultiTagField_Week52HighFlag;
        yield return MultiTagField_Week52LowFlag;
        yield return MultiTagField_TradeCurrency;
        yield return MultiTagField_ETF_TYPE;
        yield return MultiTagField_Market_Name;
        yield return MultiTagField_One_Sectors_Level;
      }
    }

    public static MultiTagField? getTypeByValue(string value)
    {
      foreach (MultiTagField item in Values)
      {
        if (string.Equals(value, item.Value))
        {
          return item;
        }
      }
      return null;
    }

    public static MultiTagField? getTypeByIndex(int index)
    {
      foreach (MultiTagField item in Values)
      {
        if (item.Index == index)
        {
          return item;
        }
      }
      return null;
    }


    public static int getIndexByValue(string value)
    {
      foreach (MultiTagField item in Values)
      {
        if (string.Equals(value, item.Value))
        {
          return item.Index;
        }
      }
      return -1;
    }

    public static string? getValueByIndex(int index)
    {
      foreach (MultiTagField item in Values)
      {
        if (item.Index == index)
        {
          return item.Value;
        }
      }
      return null;
    }
  }
}

