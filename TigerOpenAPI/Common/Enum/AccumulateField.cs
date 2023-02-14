using System;
namespace TigerOpenAPI.Common.Enum
{
  public class AccumulateField
  {
    /** 涨跌幅*（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间  */
    public static readonly AccumulateField AccumulateField_ChangeRate = new AccumulateField(1, "changeRate");
    /** 涨跌额*（精确到小数点后 3 位，超出部分会被舍弃）例如填写 [0.005,0.01] 值区间 */
    public static readonly AccumulateField AccumulateField_ChangeValue = new AccumulateField(2, "change");
    /** 总负债增长率 */
    public static readonly AccumulateField AccumulateField_TotalLiabilities_Ratio_Annual = new AccumulateField(3, "totalLiabilitiesRatio");
    /** 净资产增长率 */
    public static readonly AccumulateField AccumulateField_TotalCommonEquity_Ratio_Annual = new AccumulateField(4, "totalCommonEquityRatio");
    /** 每股收益同比增长率 */
    public static readonly AccumulateField AccumulateField_BasicEps_Ratio_Annual = new AccumulateField(5, "basicEpsRatio");
    /** 净利润同比增长率 */
    public static readonly AccumulateField AccumulateField_NetIncome_Ratio_Annual = new AccumulateField(6, "netIncomeRatio");
    /** 营业利润同比增长率 */
    public static readonly AccumulateField AccumulateField_OperatingIncome_Ratio_Annual = new AccumulateField(7, "opeIncomeratio");
    /** 每股收益 */
    public static readonly AccumulateField AccumulateField_Eps = new AccumulateField(8, "eps");
    /** 每股净资产 */
    public static readonly AccumulateField AccumulateField_NetAsset_PerShare = new AccumulateField(9, "bookValueshare");
    /** 净利润 */
    public static readonly AccumulateField AccumulateField_Net_Income = new AccumulateField(10, "netIncome");
    /** 营业利润 */
    public static readonly AccumulateField AccumulateField_Operating_Income = new AccumulateField(11, "operatingIncome");
    /** 营业收入 */
    public static readonly AccumulateField AccumulateField_Total_Revenue = new AccumulateField(12, "total_revenue");
    /** ROE = 资产回报率 */
    public static readonly AccumulateField AccumulateField_ROE = new AccumulateField(13, "ROE");
    /** ROA =净资产收益率 */
    public static readonly AccumulateField AccumulateField_ROA = new AccumulateField(14, "ROA");
    /** 股息   hermes $ */
    public static readonly AccumulateField AccumulateField_DividePrice = new AccumulateField(15, "dividePrice");
    /** 股息收益率 选股服务自身计算 */
    public static readonly AccumulateField AccumulateField_DivideRate = new AccumulateField(16, "divideRate");
    /** 毛利率 */
    public static readonly AccumulateField AccumulateField_GrossProfitRate = new AccumulateField(17, "grossMargin");
    /** 净利率* */
    public static readonly AccumulateField AccumulateField_NetProfitRate = new AccumulateField(18, "netIncomeMargin");
    /** 总资产* */
    public static readonly AccumulateField AccumulateField_TotalAssets = new AccumulateField(19, "totalAssets");
    /** 流动比率 */
    public static readonly AccumulateField AccumulateField_CurrentRatio = new AccumulateField(20, "currentRatio");
    /** 速动比率 */
    public static readonly AccumulateField AccumulateField_QuickRatio = new AccumulateField(21, "quickRatio");
    /** 经营现金流 */
    public static readonly AccumulateField AccumulateField_CashFromOps = new AccumulateField(22, "cash4Ops");
    /** 投资现金流 */
    public static readonly AccumulateField AccumulateField_CashFromInvesting = new AccumulateField(23, "cash4Invest");
    /** 筹资现金流 */
    public static readonly AccumulateField AccumulateField_CashFromFinancing = new AccumulateField(24, "cash4Finance");
    /** 资产负债率 */
    public static readonly AccumulateField AccumulateField_TotalLiabilitiesToTotalAssets = new AccumulateField(25, "allLiabAndAssets");
    /** 经营现金流同比增长率; （T期CFO-T-1期CFO）/T-1期CFO *100%  */
    public static readonly AccumulateField AccumulateField_CashFromOps_yearOnYear_Ratio = new AccumulateField(26, "cash4OpsYearOnYearRatio");
    /** 净资产收益率ROE同比增长率  （T期ROE-T-1期ROE）/T-1期ROE *100%*/
    public static readonly AccumulateField AccumulateField_ROE_yearOnYear_Ratio = new AccumulateField(27, "netIncomeYearOnYearRatio");

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
        yield return AccumulateField_DividePrice;
        yield return AccumulateField_DivideRate;
        yield return AccumulateField_GrossProfitRate;
        yield return AccumulateField_NetProfitRate;
        yield return AccumulateField_TotalAssets;
        yield return AccumulateField_CurrentRatio;
        yield return AccumulateField_QuickRatio;
        yield return AccumulateField_CashFromOps;
        yield return AccumulateField_CashFromInvesting;
        yield return AccumulateField_CashFromFinancing;
        yield return AccumulateField_TotalLiabilitiesToTotalAssets;
        yield return AccumulateField_CashFromOps_yearOnYear_Ratio;
        yield return AccumulateField_ROE_yearOnYear_Ratio;
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

