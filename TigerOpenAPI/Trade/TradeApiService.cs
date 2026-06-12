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
     * option exercise (early exercise / abandon)
     */
    public const string OPTION_EXERCISE_SUBMIT   = "option_exercise_submit";
    public const string OPTION_EXERCISE_CHECK     = "option_exercise_check";
    public const string OPTION_EXERCISE_RECORD    = "option_exercise_record";
    public const string OPTION_EXERCISE_POSITION  = "option_exercise_position";
    public const string OPTION_EXERCISE_CANCEL    = "option_exercise_cancel";

    /**
     * contract
     */
    public const string CONTRACT = "contract";
    public const string CONTRACTS = "contracts";

    /**
     * deposit/withdraw
     */
    public const string TRANSFER_FUND = "transfer_fund";

    /**
     * fund details
     */
    public const string FUND_DETAILS = "fund_details";

    /**
     * aggregate assets
     */
    public const string AGGREGATE_ASSETS = "aggregate_assets";

    /**
     * position transfer
     */
    public const string POSITION_TRANSFER = "position_transfer";
    public const string POSITION_TRANSFER_RECORDS = "position_transfer_records";
    public const string POSITION_TRANSFER_DETAIL = "position_transfer_detail";
    public const string POSITION_TRANSFER_EXTERNAL_RECORDS = "position_transfer_external_records";

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
       * option exercise
       */
      OPTION_EXERCISE_SUBMIT,
      OPTION_EXERCISE_CHECK,
      OPTION_EXERCISE_RECORD,
      OPTION_EXERCISE_POSITION,
      OPTION_EXERCISE_CANCEL,

      /**
       * contract
       */
      CONTRACT,
      CONTRACTS,

      /**
       * deposit/withdraw & fund details
       */
      TRANSFER_FUND,
      FUND_DETAILS,
      AGGREGATE_ASSETS,

      /**
       * position transfer
       */
      POSITION_TRANSFER,
      POSITION_TRANSFER_RECORDS,
      POSITION_TRANSFER_DETAIL,
      POSITION_TRANSFER_EXTERNAL_RECORDS,
    };

    public static bool IsTradeApi(in string tradeApi) =>
      string.IsNullOrWhiteSpace(tradeApi) ? false : AllTradeApiSet.Contains(tradeApi);

    private TradeApiService()
    {
    }
  }
}

