using System;
namespace TigerOpenAPI.Common.Enum
{
  public class FutureKType
  {
    public static readonly FutureKType min1 = new FutureKType("min");
    public static readonly FutureKType min2 = new FutureKType("2min");
    public static readonly FutureKType min3 = new FutureKType("3min");
    public static readonly FutureKType min5 = new FutureKType("5min");
    public static readonly FutureKType min10 = new FutureKType("10min");
    public static readonly FutureKType min15 = new FutureKType("15min");
    public static readonly FutureKType min30 = new FutureKType("30min");
    public static readonly FutureKType min45 = new FutureKType("45min");
    public static readonly FutureKType min60 = new FutureKType("60min");
    public static readonly FutureKType hour2 = new FutureKType("2hour");
    public static readonly FutureKType hour3 = new FutureKType("3hour");
    public static readonly FutureKType hour4 = new FutureKType("4hour");
    public static readonly FutureKType hour6 = new FutureKType("6hour");
    public static readonly FutureKType day = new FutureKType("day");
    public static readonly FutureKType week = new FutureKType("week");
    public static readonly FutureKType month = new FutureKType("month");

    public static IEnumerable<FutureKType> Values
    {
      get
      {
        yield return min1;
        yield return min2;
        yield return min3;
        yield return min5;
        yield return min10;
        yield return min15;
        yield return min30;
        yield return min45;
        yield return min60;
        yield return hour2;
        yield return hour3;
        yield return hour4;
        yield return hour6;
        yield return day;
        yield return week;
        yield return month;
      }
    }
    private readonly string value;
    public string Value { get { return value; } }

    FutureKType(string value)
    {
      this.value = value;
    }
  }
}

