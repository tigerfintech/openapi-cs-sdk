using System;

namespace TigerOpenAPI.Quote
{
  public class QuoteApiService
  {

    /**
     * quote
     */
    public const string MARKET_STATE = "market_state";
    public const string ALL_SYMBOLS = "all_symbols";
    public const string ALL_SYMBOL_NAMES = "all_symbol_names";
    public const string BRIEF = "brief";
    public const string STOCK_DETAIL = "stock_detail";
    public const string HOUR_TRADING_TIMELINE = "hour_trading_timeline";

    public const string TIMELINE = "timeline";
    public const string HISTORY_TIMELINE = "history_timeline";
    public const string KLINE = "kline";
    public const string TRADE_TICK = "trade_tick";
    public const string QUOTE_CONTRACT = "quote_contract";
    public const string QUOTE_REAL_TIME = "quote_real_time";
    public const string QUOTE_SHORTABLE_STOCKS = "quote_shortable_stocks";
    public const string QUOTE_STOCK_TRADE = "quote_stock_trade";
    public const string QUOTE_DEPTH = "quote_depth";
    public const string QUOTE_DELAY = "quote_delay";
    /** trading calendar */
    public const string TRADING_CALENDAR = "trading_calendar";
    public const string STOCK_BROKER = "stock_broker";
    public const string CAPITAL_DISTRIBUTION = "capital_distribution";
    public const string CAPITAL_FLOW = "capital_flow";
    public const string MARKET_SCANNER = "market_scanner";

    /**
     * option quote
     */
    public const string OPTION_EXPIRATION = "option_expiration";
    public const string OPTION_CHAIN = "option_chain";
    public const string OPTION_BRIEF = "option_brief";
    public const string OPTION_KLINE = "option_kline";
    public const string OPTION_TRADE_TICK = "option_trade_tick";

    /**
     * future quote
     */
    public const string FUTURE_EXCHANGE = "future_exchange";
    public const string FUTURE_CONTRACT_BY_CONTRACT_CODE = "future_contract_by_contract_code";
    public const string FUTURE_CONTRACT_BY_EXCHANGE_CODE = "future_contract_by_exchange_code";
    public const string FUTURE_CONTINUOUS_CONTRACTS = "future_continuous_contracts";
    public const string FUTURE_CURRENT_CONTRACT = "future_current_contract";
    public const string FUTURE_CONTRACTS = "future_contracts";
    public const string FUTURE_KLINE = "future_kline";
    public const string FUTURE_REAL_TIME_QUOTE = "future_real_time_quote";
    public const string FUTURE_TICK = "future_tick";
    public const string FUTURE_TRADING_DATE = "future_trading_date";

    /**
     * fundmental data
     */
    public const string FINANCIAL_DAILY = "financial_daily";
    public const string FINANCIAL_REPORT = "financial_report";
    public const string CORPORATE_ACTION = "corporate_action";
    public const string INDUSTRY_LIST = "industry_list";
    public const string INDUSTRY_STOCKS = "industry_stocks";
    public const string STOCK_INDUSTRY = "stock_industry";

    /**
     * grab quote
     */
    public const string GRAB_QUOTE_PERMISSION = "grab_quote_permission";
    public const string GET_QUOTE_PERMISSION = "get_quote_permission";

    public const string USER_LICENSE = "user_license";


    public static readonly HashSet<string> AllQuoteApiSet = new HashSet<string>() {
      MARKET_STATE,
      ALL_SYMBOLS,
      ALL_SYMBOL_NAMES,
      BRIEF,
      STOCK_DETAIL,
      HOUR_TRADING_TIMELINE,

      TIMELINE,
      HISTORY_TIMELINE,
      KLINE,
      TRADE_TICK,
      QUOTE_CONTRACT,
      QUOTE_REAL_TIME,
      QUOTE_SHORTABLE_STOCKS,
      QUOTE_STOCK_TRADE,
      QUOTE_DEPTH,
      QUOTE_DELAY,
      /** trading calendar */
      TRADING_CALENDAR,
      STOCK_BROKER,
      CAPITAL_DISTRIBUTION,
      CAPITAL_FLOW,
      MARKET_SCANNER,

      /**
       * option quote
       */
      OPTION_EXPIRATION,
      OPTION_CHAIN,
      OPTION_BRIEF,
      OPTION_KLINE,
      OPTION_TRADE_TICK,

      /**
       * future quote
       */
      FUTURE_EXCHANGE,
      FUTURE_CONTRACT_BY_CONTRACT_CODE,
      FUTURE_CONTRACT_BY_EXCHANGE_CODE,
      FUTURE_CONTINUOUS_CONTRACTS,
      FUTURE_CURRENT_CONTRACT,
      FUTURE_CONTRACTS,
      FUTURE_KLINE,
      FUTURE_REAL_TIME_QUOTE,
      FUTURE_TICK,
      FUTURE_TRADING_DATE,

      /**
       * fundmental data
       */
      FINANCIAL_DAILY,
      FINANCIAL_REPORT,
      CORPORATE_ACTION,
      INDUSTRY_LIST,
      INDUSTRY_STOCKS,
      STOCK_INDUSTRY,

      /**
       * grab quote
       */
      GRAB_QUOTE_PERMISSION,
      GET_QUOTE_PERMISSION,
      USER_LICENSE

    };

    public static bool IsQuoteApi(in string quoteApi) =>
      string.IsNullOrWhiteSpace(quoteApi) ? false : AllQuoteApiSet.Contains(quoteApi);

    QuoteApiService()
    {
    }
  }
}
