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

    private TradeApiService()
    {
    }
  }
}

