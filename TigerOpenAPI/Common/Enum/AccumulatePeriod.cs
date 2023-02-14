using System;
namespace TigerOpenAPI.Common.Enum
{
  public class AccumulatePeriod
  {

    /** 近五分钟 */
    public static readonly AccumulatePeriod Five_Minutes = new AccumulatePeriod(0, "_5_min", "changeRate");
    /** 近五天 */
    public static readonly AccumulatePeriod Five_Days = new AccumulatePeriod(1, "_5_days", "changeRate");
    /** 近10日 */
    public static readonly AccumulatePeriod Ten_Days = new AccumulatePeriod(2, "_10_days", "changeRate");
    /** 近20日 */
    public static readonly AccumulatePeriod Twenty_Days = new AccumulatePeriod(3, "_20_days", "changeRate");
    /** 年初至今 */
    public static readonly AccumulatePeriod Beginning_Of_The_Year_To_Now = new AccumulatePeriod(4, "_1_year", "changeRate");
    /** 近半年 */
    public static readonly AccumulatePeriod Half_Year = new AccumulatePeriod(5, "_half_year", "changeRate");
    /** 近一年 */
    public static readonly AccumulatePeriod Last_Year = new AccumulatePeriod(6, "_last_year", "changeRate");
    /** 近两年 */
    public static readonly AccumulatePeriod Last_Two_Year = new AccumulatePeriod(7, "_last_two_year", "changeRate");
    /** 近五年 */
    public static readonly AccumulatePeriod Last_Five_Year = new AccumulatePeriod(8, "_last_five_year", "changeRate");
    /** 上市至今 */
    public static readonly AccumulatePeriod Listing_Date_To_Now = new AccumulatePeriod(9, "_ListDateToNow", "changeRate");
    /** 年度范围 */
    public static readonly AccumulatePeriod ANNUAL = new AccumulatePeriod(10, "_annu",
            "totalLiabilitiesRatio,totalCommonEquityRatio,basicEpsRatio,netIncomeRatio,opeIncomeratio,eps,bookValueshare,netIncome,operatingIncome,total_revenue,ROE,ROA,dividePrice,divideRate,grossMargin,netIncomeMargin,totalAssets,currentRatio,quickRatio,cash4Ops,cash4Invest,cash4Finance,allLiabAndAssets,cash4Ops,netIncomeYearOnYearRatio,cash4OpsYearOnYearRatio");
    /** 一季度报 */
    public static readonly AccumulatePeriod QUARTERLY = new AccumulatePeriod(11, "_quart",
            "totalLiabilitiesRatio,totalCommonEquityRatio,basicEpsRatio,netIncomeRatio,opeIncomeratio,eps,bookValueshare,netIncome,operatingIncome,total_revenue,ROE,ROA,dividePrice,divideRate,grossMargin,netIncomeMargin,totalAssets,currentRatio,quickRatio,cash4Ops,cash4Invest,cash4Finance,allLiabAndAssets,cash4Ops,netIncomeYearOnYearRatio,cash4OpsYearOnYearRatio");
    /** 三季度报 */
    public static readonly AccumulatePeriod QUARTERLY_Recent_Third = new AccumulatePeriod(12, "_3_ytd",
            "totalLiabilitiesRatio,totalCommonEquityRatio,basicEpsRatio,netIncomeRatio,opeIncomeratio,eps,bookValueshare,netIncome,operatingIncome,total_revenue,ROE,ROA,dividePrice,divideRate,grossMargin,netIncomeMargin,totalAssets,currentRatio,quickRatio,cash4Ops,cash4Invest,cash4Finance,allLiabAndAssets,cash4Ops,netIncomeYearOnYearRatio,cash4OpsYearOnYearRatio");
    /** 中报 */
    public static readonly AccumulatePeriod SEMIANNUAL = new AccumulatePeriod(13, "_semiAnnu",
            "totalLiabilitiesRatio,totalCommonEquityRatio,basicEpsRatio,netIncomeRatio,opeIncomeratio,eps,bookValueshare,netIncome,operatingIncome,total_revenue,ROE,ROA,dividePrice,divideRate,grossMargin,netIncomeMargin,totalAssets,currentRatio,quickRatio,cash4Ops,cash4Invest,cash4Finance,allLiabAndAssets,cash4Ops,netIncomeYearOnYearRatio,cash4OpsYearOnYearRatio");

    private readonly int value;
    public int Value { get { return value; } }
    private readonly string suffix;
    public string Suffix { get { return suffix; } }
    private readonly string range;
    public string Range { get { return range; } }

    public AccumulatePeriod(int value, string suffix, string range)
    {
      this.value = value;
      this.suffix = suffix;
      this.range = range;
    }

    public static IEnumerable<AccumulatePeriod> Values
    {
      get
      {

        yield return Five_Minutes;
        yield return Five_Days;
        yield return Ten_Days;
        yield return Twenty_Days;
        yield return Beginning_Of_The_Year_To_Now;
        yield return Half_Year;
        yield return Last_Year;
        yield return Last_Two_Year;
        yield return Last_Five_Year;
        yield return Listing_Date_To_Now;
        yield return ANNUAL;
        yield return QUARTERLY;
        yield return QUARTERLY_Recent_Third;
        yield return SEMIANNUAL;
      }
    }


    public static AccumulatePeriod? getTypeByValue(int value)
    {
      foreach (AccumulatePeriod item in Values)
      {
        if (item.Value == value)
        {
          return item;
        }
      }
      return null;
    }


    public static string? getSuffixByValue(int value)
    {
      foreach (AccumulatePeriod item in Values)
      {
        if (item.Value == value)
        {
          return item.Suffix;
        }
      }
      return null;
    }
  }
}

