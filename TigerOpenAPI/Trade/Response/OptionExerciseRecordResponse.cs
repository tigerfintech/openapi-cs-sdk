using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;
using System;

namespace TigerOpenAPI.Trade.Response
{
  public class OptionExerciseRecord
  {
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [JsonProperty(PropertyName = "accountId")]
    public long? AccountId { get; set; }

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

    /// <summary>"Exercise" | "Expire"</summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [JsonProperty(PropertyName = "requestQuantity")]
    public double? RequestQuantity { get; set; }

    [JsonProperty(PropertyName = "quantity")]
    public double? Quantity { get; set; }

    /// <summary>"New" | "Cancel" | "Success" | "Fail"</summary>
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "executingDate")]
    public string ExecutingDate { get; set; }

    [JsonProperty(PropertyName = "itmRate")]
    public int? ItmRate { get; set; }

    [JsonProperty(PropertyName = "isForce")]
    public bool? IsForce { get; set; }

    [JsonProperty(PropertyName = "reason")]
    public string Reason { get; set; }
  }

  public class OptionExerciseRecordPage
  {
    [JsonProperty(PropertyName = "items")]
    public List<OptionExerciseRecord> Items { get; set; }

    [JsonProperty(PropertyName = "pageNum")]
    public int? PageNum { get; set; }

    [JsonProperty(PropertyName = "pageSize")]
    public int? PageSize { get; set; }

    [JsonProperty(PropertyName = "itemCount")]
    public int? ItemCount { get; set; }

    [JsonProperty(PropertyName = "pageCount")]
    public int? PageCount { get; set; }
  }

  public class OptionExerciseRecordResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public OptionExerciseRecordPage Data { get; set; }

    public OptionExerciseRecordResponse() { }
  }
}
