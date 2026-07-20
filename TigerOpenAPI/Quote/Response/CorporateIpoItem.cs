using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>新股上市事件</summary>
  public class CorporateIpoItem : CorporateActionItem
  {
    [JsonProperty(PropertyName = "ipoName")]
    public string IpoName { get; set; }

    [JsonProperty(PropertyName = "listingDate")]
    public DateTime ListingDate { get; set; }

    [JsonProperty(PropertyName = "listingPrice")]
    public Double ListingPrice { get; set; }

    [JsonProperty(PropertyName = "sharesOutstanding")]
    public Int64 SharesOutstanding { get; set; }

    [JsonProperty(PropertyName = "sharesFloat")]
    public Int64 SharesFloat { get; set; }

    [JsonProperty(PropertyName = "offerAmount")]
    public Double OfferAmount { get; set; }

    [JsonProperty(PropertyName = "priceRange")]
    public string PriceRange { get; set; }

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "minPurchaseQuantity")]
    public Int32 MinPurchaseQuantity { get; set; }

    [JsonProperty(PropertyName = "leverageRatio")]
    public Double LeverageRatio { get; set; }
  }
}
