using System;
namespace TigerOpenAPI.Common.Enum
{
  public class MultiTagField
  {

    /** 所属行业 */
    public static readonly MultiTagField MultiTagField_Industry = new MultiTagField(1, "industry");
    /** 所属概念 */
    public static readonly MultiTagField MultiTagField_Concept = new MultiTagField(2, "concept");
    /** 是否为otc股票.1=是，0=否 */
    public static readonly MultiTagField MultiTagField_isOTC = new MultiTagField(3, "isOTC");
    public static readonly MultiTagField MultiTagField_StockCode = new MultiTagField(4, "symbol");
    /** 股票类型 stock or etf ;股票类型,非0表示该股票是ETF,1表示不带杠杆的etf,2表示2倍杠杆etf,3表示3倍etf杠杆,负值表示反向的ETF */
    public static readonly MultiTagField MultiTagField_Type = new MultiTagField(5, "type");
    /** 成交量异常.1=是，0=否 ;当日实时成交量> 5* 最近一年的平均成交量 */
    public static readonly MultiTagField MultiTagField_Volume_Spike = new MultiTagField(6, "volSpike");
    /** 破净股票；市净率PB<1 */
    public static readonly MultiTagField MultiTagField_Net_Broken = new MultiTagField(7, "netBroken");
    /** 破发股票 ； 最新价<发行价 */
    public static readonly MultiTagField MultiTagField_Issue_Price_Broken = new MultiTagField(8, "issuePriceBroken");
    /** 跟踪指数/资产 - ETF */
    public static readonly MultiTagField MultiTagField_PrimaryBenchmark = new MultiTagField(9, "primaryBenchmark");
    /** 发行人 - ETF */
    public static readonly MultiTagField MultiTagField_Issuer = new MultiTagField(10, "issuer");
    /** 托管人 - ETF */
    public static readonly MultiTagField MultiTagField_Custodian = new MultiTagField(11, "custodian");
    /** 分红频率 - ETF */
    public static readonly MultiTagField MultiTagField_DistributionFrequency = new MultiTagField(12, "distributionFrequency");
    /** 是否支持期权 - ETF ; 1=是，0=否 */
    public static readonly MultiTagField MultiTagField_OptionsAvailable = new MultiTagField(13, "optionsAvailable");
    /** 今日创历史新高 - ETF 1=是，0=否 */
    public static readonly MultiTagField MultiTagField_Today_HistoryHigh = new MultiTagField(14, "todayHistoryHigh");
    /** 今日创历史新低 - ETF 1=是，0=否 */
    public static readonly MultiTagField MultiTagField_Today_HistoryLow = new MultiTagField(15, "todayHistoryLow");
    /** 股票包 */
    public static readonly MultiTagField MultiTagField_Stock_Package = new MultiTagField(16, "StockPkg");
    /** 52周最高 0 否 1是* */
    public static readonly MultiTagField MultiTagField_Week52HighFlag = new MultiTagField(17, "week52HighFlag");
    /** 52周最低 0 否 1是 */
    public static readonly MultiTagField MultiTagField_Week52LowFlag = new MultiTagField(18, "week52LowFlag");

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

