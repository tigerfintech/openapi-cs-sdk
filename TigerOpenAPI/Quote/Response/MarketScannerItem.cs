using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TigerOpenAPI.Quote.Response
{
  public class MarketScannerItem
  {

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    /** 筛选后的:简单指标属性数据 */
    [JsonProperty(PropertyName = "baseDataList")]
    public List<MarketIndicatorValue> BaseDataList { get; set; }
    /** 筛选后的:累积指标属性数据 */
    [JsonProperty(PropertyName = "accumulateDataList")]
    public List<MarketIndicatorValue> AccumulateDataList { get; set; }
    /** 筛选后的:财务指标属性数据 */
    [JsonProperty(PropertyName = "financialDataList")]
    public List<MarketIndicatorValue> FinancialDataList { get; set; }
    /** 筛选后的:多标签层面过滤器 */
    [JsonProperty(PropertyName = "multiTagDataList")]
    public List<MarketIndicatorValue> MultiTagDataList { get; set; }

    public MarketScannerItem()
    {
    }
  }
}

