using System;
namespace TigerOpenAPI.Common.Enum
{
  public class StockField
  {
    /** 股票代码*，不能填区间上下限值。 */
    /** 最新价*（精确到小数点后 3 位，超出部分会被舍弃）例如填写[10,20]值区间 */
    public static readonly StockField StockField_CurPrice = new StockField(2, "latestPrice");
    /** 买入价（精确到小数点后 3 位，超出部分会被舍弃）例如填写[10,20]值区间 */
    public static readonly StockField StockField_BidPrice = new StockField(3, "bidPrice");
    /** 卖出价（精确到小数点后 3 位，超出部分会被舍弃）例如填写[10,20]值区间 */
    public static readonly StockField StockField_AskPrice = new StockField(4, "askPrice");
    /** 今开价（精确到小数点后 3 位，超出部分会被舍弃）例如填写[10,20]值区间 */
    public static readonly StockField StockField_OpenPrice = new StockField(5, "open");
    /** 昨收价（精确到小数点后 3 位，超出部分会被舍弃）例如填写[10,20]值区间 */
    public static readonly StockField StockField_PreClosePrice = new StockField(6, "preClose");
    /** 最高价 */
    public static readonly StockField StockField_HighPrice = new StockField(7, "high");
    /** 最低价 */
    public static readonly StockField StockField_LowPrice = new StockField(8, "low");
    /** 盘前价*（精确到小数点后 3 位，超出部分会被舍弃）例如填写[10,20]值区间 */
    public static readonly StockField StockField_HourTradingPrePrice = new StockField(9, "hourTradingPrePrice");
    /** 盘后价*（精确到小数点后 3 位，超出部分会被舍弃）例如填写[10,20]值区间 */
    public static readonly StockField StockField_HourTradingAfterPrice = new StockField(10, "hourTradingAfterPrice");
    /** 成交量* */
    public static readonly StockField StockField_Volume = new StockField(11, "volume");
    /** 成交额* */
    public static readonly StockField StockField_Amount = new StockField(12, "amount");
    /** 流通股本* */
    public static readonly StockField StockField_FloatShare = new StockField(13, "floatShares");
    /** 52周最高价格* */
    public static readonly StockField StockField_Week52High = new StockField(14, "week52High");
    /** 52周最低价格* */
    public static readonly StockField StockField_Week52Low = new StockField(15, "week52Low");
    /** 通市值* FloatMarketVal  自己计算 FloatShare* 当前价格 */
    public static readonly StockField StockField_FloatMarketVal = new StockField(16, "floatMarketCap");
    /** 总市值*  MarketVal  shares * 当前价格 */
    public static readonly StockField StockField_MarketValue = new StockField(17, "marketValue");
    /** 盘前涨跌幅   (curPrice-盘前左收）自己计算 最新价-close / close */
    public static readonly StockField StockField_preHourTradingChangeRate = new StockField(18, "preHourTradingChangeRate");
    /** 盘后涨跌幅 自己计算 */
    public static readonly StockField StockField_postHourTradingChangeRate = new StockField(19, "postHourTradingChangeRate");
    /** 每股收益 滚动市盈率 TTM=过去12个月  Last Twelve Month  通过hermes获取 eps */
    public static readonly StockField StockField_ttm_Eps = new StockField(20, "ttmEps");
    /** 量比*（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly StockField StockField_VolumeRatio = new StockField(21, "volumeRatio");
    /** 委比*（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly StockField StockField_BidAskRatio = new StockField(22, "committee");
    /** 下次财报日期 * */
    public static readonly StockField StockField_EarningDate = new StockField(23, "earningDate");
    /** 市盈率* TTM（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly StockField StockField_PeTTM = new StockField(24, "peRate");
    /** 市净率*（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly StockField StockField_PbRate = new StockField(25, "pbRate");
    /** 股息   hermes $ */
    public static readonly StockField StockField_DividePrice = new StockField(26, "dividePrice");
    /** 股息收益率 选股服务自身计算 */
    public static readonly StockField StockField_DivideRate = new StockField(27, "divideRate");
    /** 股票交易市场 */
    public static readonly StockField StockField_Exchange = new StockField(29, "exchange");
    /** 换手率*（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly StockField StockField_TurnoverRate = new StockField(30, "turnoverRate");
    /** 上市时间 */
    public static readonly StockField StockField_ListingDate = new StockField(31, "listingDate");
    /** 市盈率LYR* TTM（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly StockField StockField_LyrPeRate = new StockField(32, "LyrPeRate");
    /** 总股本* */
    public static readonly StockField StockField_Share = new StockField(33, "shares");
    /** 上市价格* */
    public static readonly StockField StockField_ListingPrice = new StockField(34, "listingPrice");
    /** 交易币种* */
    public static readonly StockField StockField_TradeCurrency = new StockField(35, "tradeCurrency");
    /** 最新价-发行价* */
    public static readonly StockField StockField_DiffBetweenLastPriceAndListPrice = new StockField(36, "DiffBetweenLastPriceAndListPrice");
    /** 每股收益 lyr=Last Year Ratio 静态市盈率 */
    public static readonly StockField StockField_lyr_Eps = new StockField(37, "lyrEps");
    /** 未平仓做空量 */
    public static readonly StockField StockField_Open_Short_Interest = new StockField(38, "OpenShortInterest");
    /** 未平仓做空比例 = 未平仓做空量/总股本 */
    public static readonly StockField StockField_Open_Short_Interest_Ratio = new StockField(39, "OpenShortInterestRatio");
    /** 产权比率 = Liability/Equity 总负债/股东 */
    public static readonly StockField StockField_Equity_Ratio = new StockField(40, "EquityRatio");
    /** 权益乘数 = Asset/Equity */
    public static readonly StockField StockField_Equity_Multiplier = new StockField(41, "EquityMultiplier");
    /** 最新股东数 */
    public static readonly StockField StockField_Holder_Nums = new StockField(42, "holderNums");
    /** 最新股东户数增长率 */
    public static readonly StockField StockField_Holder_Nums_Ratio = new StockField(43, "holderRatio");
    /** 户均持股数量 */
    public static readonly StockField StockField_Per_Hold_Nums = new StockField(44, "perHolderNums");
    /** 户均持股金额 */
    public static readonly StockField StockField_Per_Hold_Money = new StockField(45, "perHolderMoney");
    /** 户均持股数半年增长率 */
    public static readonly StockField StockField_HalfYear_Holder_Nums_Ratio = new StockField(46, "HalfYearholderRatio");
    /** 发行时间 - ETF */
    public static readonly StockField StockField_InceptionDate = new StockField(47, "inceptionDate");
    /** 申购费用 - ETF */
    public static readonly StockField StockField_CreationFee = new StockField(48, "creationFee");
    /** 管理费用 - ETF */
    public static readonly StockField StockField_ManagementFee = new StockField(49, "managementFee");
    /** 成分股Top10 占比 - ETF */
    public static readonly StockField StockField_Top10_Composition_Rate = new StockField(50, "Top10CompoRate");
    /** 成分股Top15 占比 - ETF */
    public static readonly StockField StockField_Top15_Composition_Rate = new StockField(51, "Top15CompoRate");
    /** 成分股Top20 占比 - ETF */
    public static readonly StockField StockField_Top20_Composition_Rate = new StockField(52, "Top20CompoRate");
    /** 溢价率(折扣率) - ETF */
    public static readonly StockField StockField_DiscountPremium = new StockField(53, "discountPremium");
    /** 股息率 - ETF */
    public static readonly StockField StockField_dividend_Rate = new StockField(54, "dividendRate");
    /** 资产规模-净值 - ETF */
    public static readonly StockField StockField_Net_Worth_Aum = new StockField(55, "aum");
    /** 资产规模-现价 - ETF */
    public static readonly StockField StockField_assetSize = new StockField(56, "assetSize");
    /** 振幅 */
    public static readonly StockField StockField_Amplitude = new StockField(57, "Amplitude");

