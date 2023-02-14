using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class WarrantFilterModel : ApiModel
  {

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "page")]
    public Int32 Page { get; set; }

    [JsonProperty(PropertyName = "page_size")]
    public Int32 PageSize { get; set; }

    [JsonProperty(PropertyName = "sort_field_name")]
    public string SortFieldName { get; set; }

    /** sort directions  */
    [JsonProperty(PropertyName = "sort_dir"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SortDir SortDir { get; set; }

    /** 1:Call, 2: Put, 3: Bull,4: Bear, 0: All */
    [JsonProperty(PropertyName = "warrant_type")]
    public ISet<Int32> WarrantType { get; set; }

    /** broker name */
    [JsonProperty(PropertyName = "issuer_name")]
    public string IssuerName { get; set; }
    /** expiry date: yyyy-MM */
    [JsonProperty(PropertyName = "expire_ym")]
    public string ExpireYM { get; set; }
    /** 0 All, 1 Normal, 2 Terminate Trades, 3 Waiting to be listed */
    [JsonProperty(PropertyName = "state")]
    public Int32 State { get; set; }

    /** -1:out the money, 1: in the money */
    [JsonProperty(PropertyName = "in_out_price")]
    public ISet<Int32> InOutPrice { get; set; }

    [JsonProperty(PropertyName = "lot_size")]
    public ISet<Int32> LotSize { get; set; }
    [JsonProperty(PropertyName = "entitlement_ratio")]
    public ISet<Double> EntitlementRatio { get; set; }

    /** strike price */
    [JsonProperty(PropertyName = "strike")]
    public Range<Double> Strike { get; set; }

    [JsonProperty(PropertyName = "effective_leverage")]
    public Range<Double> EffectiveLeverage { get; set; }

    [JsonProperty(PropertyName = "leverage_ratio")]
    public Range<Double> LeverageRatio { get; set; }

    [JsonProperty(PropertyName = "call_price")]
    public Range<Double> CallPrice { get; set; }

    [JsonProperty(PropertyName = "volume")]
    public Range<Int64> Volume { get; set; }
    [JsonProperty(PropertyName = "premium")]
    public Range<Double> Premium { get; set; }

    [JsonProperty(PropertyName = "outstanding_ratio")]
    public Range<Double> OutstandingRatio { get; set; }

    [JsonProperty(PropertyName = "implied_volatility")]
    public Range<Double> ImpliedVolatility { get; set; }

    public WarrantFilterModel() : base()
    {
    }
  }
}

