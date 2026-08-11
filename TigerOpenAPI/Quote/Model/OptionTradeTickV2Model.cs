using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for option_trade_tick API.
  /// Wire format: { "contracts": [{ symbol, expiry, strike, right }] }
  /// </summary>
  public class OptionTradeTickV2Model : ApiModel
  {
    [JsonProperty(PropertyName = "contracts")]
    public List<OptionQueryItem> Contracts { get; set; }
  }
}
