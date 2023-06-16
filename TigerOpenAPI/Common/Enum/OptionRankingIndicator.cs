using System;
namespace TigerOpenAPI.Common.Enum
{
  public class OptionRankingIndicator : Indicator
  {
    public static readonly OptionRankingIndicator BigOrder = new OptionRankingIndicator("bigOrder");
    public static readonly OptionRankingIndicator Volume = new OptionRankingIndicator("volume");
    public static readonly OptionRankingIndicator Amount = new OptionRankingIndicator("amount");
    public static readonly OptionRankingIndicator OpenInt = new OptionRankingIndicator("openInt");

    public static IEnumerable<OptionRankingIndicator> Values
    {
      get
      {
        yield return BigOrder;
        yield return Volume;
        yield return Amount;
        yield return OpenInt;
      }
    }
    private readonly string value;
    public string Value { get { return value; } }

    OptionRankingIndicator(string value)
    {
      this.value = value;
    }

    public string GetValue() => value;

    public static OptionRankingIndicator? getIndicatorByValue(string value)
    {
      foreach (OptionRankingIndicator indicator in OptionRankingIndicator.Values)
      {
        if (indicator.GetValue().Equals(value))
        {
          return indicator;
        }
      }
      return null;
    }
  }
}
