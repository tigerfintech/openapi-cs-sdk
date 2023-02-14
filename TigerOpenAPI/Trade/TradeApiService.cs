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

