using System;
namespace TigerOpenAPI.Common.Enum
{
  // scanner base stock field
  public class StockField
  {
    /** Latest price* (accurate to 3 decimal places, the excess will be discarded) For example, fill in the value range of [10,20] */
    public static readonly StockField StockField_CurPrice = new StockField(2, "latestPrice");
    /** Purchase price (accurate to 3 decimal places, the excess will be discarded) For example, fill in the value range [10,20] */
    public static readonly StockField StockField_BidPrice = new StockField(3, "bidPrice");
    /** Selling price (accurate to 3 decimal places, the excess will be discarded) For example, fill in the value range [10,20] */
    public static readonly StockField StockField_AskPrice = new StockField(4, "askPrice");
    /** Today's offer price (accurate to 3 decimal places, the excess will be discarded) For example, fill in the value range [10,20] */
    public static readonly StockField StockField_OpenPrice = new StockField(5, "open");
    /** Yesterday's closing price (accurate to 3 decimal places, the excess will be discarded) For example, fill in the value range [10,20] */
    public static readonly StockField StockField_PreClosePrice = new StockField(6, "preClose");
    /** Highest price */
    public static readonly StockField StockField_HighPrice = new StockField(7, "high");
    /** Lowest price */
    public static readonly StockField StockField_LowPrice = new StockField(8, "low");
    /** Pre-market price* (accurate to 3 decimal places, the excess will be discarded) For example, fill in the [10,20] value range */
    public static readonly StockField StockField_HourTradingPrePrice = new StockField(9, "hourTradingPrePrice");
    /** After-hours price* (accurate to 3 decimal places, the excess will be discarded) For example, fill in the [10,20] value range */
    public static readonly StockField StockField_HourTradingAfterPrice = new StockField(10, "hourTradingAfterPrice");
    /** Volume* */
    public static readonly StockField StockField_Volume = new StockField(11, "volume");
    /** trading amount* */
    public static readonly StockField StockField_Amount = new StockField(12, "amount");
    /** Circulating share capital* */
    public static readonly StockField StockField_FloatShare = new StockField(13, "floatShares");
    /** 52 week maximum price* */
    public static readonly StockField StockField_Week52High = new StockField(14, "week52High");
    /** Lowest price for 52 weeks* */
    public static readonly StockField StockField_Week52Low = new StockField(15, "week52Low");
    /** Market value* FloatMarketVal calculates FloatShare* current price */
    public static readonly StockField StockField_FloatMarketVal = new StockField(16, "floatMarketCap");
    /** total market value * MarketVal shares * current price */
    public static readonly StockField StockField_MarketValue = new StockField(17, "marketValue");
    /** Pre-market price rise and fall (curPrice-pre-market left close) self-calculated latest price-close / close */
    public static readonly StockField StockField_preHourTradingChangeRate = new StockField(18, "preHourTradingChangeRate");
    /** Calculate the after-hours rise and fall by yourself */
    public static readonly StockField StockField_postHourTradingChangeRate = new StockField(19, "postHourTradingChangeRate");
    /** Earnings per share rolling price-earnings ratio TTM=last 12 months Last Twelve Month Get eps through hermes */
    public static readonly StockField StockField_ttm_Eps = new StockField(20, "ttmEps");
    /** Quantity ratio* (accurate to 3 digits after the decimal point, the excess will be discarded) For example, fill in the value range [0.005,0.01] */
    public static readonly StockField StockField_VolumeRatio = new StockField(21, "volumeRatio");
    /** Commission ratio* (accurate to 3 digits after the decimal point, the excess will be discarded) For example, fill in the value range [0.005,0.01] */
    public static readonly StockField StockField_BidAskRatio = new StockField(22, "committee");
    /** Next financial report date * */
    public static readonly StockField StockField_EarningDate = new StockField(23, "earningDate");
    /** Price-earnings ratio* TTM (accurate to 3 decimal places, the excess will be discarded) For example, fill in the value range [0.005,0.01] */
    public static readonly StockField StockField_PeTTM = new StockField(24, "peRate");
    /** Dividend hermes $ */
    public static readonly StockField StockField_DividePrice = new StockField(26, "dividePriceVal");
    /** Dividend yield is calculated by the stock selection service itself */
    public static readonly StockField StockField_DivideRate = new StockField(27, "divideRateVal");
    /** stock market */
    public static readonly StockField StockField_Exchange = new StockField(29, "exchange");
    /** Turnover rate* (accurate to 3 decimal places, the excess will be discarded) For example, fill in the value range [0.005,0.01] */
    public static readonly StockField StockField_TurnoverRate = new StockField(30, "turnoverRate");
    /** Time to market */
    public static readonly StockField StockField_ListingDate = new StockField(31, "listingDate");
    /** Total share capital* */
    public static readonly StockField StockField_Share = new StockField(33, "shares");
    /** listing price* */
    public static readonly StockField StockField_ListingPrice = new StockField(34, "listingPrice");
    /** Latest price - issue price* */
    public static readonly StockField StockField_DiffBetweenLastPriceAndListPrice = new StockField(36, "DiffBetweenLastPriceAndListPrice");
    /** Earnings per share lyr=Last Year Ratio static price-earnings ratio */
    public static readonly StockField StockField_lyr_Eps = new StockField(37, "lyrEps");
    /** Open short position */
    public static readonly StockField StockField_Open_Short_Interest = new StockField(38, "OpenShortInterestVal");
    /** Open short position ratio = open short position volume / total equity */
    public static readonly StockField StockField_Open_Short_Interest_Ratio = new StockField(39, "OpenShortInterestRatio");
    /** Equity Ratio = Liability/Equity Total Liabilities/Shareholders */
    public static readonly StockField StockField_Equity_Ratio = new StockField(40, "totalDebtToEquity");
    /** Equity multiplier = Asset/Equity */
    public static readonly StockField StockField_Equity_Multiplier = new StockField(41, "totalLiabilitiesToTotalAssets");
    /** The latest number of shareholders */
    public static readonly StockField StockField_Holder_Nums = new StockField(42, "holderNums");
    /** The latest growth rate of the number of shareholders */
    public static readonly StockField StockField_Holder_Nums_Ratio = new StockField(43, "holderRatio");
    /** The average number of shares held by each account */
    public static readonly StockField StockField_Per_Hold_Nums = new StockField(44, "perHolderNums");
    /** The average amount of shares held by each account */
    public static readonly StockField StockField_Per_Hold_Money = new StockField(45, "perHolderMoney");
    /** Semi-annual growth rate of the average number of shares held by each account */
    public static readonly StockField StockField_HalfYear_Holder_Nums_Ratio = new StockField(46, "HalfYearholderRatio");
    /** Issue time - ETF */
    public static readonly StockField StockField_InceptionDate = new StockField(47, "inceptionDate");
    /** Purchase fee - ETF */
    public static readonly StockField StockField_CreationFee = new StockField(48, "creationFee");
    /** Management Fee - ETF */
    public static readonly StockField StockField_ManagementFee = new StockField(49, "managementFee");
    /** Proportion of top 10 constituent stocks - ETF */
    public static readonly StockField StockField_Top10_Composition_Rate = new StockField(50, "Top10CompoRate");
    /** Proportion of Top 15 Constituent Stocks - ETF */
    public static readonly StockField StockField_Top15_Composition_Rate = new StockField(51, "Top15CompoRate");
    /** Proportion of Top 20 Constituent Stocks - ETF */
    public static readonly StockField StockField_Top20_Composition_Rate = new StockField(52, "Top20CompoRate");
    /** Premium rate (discount rate) - ETF */
    public static readonly StockField StockField_DiscountPremium = new StockField(53, "discountPremium");
    /** asset size - net worth - ETF */
    public static readonly StockField StockField_Net_Worth_Aum = new StockField(55, "aum");
    /** Asset size - current price - ETF */
    public static readonly StockField StockField_assetSize = new StockField(56, "assetSize");
    /** Amplitude */
    public static readonly StockField StockField_Amplitude = new StockField(57, "Amplitude");
    /** Pre-market change rate */
    public static readonly StockField StockField_Pre_ChangeRate = new StockField(58, "preChangeRate");
    /** Intraday change rate */
    public static readonly StockField StockField_current_ChangeRate = new StockField(59, "curChangeRate");
    /** After-market change rate */
    public static readonly StockField StockField_Post_ChangeRate = new StockField(60, "postChangeRate");
    /** Component change - ETF */
    public static readonly StockField StockField_ETF_LastHoldingChangeDay = new StockField(61, "LastHoldingChangeDay");
    /** Holding count - ETF */
    public static readonly StockField StockField_ETF_HoldingCount = new StockField(62, "etfHoldingCount");
    /** Net income without cycle */
    public static readonly StockField StockField_Net_Income = new StockField(63, "netIncomeVal");

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
        yield return StockField_DividePrice;
        yield return StockField_DivideRate;
        yield return StockField_Exchange;
        yield return StockField_TurnoverRate;
        yield return StockField_ListingDate;
        yield return StockField_Share;
        yield return StockField_ListingPrice;
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
        yield return StockField_Net_Worth_Aum;
        yield return StockField_assetSize;
        yield return StockField_Amplitude;
        yield return StockField_Pre_ChangeRate;
        yield return StockField_current_ChangeRate;
        yield return StockField_Post_ChangeRate;
        yield return StockField_ETF_LastHoldingChangeDay;
        yield return StockField_ETF_HoldingCount;
        yield return StockField_Net_Income;
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

