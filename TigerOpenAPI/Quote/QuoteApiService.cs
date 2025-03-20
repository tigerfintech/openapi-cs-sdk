﻿using System;

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
    public const string QUOTE_OVERNIGHT = "quote_overnight";
    /** trading calendar */
    public const string TRADING_CALENDAR = "trading_calendar";
    public const string STOCK_BROKER = "stock_broker";
    public const string CAPITAL_DISTRIBUTION = "capital_distribution";
    public const string CAPITAL_FLOW = "capital_flow";
    public const string MARKET_SCANNER = "market_scanner";
    public const string MARKET_SCANNER_TAGS = "market_scanner_tags";

    /**
     * option quote
     */
    public const string OPTION_EXPIRATION = "option_expiration";
    public const string OPTION_CHAIN = "option_chain";
    public const string OPTION_BRIEF = "option_brief";
    public const string OPTION_KLINE = "option_kline";
    public const string OPTION_TRADE_TICK = "option_trade_tick";
    public const string OPTION_DEPTH = "option_depth";
    public const string ALL_HK_OPTION_SYMBOLS = "all_hk_option_symbols";

    /**
     * warrant/cbbc quote
     */
    public const string WARRANT_FILTER = "warrant_filter";
    public const string WARRANT_REAL_TIME_QUOTE = "warrant_real_time_quote";

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
    public const string FUTURE_HISTORY_MAIN_CONTRACT = "future_history_main_contract";

    /**
     * fund quote
     */
    public const string FUND_ALL_SYMBOLS = "fund_all_symbols";
    public const string FUND_CONTRACTS = "fund_contracts";
    public const string FUND_QUOTE = "fund_quote";
    public const string FUND_HISTORY_QUOTE = "fund_history_quote";

    /**
     * fundamental data
     */
    public const string FINANCIAL_DAILY = "financial_daily";
    public const string FINANCIAL_REPORT = "financial_report";
    public const string CORPORATE_ACTION = "corporate_action";
    public const string INDUSTRY_LIST = "industry_list";
    public const string INDUSTRY_STOCKS = "industry_stocks";
    public const string STOCK_INDUSTRY = "stock_industry";
    public const string FINANCIAL_CURRENCY = "financial_currency";
    public const string FINANCIAL_EXCHANGE_RATE = "financial_exchange_rate";
    public const string STOCK_FUNDAMENTAL = "stock_fundamental";
    public const string TRADE_RANK = "trade_rank";

    /**
     * grab quote
     */
    public const string GRAB_QUOTE_PERMISSION = "grab_quote_permission";
    public const string GET_QUOTE_PERMISSION = "get_quote_permission";
    public const string KLINE_QUOTA = "kline_quota";

    public const string USER_LICENSE = "user_license";
    public const string USER_TOKEN_REFRESH = "user_token_refresh";


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
      QUOTE_OVERNIGHT,
      /** trading calendar */
      TRADING_CALENDAR,
      STOCK_BROKER,
      CAPITAL_DISTRIBUTION,
      CAPITAL_FLOW,
      MARKET_SCANNER,
      MARKET_SCANNER_TAGS,

      /**
       * option quote
       */
      OPTION_EXPIRATION,
      OPTION_CHAIN,
      OPTION_BRIEF,
      OPTION_KLINE,
      OPTION_TRADE_TICK,
      OPTION_DEPTH,
      ALL_HK_OPTION_SYMBOLS,

      /**
       * warrant/cbbc quote
       */
      WARRANT_FILTER,
      WARRANT_REAL_TIME_QUOTE,

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
      FUTURE_HISTORY_MAIN_CONTRACT,

      /**
       * fund quote
       */
      FUND_ALL_SYMBOLS,
      FUND_CONTRACTS,
      FUND_QUOTE,
      FUND_HISTORY_QUOTE,

      /**
       * fundmental data
       */
      FINANCIAL_DAILY,
      FINANCIAL_REPORT,
      CORPORATE_ACTION,
      INDUSTRY_LIST,
      INDUSTRY_STOCKS,
      STOCK_INDUSTRY,
      FINANCIAL_CURRENCY,
      FINANCIAL_EXCHANGE_RATE,
      STOCK_FUNDAMENTAL,
      TRADE_RANK,

      /**
       * grab quote
       */
      GRAB_QUOTE_PERMISSION,
      GET_QUOTE_PERMISSION,
      KLINE_QUOTA,
      USER_LICENSE,
      USER_TOKEN_REFRESH

    };

    public static bool IsQuoteApi(in string quoteApi) =>
      string.IsNullOrWhiteSpace(quoteApi) ? false : AllQuoteApiSet.Contains(quoteApi);

    QuoteApiService()
    {
    }
  }
}
