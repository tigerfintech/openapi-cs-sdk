using System;
namespace TigerOpenAPI.Common.Enum
{
  public class AccumulateField
  {
    /** Change rate* (accurate to 3 decimal places, excess parts will be discarded) For example, fill in the [0.005, 0.01] value range */
    public static readonly AccumulateField AccumulateField_ChangeRate = new AccumulateField(1, "changeRate");
    /** Change value* (accurate to 3 decimal places, excess parts will be discarded) For example, fill in the [0.005, 0.01] value range */
    public static readonly AccumulateField AccumulateField_ChangeValue = new AccumulateField(2, "changeVal");
    /** Total liabilities growth rate */
    public static readonly AccumulateField AccumulateField_TotalLiabilities_Ratio_Annual = new AccumulateField(3, "totalLiabilitiesRatio");
    /** Net asset growth rate */
    public static readonly AccumulateField AccumulateField_TotalCommonEquity_Ratio_Annual = new AccumulateField(4, "totalCommonEquityRatio");
    /** Year-on-year growth rate of earnings per share */
    public static readonly AccumulateField AccumulateField_BasicEps_Ratio_Annual = new AccumulateField(5, "basicEpsRatio");
    /** Year-on-year growth rate of net profit */
    public static readonly AccumulateField AccumulateField_NetIncome_Ratio_Annual = new AccumulateField(6, "netIncomeRatio");
    /** Year-on-year growth rate of operating profit */
    public static readonly AccumulateField AccumulateField_OperatingIncome_Ratio_Annual = new AccumulateField(7, "opeIncomeratio");
    /** Earnings per share */
    public static readonly AccumulateField AccumulateField_Eps = new AccumulateField(8, "eps");
    /** Net asset per share */
    public static readonly AccumulateField AccumulateField_NetAsset_PerShare = new AccumulateField(9, "bookValueshare");
    /** Net profit */
    public static readonly AccumulateField AccumulateField_Net_Income = new AccumulateField(10, "netIncome");
    /** Operating profit */
    public static readonly AccumulateField AccumulateField_Operating_Income = new AccumulateField(11, "operatingIncome");
    /** Operating revenue */
    public static readonly AccumulateField AccumulateField_Total_Revenue = new AccumulateField(12, "total_revenue");
    /** ROE = Return on equity */
    public static readonly AccumulateField AccumulateField_ROE = new AccumulateField(13, "ROE");
    /** ROA = Return on assets */
    public static readonly AccumulateField AccumulateField_ROA = new AccumulateField(14, "ROA");
    /** Gross profit rate */
    public static readonly AccumulateField AccumulateField_GrossProfitRate = new AccumulateField(17, "grossMargin");
    /** Net profit margin */
    public static readonly AccumulateField AccumulateField_NetProfitRate = new AccumulateField(18, "netIncomeMargin");
    /** Total assets */
    public static readonly AccumulateField AccumulateField_TotalAssets = new AccumulateField(19, "totalAssets");
    /** Current ratio */
    public static readonly AccumulateField AccumulateField_CurrentRatio = new AccumulateField(20, "currentRatio");
    /** Quick ratio */
    public static readonly AccumulateField AccumulateField_QuickRatio = new AccumulateField(21, "quickRatio");
    /** Year-on-year growth rate of operating cash flow */
    public static readonly AccumulateField AccumulateField_CashFromOpsRatio = new AccumulateField(22, "cash4OpsRatio");
    /** Cash flow from investing */
    public static readonly AccumulateField AccumulateField_CashFromInvesting = new AccumulateField(23, "cash4Invest");
    /** Cash flow from financing */
    public static readonly AccumulateField AccumulateField_CashFromFinancing = new AccumulateField(24, "cash4Finance");
    /** Debt to asset ratio */
    public static readonly AccumulateField AccumulateField_TotalLiabilitiesToTotalAssets = new AccumulateField(25, "allLiabAndAssets");
    /** Year-on-year growth rate of net income return on equity (T period ROE-T-1 period ROE) / T-1 period ROE * 100% */
    public static readonly AccumulateField AccumulateField_ROE_yearOnYear_Ratio = new AccumulateField(27, "netIncomeYearOnYearRatio");
    /** Operating profit ratio */
    public static readonly AccumulateField AccumulateField_Operating_Profits_Ratio = new AccumulateField(28, "OperatingProfitsRatio");
    /** Operating cash flow */
    public static readonly AccumulateField AccumulateField_CashFromOpsVal = new AccumulateField(29, "cash4OpsVal");

    private readonly int index;
    public int Index { get { return index; } }
    private readonly string value;
    public string Value { get { return value; } }

    public AccumulateField(int index, string value)
    {
      this.index = index;
      this.value = value;
    }

    public static IEnumerable<AccumulateField> Values
    {
      get
      {
        yield return AccumulateField_ChangeRate;
        yield return AccumulateField_ChangeValue;
        yield return AccumulateField_TotalLiabilities_Ratio_Annual;
        yield return AccumulateField_TotalCommonEquity_Ratio_Annual;
        yield return AccumulateField_BasicEps_Ratio_Annual;
        yield return AccumulateField_NetIncome_Ratio_Annual;
        yield return AccumulateField_OperatingIncome_Ratio_Annual;
        yield return AccumulateField_Eps;
        yield return AccumulateField_NetAsset_PerShare;
        yield return AccumulateField_Net_Income;
        yield return AccumulateField_Operating_Income;
        yield return AccumulateField_Total_Revenue;
        yield return AccumulateField_ROE;
        yield return AccumulateField_ROA;
        yield return AccumulateField_GrossProfitRate;
        yield return AccumulateField_NetProfitRate;
        yield return AccumulateField_TotalAssets;
        yield return AccumulateField_CurrentRatio;
        yield return AccumulateField_QuickRatio;
        yield return AccumulateField_CashFromOpsRatio;
        yield return AccumulateField_CashFromInvesting;
        yield return AccumulateField_CashFromFinancing;
        yield return AccumulateField_TotalLiabilitiesToTotalAssets;
        yield return AccumulateField_ROE_yearOnYear_Ratio;
        yield return AccumulateField_Operating_Profits_Ratio;
        yield return AccumulateField_CashFromOpsVal;
      }
    }

    public static AccumulateField? getTypeByValue(string value)
    {
      foreach (AccumulateField item in Values)
      {
        if (string.Equals(value, item.Value))
        {
          return item;
        }
      }
      return null;
    }

    public static AccumulateField? getTypeByIndex(int index)
    {
      foreach (AccumulateField item in Values)
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
      foreach (AccumulateField item in Values)
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
      foreach (AccumulateField item in Values)
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

