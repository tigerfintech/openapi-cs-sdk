using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  /// <summary>
  /// Request model for querying fund details (cash flow records)
  /// </summary>
  public class FundDetailsModel : TradeModel
  {
    [JsonProperty(PropertyName = "seg_types")]
    public List<string> SegTypes { get; set; }

    [JsonProperty(PropertyName = "fund_type")]
    public string FundType { get; set; }

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "start_date")]
    public string StartDate { get; set; }

    [JsonProperty(PropertyName = "end_date")]
    public string EndDate { get; set; }

    [JsonProperty(PropertyName = "start")]
    public long? Start { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public long? Limit { get; set; }

    public FundDetailsModel() : base()
    {
    }

    public FundDetailsModel(string account) : base()
    {
      Account = account;
    }

    public FundDetailsModel(string account, List<string> segTypes) : this(account)
    {
      SegTypes = segTypes;
    }

    public FundDetailsModel(string account, List<string> segTypes, string secretKey) : this(account, segTypes)
    {
      SecretKey = secretKey;
    }

    public FundDetailsModel(string account, List<string> segTypes, string fundType, string secretKey) : this(account, segTypes, secretKey)
    {
      FundType = fundType;
    }

    public FundDetailsModel(string account, List<string> segTypes, long start, long limit) : this(account, segTypes)
    {
      Start = start;
      Limit = limit;
    }

    public FundDetailsModel(string account, List<string> segTypes, long start, long limit, string secretKey) : this(account, segTypes, start, limit)
    {
      SecretKey = secretKey;
    }
  }
}
