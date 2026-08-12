using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Trade.Response
{
  public class OptionExercisePosition
  {
    [JsonProperty(PropertyName = "contractId")]
    public long? ContractId { get; set; }

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "stkSymbol")]
    public string StkSymbol { get; set; }

    [JsonProperty(PropertyName = "expireDate")]
    public string ExpireDate { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }

    [JsonProperty(PropertyName = "callPut")]
    public string CallPut { get; set; }

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }

    [JsonProperty(PropertyName = "accountId")]
    public long? AccountId { get; set; }

    [JsonProperty(PropertyName = "position")]
    public double? Position { get; set; }

    [JsonProperty(PropertyName = "availableQuantity")]
    public double? AvailableQuantity { get; set; }
  }

  public class OptionExercisePositionPage
  {
    [JsonProperty(PropertyName = "items")]
    public List<OptionExercisePosition> Items { get; set; }

    [JsonProperty(PropertyName = "pageNum")]
    public int? PageNum { get; set; }

    [JsonProperty(PropertyName = "pageSize")]
    public int? PageSize { get; set; }

    [JsonProperty(PropertyName = "itemCount")]
    public int? ItemCount { get; set; }

    [JsonProperty(PropertyName = "pageCount")]
    public int? PageCount { get; set; }
  }

  public class OptionExercisePositionResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public OptionExercisePositionPage Data { get; set; }

    public OptionExercisePositionResponse() { }
  }
}
