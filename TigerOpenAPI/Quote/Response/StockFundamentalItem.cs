using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class StockFundamentalItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "roe")]
    public double Roe { get; set; }

    [JsonProperty(PropertyName = "roa")]
    public double Roa { get; set; }

    [JsonProperty(PropertyName = "pbRate")]
    public double PbRate { get; set; }

    [JsonProperty(PropertyName = "psRate")]
    public double PsRate { get; set; }

    [JsonProperty(PropertyName = "divideRate")]
    public double DivideRate { get; set; }

    [JsonProperty(PropertyName = "week52High")]
    public double Week52High { get; set; }

    [JsonProperty(PropertyName = "week52Low")]
    public double Week52Low { get; set; }

    [JsonProperty(PropertyName = "ttmEps")]
    public double TtmEps { get; set; }

    [JsonProperty(PropertyName = "lyrEps")]
    public double LyrEps { get; set; }

    [JsonProperty(PropertyName = "volumeRatio")]
    public double VolumeRatio { get; set; }

    [JsonProperty(PropertyName = "turnoverRate")]
    public double TurnoverRate { get; set; }

    [JsonProperty(PropertyName = "ttmPeRate")]
    public double TtmPeRate { get; set; }

    [JsonProperty(PropertyName = "lyrPeRate")]
    public double LyrPeRate { get; set; }

    [JsonProperty(PropertyName = "marketCap")]
    public double MarketCap { get; set; }

    [JsonProperty(PropertyName = "floatMarketCap")]
    public double FloatMarketCap { get; set; }
  }
}
