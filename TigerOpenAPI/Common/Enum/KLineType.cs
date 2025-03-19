using System;
namespace TigerOpenAPI.Common.Enum
{
  public class KLineType
  {
    public static readonly KLineType day = new KLineType("day");
    public static readonly KLineType week = new KLineType("week");
    public static readonly KLineType month = new KLineType("month");
    public static readonly KLineType year = new KLineType("year");
    public static readonly KLineType min1 = new KLineType("1min");
    public static readonly KLineType min3 = new KLineType("3min");
    public static readonly KLineType min5 = new KLineType("5min");
    public static readonly KLineType min15 = new KLineType("15min");
    public static readonly KLineType min30 = new KLineType("30min");
    public static readonly KLineType min60 = new KLineType("60min");
    public static readonly KLineType min120 = new KLineType("120min");
    public static readonly KLineType min240 = new KLineType("240min");

    public static IEnumerable<KLineType> Values
    {
      get
      {
        yield return day;
        yield return week;
        yield return month;
        yield return year;
        yield return min1;
        yield return min3;
        yield return min5;
        yield return min15;
        yield return min30;
        yield return min60;
        yield return min120;
        yield return min240;
      }
    }
    private readonly string value;
    public string Value { get { return value; } }

    KLineType(string value)
    {
      this.value = value;
    }
  }
}

