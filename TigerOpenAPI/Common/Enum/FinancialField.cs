using System;
namespace TigerOpenAPI.Common.Enum
{
  public class FinancialField
  {
    /** Gross profit margin * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_GrossProfitRate = new FinancialField(1, "grossMarginVal");
    /** Net profit margin * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_NetProfitRate = new FinancialField(2, "netIncomeMarginVal");
    /** Non-GAAP net profit margin * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_EarningsFromContOpsMargin = new FinancialField(3, "earningsFromContOpsMargin");
    /** Long-term debt to equity ratio */
    public static readonly FinancialField FinancialField_LongTermDebtToEquity = new FinancialField(5, "ltDebtToEquity");
    /** EBIT/Interest expense ratio */
    public static readonly FinancialField FinancialField_EbitToInterestExp = new FinancialField(6, "ebitToInterestExp");
    /** Total asset turnover ratio * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_TotalAssetTurnover = new FinancialField(8, "totalAssetTurnover");
    /** Accounts receivable turnover ratio */
    public static readonly FinancialField FinancialField_AccountsReceivableTurnover = new FinancialField(9, "accountsReceivableTurnover");
    /** Inventory turnover ratio * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_InventoryTurnover = new FinancialField(10, "inventoryTurnover");
    /** Current ratio * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_CurrentRatio = new FinancialField(11, "currentRatioVal");
    /** Quick ratio * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_QuickRatio = new FinancialField(12, "quickRatioVal");
    /** Return on total assets (ROA) TTM * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_ROATTM = new FinancialField(13, "roa");
    /** Return on equity (ROE) * (accurate to 3 decimal places, exceeding parts will be discarded) For example, fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_ReturnOnEquityRate = new FinancialField(14, "roe");
    /** Year-over-year growth rate of total revenues or sales */
    public static readonly FinancialField FinancialField_TotalRevenues1YrGrowth = new FinancialField(15, "totalRevenues1YrGrowth");
    /** Year-over-year growth rate of gross profit */
    public static readonly FinancialField FinancialField_GrossProfit1YrGrowth = new FinancialField(16, "grossProfit1YrGrowth");
    /** Year-over-year growth rate of net income */
    public static readonly FinancialField FinancialField_NetIncome1YrGrowth = new FinancialField(17, "netIncome1YrGrowth");
    /** Year-over-year growth rate of accounts receivable */
    public static readonly FinancialField FinancialField_AccountsReceivable1YrGrowth = new FinancialField(18, "accountsReceivable1YrGrowth");
    /** Inventory 1-year growth rate */
    public static readonly FinancialField FinancialField_Inventory1YrGrowth = new FinancialField(19, "inventory1YrGrowth");
    /** Total assets 1-year growth rate */
    public static readonly FinancialField FinancialField_TotalAssets1YrGrowth = new FinancialField(20, "totalAssets1YrGrowth");
    /** Tangible book value 1-year growth rate */
    public static readonly FinancialField FinancialField_TangibleBookValue1YrGrowth = new FinancialField(21, "tangibleBookValue1YrGrowth");
    /** Cash flow from operations 1-year growth rate = Cash flow from operations year-over-year growth rate */
    public static readonly FinancialField FinancialField_CashFromOperations1YrGrowth = new FinancialField(22, "cashFromOperations1YrGrowth");
    /** Capital expenditures 1-year growth rate */
    public static readonly FinancialField FinancialField_CapitalExpenditures1YrGrowth = new FinancialField(23, "capitalExpenditures1YrGrowth");
    /** Total revenues 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_TotalRevenues3YrCagr = new FinancialField(24, "totalRevenues3YrCagr");
    /** Gross profit margin 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_GrossProfit3YrCagr = new FinancialField(25, "grossProfit3YrCagr");
    /** Net income 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_NetIncome3YrCagr = new FinancialField(26, "netIncome3YrCagr");
    /** Accounts receivable 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_AccountsReceivable3YrCagr = new FinancialField(27, "accountsReceivable3YrCagr");
    /** Inventory 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_Inventory3YrCagr = new FinancialField(28, "inventory3YrCagr");
    /** Total assets 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_TotalAssets3YrCagr = new FinancialField(29, "totalAssets3YrCagr");
    /** Tangible book value 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_TangibleBookValue3YrCagr = new FinancialField(30, "tangibleBookValue3YrCagr");
    /** Cash flow from operations 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_CashFromOps3YrCagr = new FinancialField(31, "cashFromOps3YrCagr");
    /** Capital expenditures 3-year compound annual growth rate (CAGR) */
    public static readonly FinancialField FinancialField_CapitalExpenditures3YrCagr = new FinancialField(32, "capitalExpenditures3YrCagr");
    /** Net income */
    public static readonly FinancialField FinancialField_NetIncomeToCompany = new FinancialField(33, "netIncomeToCompany");
    /** Cash flow from operations */
    public static readonly FinancialField FinancialField_CashFromOperations = new FinancialField(34, "cashFromOps");
    /** Cash flow from investing */
    public static readonly FinancialField FinancialField_CashFromInvesting = new FinancialField(35, "cashFromInvesting");
    /** Cash flow from financing */
    public static readonly FinancialField FinancialField_CashFromFinancing = new FinancialField(36, "cashFromFinancing");
    /** Net income 2-year compound annual growth rate */
    public static readonly FinancialField FinancialField_NormalizedNetIncome2YrCagr = new FinancialField(37, "netIncome2YrCagr");
    /** Total revenues 2-year compound annual growth rate */
    public static readonly FinancialField FinancialField_TotalRevenues2YrCagr = new FinancialField(38, "totalRevenues2YrCagr");
    /** Net income 5-year compound annual growth rate */
    public static readonly FinancialField FinancialField_NetIncome5YrCagr = new FinancialField(39, "netIncome5YrCagr");
    /** Total revenues 5-year compound annual growth rate */
    public static readonly FinancialField FinancialField_TotalRevenues5YrCagr = new FinancialField(40, "totalRevenues5YrCagr");
    /** Total assets */
    public static readonly FinancialField FinancialField_TotalAssets = new FinancialField(41, "totalAssetsVal");
    /** Fixed asset turnover (accurate to 3 decimal places, any excess will be discarded) e.g. fill in the value range [0.005, 0.01] */
    public static readonly FinancialField FinancialField_FixedAssetTurnover = new FinancialField(42, "fixedAssetTurnover");
    /** Operating income */
    public static readonly FinancialField FinancialField_OperatingIncome = new FinancialField(43, "operatingIncomeVal");
    /** Total revenue */
    public static readonly FinancialField FinancialField_TotalRevenue = new FinancialField(44, "totalRevenue");
    /** Price-to-earnings ratio LYR PE = price-to-earnings ratio */
    public static readonly FinancialField FinancialField_LYR_PE = new FinancialField(45, "LyrPE");
    /** Price-to-earnings ratio TTM PE = price-to-earnings ratio */
    public static readonly FinancialField FinancialField_TTM_PE = new FinancialField(46, "ttmPE");
    /** Price-to-sales ratio LYR PS = Price-to-sales Ratio */
    public static readonly FinancialField FinancialField_LYR_PS = new FinancialField(47, "LyrPS");
    /** Price-to-sales ratio TTM PS = Price-to-sales Ratio */
    public static readonly FinancialField FinancialField_TTM_PS = new FinancialField(48, "ttmPS");
    /** Large inflow amount today */
    public static readonly FinancialField FinancialField_LargeInflowAmountToday = new FinancialField(49, "largeInflowAmountToday");
    /** Percentage of large inflow amount today */
    public static readonly FinancialField FinancialField_LargeInflowAmountTodayPre = new FinancialField(50, "largeInflowAmountTodayPre");
    /** Short interest */
    public static readonly FinancialField FinancialField_ShortInterest = new FinancialField(51, "shortInterest");
    /** Short interest ratio */
    public static readonly FinancialField FinancialField_ShortInterestPre = new FinancialField(52, "shortInterestPre");
    /** Hong Kong Stock Connect holding ratio = Shenzhen-Hong Kong Stock Connect holding ratio = Shanghai-Hong Kong Stock Connect holding ratio */
    public static readonly FinancialField FinancialField_HK_StockConnectRate = new FinancialField(53, "hkStockConnectRate");
    /** Shanghai Stock Connect holding ratio */
    public static readonly FinancialField FinancialField_SH_StockConnectRate = new FinancialField(54, "shStockConnectRate");
    /** Shenzhen Stock Connect holding ratio */
    public static readonly FinancialField FinancialField_SZ_StockConnectRate = new FinancialField(55, "szStockConnectRate");
    /** Operating profit ratio */
    public static readonly FinancialField FinancialField_Operating_Profits_Rate = new FinancialField(56, "operatingProfitsRate");
    /** Hong Kong Stock Connect (Shanghai) net inflow */
    public static readonly FinancialField FinancialField_HK_StockShConnectInflow = new FinancialField(57, "hkStockShConnectInflow");
    /** Hong Kong Stock Connect (Shenzhen) net inflow */
    public static readonly FinancialField FinancialField_HK_StockSzConnectInflow = new FinancialField(58, "hkStockSzConnectInflow");
    /** Shanghai Stock Connect net inflow */
    public static readonly FinancialField FinancialField_SH_StockConnectInflow = new FinancialField(59, "shStockConnectInflow");
    /** Shenzhen Stock Connect net inflow */
    public static readonly FinancialField FinancialField_SZ_StockConnectInflow = new FinancialField(60, "szStockConnectInflow");
    /** Annualized return since listing - ETF */
    public static readonly FinancialField FinancialField_ListingAnnualReturn = new FinancialField(61, "listingAnnualReturn");
    /** Annualized return in the past year - ETF */
    public static readonly FinancialField FinancialField_LstYearAnnualReturn = new FinancialField(62, "lstYearAnnualReturn");
    /** Annualized return in the past 2 years - ETF */
    public static readonly FinancialField FinancialField_Lst2YearAnnualReturn = new FinancialField(63, "lst2YearAnnualReturn");
    /** Annualized return in the past 5 years - ETF */
    public static readonly FinancialField FinancialField_Lst5YearAnnualReturn = new FinancialField(64, "lst5YearAnnualReturn");
    /** Annualized volatility since listing - ETF */
    public static readonly FinancialField FinancialField_ListingAnnualVolatility = new FinancialField(65, "listingAnnualVolatility");
    /** Annualized volatility in the past year - ETF */
    public static readonly FinancialField FinancialField_LstYearAnnualVolatility = new FinancialField(66, "lstYearAnnualVolatility");
    /** Annualized volatility in the past 2 years - ETF */
    public static readonly FinancialField FinancialField_Lst2YearAnnualVolatility = new FinancialField(67, "lst2YearAnnualVolatility");
    /** Annualized volatility in the past 5 years - ETF */
    public static readonly FinancialField FinancialField_Lst5YearAnnualVolatility = new FinancialField(68, "lst5YearAnnualVolatility");
    /** Price-to-book ratio LYR PB = price-to-book ratio */
    public static readonly FinancialField FinancialField_LYR_PB = new FinancialField(69, "LyrPB");
    /** Price-to-book ratio TTM PB = price-to-book ratio */
    public static readonly FinancialField FinancialField_TTM_PB = new FinancialField(70, "ttmPB");

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
        yield return FinancialField_LongTermDebtToEquity;
        yield return FinancialField_EbitToInterestExp;
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
        yield return FinancialField_LYR_PB;
        yield return FinancialField_TTM_PB;
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

