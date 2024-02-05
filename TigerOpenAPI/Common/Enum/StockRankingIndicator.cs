using System;
namespace TigerOpenAPI.Common.Enum
{
  public class StockRankingIndicator : Indicator
  {
    public static readonly StockRankingIndicator ChangeRate = new StockRankingIndicator("changeRate");
    public static readonly StockRankingIndicator ChangeRate5Min = new StockRankingIndicator("changeRate5Min");
    public static readonly StockRankingIndicator TurnoverRate = new StockRankingIndicator("turnoverRate");
    public static readonly StockRankingIndicator Volume = new StockRankingIndicator("volume");
    public static readonly StockRankingIndicator Amount = new StockRankingIndicator("amount");
    public static readonly StockRankingIndicator Amplitude = new StockRankingIndicator("amplitude");

    public static IEnumerable<StockRankingIndicator> Values
    {
      get
      {
        yield return ChangeRate;
        yield return ChangeRate5Min;
        yield return TurnoverRate;
        yield return Volume;
        yield return Amount;
        yield return Amplitude;
      }
    }
    private readonly string value;
    public string Value { get { return value; } }

    StockRankingIndicator(string value)
    {
      this.value = value;
    }

    public string GetValue() => value;

    public static StockRankingIndicator? getIndicatorByValue(string value)
    {
      foreach (StockRankingIndicator indicator in StockRankingIndicator.Values)
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
