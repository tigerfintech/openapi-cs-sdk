using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TigerOpenAPI.Push.Model
{
  public class SubscribedSymbol
  {
    /**
     * subscribed quote symbol limit
     */
    [JsonProperty(PropertyName = "limit")]
    public int Limit { get; set; }

    /**
     * subscribed quote symbol size
     */
    [JsonProperty(PropertyName = "used")]
    public int Used { get; set; }

    [JsonProperty(PropertyName = "askBidLimit")]
    public int AskBidLimit { get; set; }

    [JsonProperty(PropertyName = "askBidUsed")]
    public int AskBidUsed { get; set; }

    [JsonProperty(PropertyName = "tradeTickLimit")]
    public int TradeTickLimit { get; set; }

    [JsonProperty(PropertyName = "tradeTickUsed")]
    public int TradeTickUsed { get; set; }

    /**
     * subscribed quote symbol's detail
     */
    [JsonProperty(PropertyName = "subscribedSymbols")]
    public ISet<String> SubscribedSymbols { get; set; }

    /**
     * subscribed depth-quote symbol's detail
     */
    [JsonProperty(PropertyName = "subscribedAskBidSymbols")]
    public ISet<String> SubscribedAskBidSymbols { get; set; }

    /**
     * subscribed trade-tick symbol's detail
     */
    [JsonProperty(PropertyName = "subscribedTradeTickSymbols")]
    public ISet<String> SubscribedTradeTickSymbols { get; set; }

    [JsonProperty(PropertyName = "subscribedMarketQuote")]
    public ISet<String> SubscribedMarketQuote { get; set; }

    public SubscribedSymbol()
    {
    }
  }
}

