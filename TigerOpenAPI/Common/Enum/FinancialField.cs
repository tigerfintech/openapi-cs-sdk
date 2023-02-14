using System;
namespace TigerOpenAPI.Common.Enum
{
  public class FinancialField
  {

    /** 毛利率*（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_GrossProfitRate = new FinancialField(1, "grossMargin");
    /** 净利率*（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_NetProfitRate = new FinancialField(2, "netIncomeMargin");
    /** 扣非净利润率  *（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_EarningsFromContOpsMargin = new FinancialField(3, "earningsFromContOpsMargin");
    /** 总负债/股东权益* (单位：元) */
    public static readonly FinancialField FinancialField_TotalDebtToEquity = new FinancialField(4, "totalDebtToEquity");
    /** 长期负债/股东权益 **/
    public static readonly FinancialField FinancialField_LongTermDebtToEquity = new FinancialField(5, "ltDebtToEquity");
    /** EBIT/利息支出 **/
    public static readonly FinancialField FinancialField_EbitToInterestExp = new FinancialField(6, "ebitToInterestExp");
    /** 总负债/总资产 **/
    public static readonly FinancialField FinancialField_TotalLiabilitiesToTotalAssets = new FinancialField(7, "totalLiabilitiesToTotalAssets");
    /** 总资产周转率（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_TotalAssetTurnover = new FinancialField(8, "totalAssetTurnover");
    /** 应收帐款周转率 */
    public static readonly FinancialField FinancialField_AccountsReceivableTurnover = new FinancialField(9, "accountsReceivableTurnover");
    /** 存货周转率（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_InventoryTurnover = new FinancialField(10, "inventoryTurnover");
    /** 流动比率（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_CurrentRatio = new FinancialField(11, "currentRatio");
    /** 速动比率（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_QuickRatio = new FinancialField(12, "quickRatio");
    /** 资产回报率 总资产收益率 *$ TTM（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_ROATTM = new FinancialField(13, "roa");
    /** 净资产收益率 $（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_ReturnOnEquityRate = new FinancialField(14, "roe");
    /** 营业收入一年增长率 或者 营收增长率 */
    public static readonly FinancialField FinancialField_TotalRevenues1YrGrowth = new FinancialField(15, "totalRevenues1YrGrowth");
    /** 毛利润率一年增长率  营业利润增长率 */
    public static readonly FinancialField FinancialField_GrossProfit1YrGrowth = new FinancialField(16, "grossProfit1YrGrowth");
    /** 净利润一年增长率 */
    public static readonly FinancialField FinancialField_NetIncome1YrGrowth = new FinancialField(17, "netIncome1YrGrowth");
    /** 应收帐款一年增长率 */
    public static readonly FinancialField FinancialField_AccountsReceivable1YrGrowth = new FinancialField(18, "accountsReceivable1YrGrowth");
    /** 存货一年增长率 */
    public static readonly FinancialField FinancialField_Inventory1YrGrowth = new FinancialField(19, "inventory1YrGrowth");
    /** 总资产一年增长率 */
    public static readonly FinancialField FinancialField_TotalAssets1YrGrowth = new FinancialField(20, "totalAssets1YrGrowth");
    /** 有形资产一年增长率 */
    public static readonly FinancialField FinancialField_TangibleBookValue1YrGrowth = new FinancialField(21, "tangibleBookValue1YrGrowth");
    /** 经营现金流一年增长率 */
    public static readonly FinancialField FinancialField_CashFromOperations1YrGrowth = new FinancialField(22, "cashFromOperations1YrGrowth");
    /** 资本开支一年增长率 */
    public static readonly FinancialField FinancialField_CapitalExpenditures1YrGrowth = new FinancialField(23, "capitalExpenditures1YrGrowth");
    /** 营业收入三年增长率 或者叫 营收3年复合增长率 */
    public static readonly FinancialField FinancialField_TotalRevenues3YrCagr = new FinancialField(24, "totalRevenues3YrCagr");
    /** 毛利润率三年增长率 */
    public static readonly FinancialField FinancialField_GrossProfit3YrCagr = new FinancialField(25, "grossProfit3YrCagr");
    /** 净利润三年增长率 */
    public static readonly FinancialField FinancialField_NetIncome3YrCagr = new FinancialField(26, "netIncome3YrCagr");
    /** 应收帐款三年增长率 */
    public static readonly FinancialField FinancialField_AccountsReceivable3YrCagr = new FinancialField(27, "accountsReceivable3YrCagr");
    /** 存货三年增长率 */
    public static readonly FinancialField FinancialField_Inventory3YrCagr = new FinancialField(28, "inventory3YrCagr");
    /** 总资产三年增长率 */
    public static readonly FinancialField FinancialField_TotalAssets3YrCagr = new FinancialField(29, "totalAssets3YrCagr");
    /** 有形资产三年增长率 */
    public static readonly FinancialField FinancialField_TangibleBookValue3YrCagr = new FinancialField(30, "tangibleBookValue3YrCagr");
    /** 经营现金流三年增长率 */
    public static readonly FinancialField FinancialField_CashFromOps3YrCagr = new FinancialField(31, "cashFromOps3YrCagr");
    /** 资本开支三年增长率 */
    public static readonly FinancialField FinancialField_CapitalExpenditures3YrCagr = new FinancialField(32, "capitalExpenditures3YrCagr");
    /** 净利润 */
    public static readonly FinancialField FinancialField_NetIncomeToCompany = new FinancialField(33, "netIncomeToCompany");
    /** 经营现金流 */
    public static readonly FinancialField FinancialField_CashFromOperations = new FinancialField(34, "cashFromOps");
    /** 投资现金流 */
    public static readonly FinancialField FinancialField_CashFromInvesting = new FinancialField(35, "cashFromInvesting");
    /** 筹资现金流 */
    public static readonly FinancialField FinancialField_CashFromFinancing = new FinancialField(36, "cashFromFinancing");
    /** 净利润2年复合增长率 */
    public static readonly FinancialField FinancialField_NormalizedNetIncome2YrCagr = new FinancialField(37, "normalizedNetIncome2YrCagr");
    /** 营收2年复合增长率 */
    public static readonly FinancialField FinancialField_TotalRevenues2YrCagr = new FinancialField(38, "totalRevenues2YrCagr");
    /** 净利润5年复合增长率 */
    public static readonly FinancialField FinancialField_NetIncome5YrCagr = new FinancialField(39, "netIncome5YrCagr");
    /** 营收5年复合增长率 */
    public static readonly FinancialField FinancialField_TotalRevenues5YrCagr = new FinancialField(40, "totalRevenues5YrCagr");
    /** 总资产 */
    public static readonly FinancialField FinancialField_TotalAssets = new FinancialField(41, "totalAssets");
    /** 固定资产周转率（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly FinancialField FinancialField_FixedAssetTurnover = new FinancialField(42, "fixedAssetTurnover");
    /** 营业利润 */
    public static readonly FinancialField FinancialField_OperatingIncome = new FinancialField(43, "operatingIncome");
    /** 营业总收入 */
    public static readonly FinancialField FinancialField_TotalRevenue = new FinancialField(44, "totalRevenue");
    /** 市盈率LYR PE =price-to-earnings ratio */
    public static readonly FinancialField FinancialField_LYR_PE = new FinancialField(45, "LyrPE");
    /** 市盈率TTM PE =price-to-earnings ratio */
    public static readonly FinancialField FinancialField_TTM_PE = new FinancialField(46, "ttmPE");
    /** 市销率LYR PS =Price-to-sales Ratio */
    public static readonly FinancialField FinancialField_LYR_PS = new FinancialField(47, "LyrPS");
    /** 市销率TTM PS =Price-to-sales Ratio */
    public static readonly FinancialField FinancialField_TTM_PS = new FinancialField(48, "ttmPS");
    /** 市净率LYR PB =price/book value ratio */
    public static readonly FinancialField FinancialField_LYR_PB = new FinancialField(47, "LyrPB");
    /** 市净率TTM PB =price/book value ratio */
    public static readonly FinancialField FinancialField_TTM_PB = new FinancialField(48, "ttmPB");
    /** 当日主力净流入额 */
    public static readonly FinancialField FinancialField_LargeInflowAmountToday = new FinancialField(49, "largeInflowAmountToday");
    /** 当日主力增仓占比 */
    public static readonly FinancialField FinancialField_LargeInflowAmountTodayPre = new FinancialField(50, "largeInflowAmountTodayPre");
    /** 未平仓做空量 */
    public static readonly FinancialField FinancialField_ShortInterest = new FinancialField(51, "shortInterest");
    /** 未平仓做空比例 */
    public static readonly FinancialField FinancialField_ShortInterestPre = new FinancialField(52, "shortInterestPre");
    /** 港股通持股比例=港股通(深)持股比例=港股通(沪)持股比例 */
    public static readonly FinancialField FinancialField_HK_StockConnectRate = new FinancialField(53, "hkStockConnectRate");
    /** 沪股通持股比例 */
    public static readonly FinancialField FinancialField_SH_StockConnectRate = new FinancialField(54, "shStockConnectRate");
    /** 深股通持股比例 */
    public static readonly FinancialField FinancialField_SZ_StockConnectRate = new FinancialField(55, "szStockConnectRate");
    /** 营业利润占比 */
    public static readonly FinancialField FinancialField_Operating_Profits_Rate = new FinancialField(56, "operatingProfitsRate");
    /** 港股通(沪)净买入额 */
    public static readonly FinancialField FinancialField_HK_StockShConnectInflow = new FinancialField(57, "hkStockShConnectInflow");
    /** 港股通(深)净买入额 */
    public static readonly FinancialField FinancialField_HK_StockSzConnectInflow = new FinancialField(58, "hkStockSzConnectInflow");
    /** 沪股通净买入额 */
    public static readonly FinancialField FinancialField_SH_StockConnectInflow = new FinancialField(59, "shStockConnectInflow");
    /** 深股通净买入额 */
    public static readonly FinancialField FinancialField_SZ_StockConnectInflow = new FinancialField(60, "szStockConnectInflow");
    /** 上市以来年化收益率 ETF */
    public static readonly FinancialField FinancialField_ListingAnnualReturn = new FinancialField(61, "listingAnnualReturn");
    /** 近1年年化收益率  ETF */
    public static readonly FinancialField FinancialField_LstYearAnnualReturn = new FinancialField(62, "lstYearAnnualReturn");
    /** 近2年年化收益率  ETF */
    public static readonly FinancialField FinancialField_Lst2YearAnnualReturn = new FinancialField(63, "lst2YearAnnualReturn");
    /** 近5年年化收益率  ETF */
    public static readonly FinancialField FinancialField_Lst5YearAnnualReturn = new FinancialField(64, "lst5YearAnnualReturn");
    /** 上市以来年化波动率  ETF */
    public static readonly FinancialField FinancialField_ListingAnnualVolatility = new FinancialField(65, "listingAnnualVolatility");
    /** 近1年年化波动率  ETF */
    public static readonly FinancialField FinancialField_LstYearAnnualVolatility = new FinancialField(66, "lstYearAnnualVolatility");
    /** 近2年年化波动率  ETF */
    public static readonly FinancialField FinancialField_Lst2YearAnnualVolatility = new FinancialField(67, "lst2YearAnnualVolatility");
    /** 近5年年化波动率  ETF */
    public static readonly FinancialField FinancialField_Lst5YearAnnualVolatility = new FinancialField(68, "lst5YearAnnualVolatility");

