using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for option_trade_tick API.
  ///
  /// Wire format: top-level JSON array
  ///   [{ "symbol": "...", "expiry": ..., "strike": "...", "right": "..." }, ...]
  ///
  /// Server rejects the object form ({"contracts": [...]}). Extends
  /// <see cref="BatchApiModel{T}"/> so <c>TigerClient.BuildParams</c> hits
  /// the BatchApiModel branch and serializes just the inner list as the
  /// biz_content root. Populate <c>Items</c> with one or more
  /// <see cref="OptionQueryItem"/> entries.
  ///
  /// Matches Java (<c>BatchApiModel(items)</c>) and Python
  /// (<c>MultipleContractParams.to_openapi_dict</c>).
  /// </summary>
  public class OptionTradeTickV2Model : BatchApiModel<OptionQueryItem>
  {
    public OptionTradeTickV2Model() : base()
    {
    }
  }
}
