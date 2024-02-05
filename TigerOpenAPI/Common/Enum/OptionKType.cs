using System;
namespace TigerOpenAPI.Common.Enum
{
  public class OptionKType
  {
    public static readonly OptionKType day = new OptionKType("day");
    public static readonly OptionKType min1 = new OptionKType("1min");
    public static readonly OptionKType min5 = new OptionKType("5min");
    public static readonly OptionKType min30 = new OptionKType("30min");
    public static readonly OptionKType min60 = new OptionKType("60min");

    public static IEnumerable<OptionKType> Values
    {
      get
      {
        yield return day;
        yield return min1;
        yield return min5;
        yield return min30;
        yield return min60;
      }
    }
    private readonly string value;
    public string Value { get { return value; } }

    OptionKType(string value)
    {
      this.value = value;
    }
  }
}

