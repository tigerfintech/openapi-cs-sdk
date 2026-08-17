using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// A row in the <c>stock_detail</c> response.
  ///
  /// The wire format is a superset of the <c>quote_real_time</c> item —
  /// it adds fundamental fields (shares, EPS, ADR rate, listing date),
  /// market-status metadata, and sub-objects for the next market
  /// transition, extended-hours snapshot, corporate actions, and notices.
  /// Wire keys are camelCase; sub-objects reuse existing SDK types
  /// (<see cref="HourTrading"/>) so field coverage stays in one place.
  ///
  /// Field names mirror Python <c>DETAIL_FIELD_MAPPINGS</c> in
  /// <c>stock_details_response.py</c>. Fields that may legitimately be
  /// absent (fundamentals, corporate actions, notices) are declared as
  /// nullable / reference types so missing wire keys deserialize cleanly.
  /// </summary>
  public class StockDetailItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }

    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }

    [JsonProperty(PropertyName = "exchange")]
    public string Exchange { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "shortable")]
    public bool? Shortable { get; set; }

    // ---- Snapshot (subset of RealTimeQuoteItem) ----
    [JsonProperty(PropertyName = "askPrice")]
    public double? AskPrice { get; set; }

    [JsonProperty(PropertyName = "askSize")]
    public long? AskSize { get; set; }

    [JsonProperty(PropertyName = "bidPrice")]
    public double? BidPrice { get; set; }

    [JsonProperty(PropertyName = "bidSize")]
    public long? BidSize { get; set; }

    [JsonProperty(PropertyName = "preClose")]
    public double? PreClose { get; set; }

    [JsonProperty(PropertyName = "latestPrice")]
    public double? LatestPrice { get; set; }

    [JsonProperty(PropertyName = "adjPreClose")]
    public double? AdjPreClose { get; set; }

    /// <summary>
    /// stock_detail returns latestTime as a formatted string ("08-14 16:00:00 EDT"),
    /// unlike other real-time endpoints that return an epoch-ms long.
    /// </summary>
    [JsonProperty(PropertyName = "latestTime")]
    public string? LatestTime { get; set; }

    [JsonProperty(PropertyName = "volume")]
    public long? Volume { get; set; }

    [JsonProperty(PropertyName = "open")]
    public double? Open { get; set; }

    [JsonProperty(PropertyName = "high")]
    public double? High { get; set; }

    [JsonProperty(PropertyName = "low")]
    public double? Low { get; set; }

    [JsonProperty(PropertyName = "change")]
    public double? Change { get; set; }

    [JsonProperty(PropertyName = "amount")]
    public double? Amount { get; set; }

    [JsonProperty(PropertyName = "amplitude")]
    public double? Amplitude { get; set; }

    [JsonProperty(PropertyName = "marketStatus")]
    public string MarketStatus { get; set; }

    /// <summary>
    /// 0=not trading, 1=pre-market auction, 2=in trading, 3=after-hours
    /// auction (matches Python doc).
    /// </summary>
    [JsonProperty(PropertyName = "tradingStatus")]
    public int? TradingStatus { get; set; }

    // ---- Fundamentals ----
    [JsonProperty(PropertyName = "floatShares")]
    public long? FloatShares { get; set; }

    [JsonProperty(PropertyName = "shares")]
    public long? Shares { get; set; }

    [JsonProperty(PropertyName = "eps")]
    public double? Eps { get; set; }

    [JsonProperty(PropertyName = "adrRate")]
    public double? AdrRate { get; set; }

    /// <summary>
    /// ETF flag. 0 = non-ETF; non-zero indicates ETF, with 1/2/3
    /// signalling non-leveraged / 2x / 3x, and negative values marking
    /// inverse ETFs (matches Python doc).
    /// </summary>
    /// <summary>
    /// stock_detail returns etf as a float (0.0 / 1.0), not an integer flag.
    /// Use double? to tolerate both forms.
    /// </summary>
    [JsonProperty(PropertyName = "etf")]
    public double? Etf { get; set; }

    /// <summary>Listing date (epoch millis at 00:00 in the market's local tz).</summary>
    [JsonProperty(PropertyName = "listingDate")]
    public long? ListingDate { get; set; }

    [JsonProperty(PropertyName = "halted")]
    public double? Halted { get; set; }

    [JsonProperty(PropertyName = "delay")]
    public int? Delay { get; set; }

    // ---- Sub-objects (may all be absent) ----
    [JsonProperty(PropertyName = "hourTrading")]
    public HourTrading HourTrading { get; set; }

    /// <summary>
    /// Next market state transition. Loose-typed so future field additions
    /// (e.g. a session enum) don't break deserialization.
    /// </summary>
    [JsonProperty(PropertyName = "nextMarketStatus")]
    public Newtonsoft.Json.Linq.JObject NextMarketStatus { get; set; }

    [JsonProperty(PropertyName = "stockSplit")]
    public Newtonsoft.Json.Linq.JObject StockSplit { get; set; }

    [JsonProperty(PropertyName = "stockRight")]
    public Newtonsoft.Json.Linq.JObject StockRight { get; set; }

    [JsonProperty(PropertyName = "symbolChange")]
    public Newtonsoft.Json.Linq.JObject SymbolChange { get; set; }

    [JsonProperty(PropertyName = "stockNotice")]
    public Newtonsoft.Json.Linq.JObject StockNotice { get; set; }
  }
}
