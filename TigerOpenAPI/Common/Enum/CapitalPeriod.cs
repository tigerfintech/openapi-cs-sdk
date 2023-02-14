using System;
namespace TigerOpenAPI.Common.Enum
{
  public class CapitalPeriod
  {
    public static readonly CapitalPeriod intraday = new CapitalPeriod("intraday");
    public static readonly CapitalPeriod day = new CapitalPeriod("day");
    public static readonly CapitalPeriod week = new CapitalPeriod("week");
    public static readonly CapitalPeriod month = new CapitalPeriod("month");
    public static readonly CapitalPeriod year = new CapitalPeriod("year");
    public static readonly CapitalPeriod quarter = new CapitalPeriod("quarter");
    public static readonly CapitalPeriod halfayear = new CapitalPeriod("6month");

    public static IEnumerable<CapitalPeriod> Values
    {
      get
      {
        yield return intraday;
        yield return day;
        yield return week;
        yield return month;
        yield return year;
        yield return quarter;
        yield return halfayear;
      }
    }
    private readonly string value;
    public string Value { get { return value; } }

    CapitalPeriod(string value)
    {
      this.value = value;
    }
  }
}