    private readonly int index;
    public int Index { get { return index; } }
    private readonly string value;
    public string Value { get { return value; } }

    public StockField(int index, string value)
    {
      this.index = index;
      this.value = value;
    }

    public static IEnumerable<StockField> Values
    {
      get
      {
        yield return StockField_CurPrice;
        yield return StockField_BidPrice;
        yield return StockField_AskPrice;
        yield return StockField_OpenPrice;
        yield return StockField_PreClosePrice;
        yield return StockField_HighPrice;
        yield return StockField_LowPrice;
        yield return StockField_HourTradingPrePrice;
        yield return StockField_HourTradingAfterPrice;
        yield return StockField_Volume;
        yield return StockField_Amount;
        yield return StockField_FloatShare;
        yield return StockField_Week52High;
        yield return StockField_Week52Low;
        yield return StockField_FloatMarketVal;
        yield return StockField_MarketValue;
        yield return StockField_preHourTradingChangeRate;
        yield return StockField_postHourTradingChangeRate;
        yield return StockField_ttm_Eps;
        yield return StockField_VolumeRatio;
        yield return StockField_BidAskRatio;
        yield return StockField_EarningDate;
        yield return StockField_PeTTM;
        yield return StockField_PbRate;
        yield return StockField_DividePrice;
        yield return StockField_DivideRate;
        yield return StockField_Exchange;
        yield return StockField_TurnoverRate;
        yield return StockField_ListingDate;
        yield return StockField_LyrPeRate;
        yield return StockField_Share;
        yield return StockField_ListingPrice;
        yield return StockField_TradeCurrency;
        yield return StockField_DiffBetweenLastPriceAndListPrice;
        yield return StockField_lyr_Eps;
        yield return StockField_Open_Short_Interest;
        yield return StockField_Open_Short_Interest_Ratio;
        yield return StockField_Equity_Ratio;
        yield return StockField_Equity_Multiplier;
        yield return StockField_Holder_Nums;
        yield return StockField_Holder_Nums_Ratio;
        yield return StockField_Per_Hold_Nums;
        yield return StockField_Per_Hold_Money;
        yield return StockField_HalfYear_Holder_Nums_Ratio;
        yield return StockField_InceptionDate;
        yield return StockField_CreationFee;
        yield return StockField_ManagementFee;
        yield return StockField_Top10_Composition_Rate;
        yield return StockField_Top15_Composition_Rate;
        yield return StockField_Top20_Composition_Rate;
        yield return StockField_DiscountPremium;
        yield return StockField_dividend_Rate;
        yield return StockField_Net_Worth_Aum;
        yield return StockField_assetSize;
        yield return StockField_Amplitude;
      }
    }

    public static StockField? getTypeByValue(string value)
    {
      foreach (StockField item in Values)
      {
        if (string.Equals(value, item.Value))
        {
          return item;
        }
      }
      return null;
    }

    public static StockField? getTypeByIndex(int index)
    {
      foreach (StockField item in Values)
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
      foreach (StockField item in Values)
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
      foreach (StockField item in Values)
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

