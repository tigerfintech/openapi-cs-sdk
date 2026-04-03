using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionTransferExternalRecordItem
  {
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }

    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "allFinished")]
    public bool AllFinished { get; set; }

    [JsonProperty(PropertyName = "counterpartyContacted")]
    public bool CounterpartyContacted { get; set; }

    [JsonProperty(PropertyName = "accountId")]
    public string AccountId { get; set; }

    [JsonProperty(PropertyName = "transferMethod")]
    public string TransferMethod { get; set; }

    [JsonProperty(PropertyName = "institutionName")]
    public string InstitutionName { get; set; }

    [JsonProperty(PropertyName = "institutionType")]
    public string InstitutionType { get; set; }

    [JsonProperty(PropertyName = "remoteClearingBroker")]
    public string RemoteClearingBroker { get; set; }

    [JsonProperty(PropertyName = "dtcNumber")]
    public string DtcNumber { get; set; }

    [JsonProperty(PropertyName = "remoteUserName")]
    public string RemoteUserName { get; set; }

    [JsonProperty(PropertyName = "remoteAccount")]
    public string RemoteAccount { get; set; }

    [JsonProperty(PropertyName = "contactName")]
    public string ContactName { get; set; }

    [JsonProperty(PropertyName = "contactEmail")]
    public string ContactEmail { get; set; }

    [JsonProperty(PropertyName = "contactPhone")]
    public string ContactPhone { get; set; }

    [JsonProperty(PropertyName = "cancelable")]
    public bool Cancelable { get; set; }

    [JsonProperty(PropertyName = "side")]
    public string Side { get; set; }

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }

    [JsonProperty(PropertyName = "userName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "transferHin")]
    public string TransferHin { get; set; }

    [JsonProperty(PropertyName = "fullPortfolio")]
    public bool FullPortfolio { get; set; }

    [JsonProperty(PropertyName = "createdAt")]
    public long CreatedAt { get; set; }

    [JsonProperty(PropertyName = "updatedAt")]
    public long UpdatedAt { get; set; }

    [JsonProperty(PropertyName = "transferPropertyInfos")]
    public List<object> TransferPropertyInfos { get; set; }
  }
}
