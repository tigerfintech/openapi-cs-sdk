using System;
namespace TigerOpenAPI.Trade
{
  public class TradeApiService
  {

    /**
     * trade
     */
    public const string ORDER_NO = "order_no";
    public const string PREVIEW_ORDER = "preview_order";
    public const string PLACE_ORDER = "place_order";
    public const string CANCEL_ORDER = "cancel_order";
    public const string MODIFY_ORDER = "modify_order";
    public const string TRANSFER_SEGMENT_FUND = "transfer_segment_fund";
    public const string CANCEL_SEGMENT_FUND = "cancel_segment_fund";
    public const string PLACE_FOREX_ORDER = "place_forex_order";

    /**
     * account/asset
     */
    public const string ACCOUNTS = "accounts";
    public const string ASSETS = "assets";
    public const string PRIME_ASSETS = "prime_assets";
    public const string ANALYTICS_ASSET = "analytics_asset";
    public const string POSITIONS = "positions";
    public const string ORDERS = "orders";
    public const string ACTIVE_ORDERS = "active_orders";
    public const string INACTIVE_ORDERS = "inactive_orders";
    public const string FILLED_ORDERS = "filled_orders";
    public const string ORDER_TRANSACTIONS = "order_transactions";
    public const string SEGMENT_FUND_HISTORY = "segment_fund_history";
    public const string SEGMENT_FUND_AVAILABLE = "segment_fund_available";
    public const string ESTIMATE_TRADABLE_QUANTITY = "estimate_tradable_quantity";

    /**
     * contract
     */
    public const string CONTRACT = "contract";
    public const string CONTRACTS = "contracts";

    public static readonly HashSet<string> AllTradeApiSet = new HashSet<string>()
    {
      /**
       * trade
       */
      ORDER_NO,
      PREVIEW_ORDER,
      PLACE_ORDER,
      CANCEL_ORDER,
      MODIFY_ORDER,
      TRANSFER_SEGMENT_FUND,
      CANCEL_SEGMENT_FUND,
      PLACE_FOREX_ORDER,

      /**
       * account/asset
       */
      ACCOUNTS,
      ASSETS,
      PRIME_ASSETS,
      ANALYTICS_ASSET,
      POSITIONS,
      ORDERS,
      ACTIVE_ORDERS,
      INACTIVE_ORDERS,
      FILLED_ORDERS,
      ORDER_TRANSACTIONS,
      SEGMENT_FUND_HISTORY,
      SEGMENT_FUND_AVAILABLE,
      ESTIMATE_TRADABLE_QUANTITY,

      /**
       * contract
       */
      CONTRACT,
      CONTRACTS,
    };

    public static bool IsTradeApi(in string tradeApi) =>
      string.IsNullOrWhiteSpace(tradeApi) ? false : AllTradeApiSet.Contains(tradeApi);

    private TradeApiService()
    {
    }
  }
}