    private readonly int index;
    public int Index { get { return index; } }
    private readonly string value;
    public string Value { get { return value; } }

    public FinancialField(int index, string value)
    {
      this.index = index;
      this.value = value;
    }

    public static IEnumerable<FinancialField> Values
    {
      get
      {

        yield return FinancialField_GrossProfitRate;
        yield return FinancialField_NetProfitRate;
        yield return FinancialField_EarningsFromContOpsMargin;
        yield return FinancialField_TotalDebtToEquity;
        yield return FinancialField_LongTermDebtToEquity;
        yield return FinancialField_EbitToInterestExp;
        yield return FinancialField_TotalLiabilitiesToTotalAssets;
        yield return FinancialField_TotalAssetTurnover;
        yield return FinancialField_AccountsReceivableTurnover;
        yield return FinancialField_InventoryTurnover;
        yield return FinancialField_CurrentRatio;
        yield return FinancialField_QuickRatio;
        yield return FinancialField_ROATTM;
        yield return FinancialField_ReturnOnEquityRate;
        yield return FinancialField_TotalRevenues1YrGrowth;
        yield return FinancialField_GrossProfit1YrGrowth;
        yield return FinancialField_NetIncome1YrGrowth;
        yield return FinancialField_AccountsReceivable1YrGrowth;
        yield return FinancialField_Inventory1YrGrowth;
        yield return FinancialField_TotalAssets1YrGrowth;
        yield return FinancialField_TangibleBookValue1YrGrowth;
        yield return FinancialField_CashFromOperations1YrGrowth;
        yield return FinancialField_CapitalExpenditures1YrGrowth;
        yield return FinancialField_TotalRevenues3YrCagr;
        yield return FinancialField_GrossProfit3YrCagr;
        yield return FinancialField_NetIncome3YrCagr;
        yield return FinancialField_AccountsReceivable3YrCagr;
        yield return FinancialField_Inventory3YrCagr;
        yield return FinancialField_TotalAssets3YrCagr;
        yield return FinancialField_TangibleBookValue3YrCagr;
        yield return FinancialField_CashFromOps3YrCagr;
        yield return FinancialField_CapitalExpenditures3YrCagr;
        yield return FinancialField_NetIncomeToCompany;
        yield return FinancialField_CashFromOperations;
        yield return FinancialField_CashFromInvesting;
        yield return FinancialField_CashFromFinancing;
        yield return FinancialField_NormalizedNetIncome2YrCagr;
        yield return FinancialField_TotalRevenues2YrCagr;
        yield return FinancialField_NetIncome5YrCagr;
        yield return FinancialField_TotalRevenues5YrCagr;
        yield return FinancialField_TotalAssets;
        yield return FinancialField_FixedAssetTurnover;
        yield return FinancialField_OperatingIncome;
        yield return FinancialField_TotalRevenue;
        yield return FinancialField_LYR_PE;
        yield return FinancialField_TTM_PE;
        yield return FinancialField_LYR_PS;
        yield return FinancialField_TTM_PS;
        yield return FinancialField_LYR_PB;
        yield return FinancialField_TTM_PB;
        yield return FinancialField_LargeInflowAmountToday;
        yield return FinancialField_LargeInflowAmountTodayPre;
        yield return FinancialField_ShortInterest;
        yield return FinancialField_ShortInterestPre;
        yield return FinancialField_HK_StockConnectRate;
        yield return FinancialField_SH_StockConnectRate;
        yield return FinancialField_SZ_StockConnectRate;
        yield return FinancialField_Operating_Profits_Rate;
        yield return FinancialField_HK_StockShConnectInflow;
        yield return FinancialField_HK_StockSzConnectInflow;
        yield return FinancialField_SH_StockConnectInflow;
        yield return FinancialField_SZ_StockConnectInflow;
        yield return FinancialField_ListingAnnualReturn;
        yield return FinancialField_LstYearAnnualReturn;
        yield return FinancialField_Lst2YearAnnualReturn;
        yield return FinancialField_Lst5YearAnnualReturn;
        yield return FinancialField_ListingAnnualVolatility;
        yield return FinancialField_LstYearAnnualVolatility;
        yield return FinancialField_Lst2YearAnnualVolatility;
        yield return FinancialField_Lst5YearAnnualVolatility;
      }
    }

    public static FinancialField? getTypeByValue(string value)
    {
      foreach (FinancialField item in Values)
      {
        if (string.Equals(value, item.Value))
        {
          return item;
        }
      }
      return null;
    }

    public static FinancialField? getTypeByIndex(int index)
    {
      foreach (FinancialField item in Values)
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
      foreach (FinancialField item in Values)
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
      foreach (FinancialField item in Values)
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

