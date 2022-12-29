using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;

class Program
{
  static async Task Main(string[] args)
  {
    Console.WriteLine("Hello, World!");
    TigerConfig.LogDir = "/data0/logs/tiger-openapi-cs";
    ApiLogger.DebugEnabled = true;
    ApiLogger.Info("start");

    // tiger config
    TigerConfig config = new TigerConfig()
    {
      License = License.TBNZ,// must
      TigerId = "20150899", // must
      DefaultAccount = "572386",// (optional) paper account: 20200821144442583
      PrivateKey = TigerConfig.ReadPrivateKey("/Users/tiger/dev/liutp_account/rsa_private_key_pkcs8_prod.pem"),// must
      FailRetryCounts = 2, // (optional) range:[1, 5],  default is 2
      AutoGrabPermission = false,   // (optional) default is true
      Language = Language.en_US,   // (optional) default is en_US
      TimeZone = DateUtil.HK_ZONE  // (optional) default is HK_ZONE
    };
    QuoteClient quoteClient = new QuoteClient(config);

    // QuoteApiService.USER_LICENSE
    //TigerResponse? response = await test_USER_LICENSE_Async(quoteClient);
    //TigerResponse? response = await test_GRAB_QUOTE_PERMISSION_Async(quoteClient);
    //TigerResponse? response = await test_GET_QUOTE_PERMISSION_Async(quoteClient);

    //TigerResponse? response = await test_MARKET_STATE_Async(quoteClient);
    //TigerResponse? response = await test_TRADING_CALENDAR_Async(quoteClient);
    //TigerResponse? response = await test_ALL_SYMBOLS_Async(quoteClient);
    //TigerResponse? response = await test_ALL_SYMBOL_NAMES_Async(quoteClient);
    //TigerResponse? response = await test_QUOTE_DELAY_Async(quoteClient);
    //TigerResponse? response = await test_TIMELINE_Async(quoteClient);
    //TigerResponse? response = await test_HISTORY_TIMELINE_Async(quoteClient);
    //TigerResponse? response = await test_QUOTE_REAL_TIME_Async(quoteClient);
    //TigerResponse? response = await test_KLINE_Async(quoteClient);
    //TigerResponse? response = await test_QUOTE_DEPTH_Async(quoteClient);

    //TigerResponse? response = await test_TRADE_TICK_Async(quoteClient);
    //TigerResponse? response = await test_QUOTE_STOCK_TRADE_Async(quoteClient);
    //TigerResponse? response = await test_CAPITAL_FLOW_Async(quoteClient);
    //TigerResponse? response = await test_CAPITAL_DISTRIBUTION_Async(quoteClient);
    //TigerResponse? response = await test_STOCK_BROKER_Async(quoteClient);
    //TigerResponse? response = await test_OPTION_EXPIRATION_Async(quoteClient);
    //TigerResponse? response = await test_OPTION_CHAIN_Async(quoteClient);
    //TigerResponse? response = await test_OPTION_BRIEF_Async(quoteClient);
    //TigerResponse? response = await test_OPTION_KLINE_Async(quoteClient);
    //TigerResponse? response = await test_OPTION_TRADE_TICK_Async(quoteClient);

    //TigerResponse? response = await test_FUTURE_EXCHANGE_Async(quoteClient);
    //TigerResponse? response = await test_FUTURE_CONTRACT_BY_EXCHANGE_CODE_Async(quoteClient);
    //TigerResponse? response = await test_FUTURE_CONTRACT_BY_CONTRACT_CODE_Async(quoteClient);
    // 查询指定品种的全部合约
    //TigerResponse? response = await test_FUTURE_CONTRACTS_Async(quoteClient);
    // 查询指定品种的连续合约(main合约)
    //TigerResponse? response = await test_FUTURE_CONTINUOUS_CONTRACTS_Async(quoteClient);
    // 查询指定品种的当前合约
    //TigerResponse? response = await test_FUTURE_CURRENT_CONTRACT_Async(quoteClient);
    // 查询指定期货合约的交易时间
    //TigerResponse? response = await test_FUTURE_TRADING_DATE_Async(quoteClient);
    //TigerResponse? response = await test_FUTURE_KLINE_Async(quoteClient);
    //TigerResponse? response = await test_FUTURE_REAL_TIME_QUOTE_Async(quoteClient);
    TigerResponse? response = await test_FUTURE_TICK_Async(quoteClient);

    ApiLogger.Info("response:" + JsonConvert.SerializeObject(response));
    Thread.Sleep(1000);
    ApiLogger.Info("end");
  }

  
  static async Task<FutureTickResponse?> test_FUTURE_TICK_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureTickResponse> request = new TigerRequest<FutureTickResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_TICK,
      ModelValue = new FutureTickModel()
      {
        ContractCode = "ES2303",
        BeginIndex = 10,
        EndIndex = 100,
        Limit = 20
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureRealTimeQuoteResponse?> test_FUTURE_REAL_TIME_QUOTE_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureRealTimeQuoteResponse> request = new TigerRequest<FutureRealTimeQuoteResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_REAL_TIME_QUOTE,
      ModelValue = new FutureContractCodesModel()
      {
        ContractCodes = new List<string> { "CL2303" }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureKlineResponse?> test_FUTURE_KLINE_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureKlineResponse> request = new TigerRequest<FutureKlineResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_KLINE,
      ModelValue = new FutureKlineModel()
      {
        ContractCodes = new List<string> { "ES2303" },
        Period = FutureKType.min15.Value,
        BeginTime = DateUtil.ConvertTimestamp("2022-12-28 09:00:00", DateUtil.NY_ZONE),
        EndTime = DateUtil.ConvertTimestamp("2022-12-28 20:00:00", DateUtil.NY_ZONE),
        Limit = 20
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureTradingDateResponse?> test_FUTURE_TRADING_DATE_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureTradingDateResponse> request = new TigerRequest<FutureTradingDateResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_TRADING_DATE,
      ModelValue = new FutureTradingDateModel() {
        ContractCode = "ES2303",
        TradingDate = DateUtil.CurrentTimeMillis()
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractResponse?> test_FUTURE_CURRENT_CONTRACT_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractResponse> request = new TigerRequest<FutureContractResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CURRENT_CONTRACT,
      ModelValue = new FutureContractByTypeModel() { FutureType = "ES" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractResponse?> test_FUTURE_CONTINUOUS_CONTRACTS_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractResponse> request = new TigerRequest<FutureContractResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CONTINUOUS_CONTRACTS,
      ModelValue = new FutureContractByTypeModel() { FutureType = "ES" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractsResponse?> test_FUTURE_CONTRACTS_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractsResponse> request = new TigerRequest<FutureContractsResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CONTRACTS,
      ModelValue = new FutureContractByTypeModel() { FutureType = "CL" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractResponse?> test_FUTURE_CONTRACT_BY_CONTRACT_CODE_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractResponse> request = new TigerRequest<FutureContractResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CONTRACT_BY_CONTRACT_CODE,
      ModelValue = new FutureContractByConCodeModel() { ContractCode = "ES2303" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractsResponse?> test_FUTURE_CONTRACT_BY_EXCHANGE_CODE_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractsResponse> request = new TigerRequest<FutureContractsResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CONTRACT_BY_EXCHANGE_CODE,
      ModelValue = new FutureContractByExchCodeModel() { ExchangeCode = "CME" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureExchangeResponse?> test_FUTURE_EXCHANGE_Async(QuoteClient quoteClient)
  {
    TigerRequest<FutureExchangeResponse> request = new TigerRequest<FutureExchangeResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_EXCHANGE,
      ModelValue = new FutureExchangeModel()
      {
        SecType = SecType.FUT.ToString()
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionTradeTickResponse?> test_OPTION_TRADE_TICK_Async(QuoteClient quoteClient)
  {
    TigerRequest<OptionTradeTickResponse> request = new TigerRequest<OptionTradeTickResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_TRADE_TICK,
      ModelValue = new BatchApiModel<OptionCommonModel>()
      {
        Items = new List<OptionCommonModel>()
        {
          new OptionCommonModel() { Symbol = "AAPL", Right = "PUT", Strike = "130.0",
            Expiry = DateUtil.ConvertTimestamp("2023-01-06", DateUtil.NY_ZONE)}
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_OPTION_KLINE_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_KLINE,
      ModelValue = new BatchApiModel<OptionKlineModel>()
      {
        Items = new List<OptionKlineModel>()
        {
          new OptionKlineModel() { Symbol = "AAPL", Right = "PUT", Strike = "130.0",
            Expiry = DateUtil.ConvertTimestamp("2023-01-06", DateUtil.NY_ZONE),
            BeginTime = DateUtil.ConvertTimestamp("2022-12-15", DateUtil.NY_ZONE),
            EndTime = DateUtil.ConvertTimestamp("2022-12-30", DateUtil.NY_ZONE),
          }
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_OPTION_BRIEF_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_BRIEF,
      ModelValue = new BatchApiModel<OptionCommonModel>()
      {
        Items = new List<OptionCommonModel>()
        {
          new OptionCommonModel() { Symbol = "AAPL", Right = "PUT", Strike = "130.0",
            Expiry = DateUtil.ConvertTimestamp("2023-01-06", DateUtil.NY_ZONE)}
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_OPTION_CHAIN_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_CHAIN,
      ModelValue = new OptionChainV3Model()
      {
        OptionBasic = new List<OptionChainModel>()
        {
          new OptionChainModel() { Symbol = "AAPL", Expiry = DateUtil.ConvertTimestamp("2023-01-27", DateUtil.NY_ZONE)}
        },
        OptionFilter = new OptionChainFilterModel()
        {
          InTheMoney = true,
          ImpliedVolatility = new Range<Double>(0.3037, 0.6282),
          OpenInterest = new Range<int>(100, 5000),
          Greeks = new Greeks()
          {
            Delta = new Range<Double>(-0.8, 0.6),
            Gamma = new Range<double>(0.024, 0.071),
            Vega = new Range<double>(0.019, 0.143),
            Theta = new Range<double>(-0.064, -0.036),
            Rho = new Range<double>(-0.096, 0.001)
          }
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_OPTION_EXPIRATION_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_EXPIRATION,
      ModelValue = new OptionExpirationModel()
      {
        Symbols = new List<string> { "AAPL" }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerDictResponse?> test_STOCK_BROKER_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = QuoteApiService.STOCK_BROKER,
      ModelValue = new QuoteStockBrokerModel()
      {
        Symbol = "00700",// only support HK market's symbol
        Limit = 10
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerDictResponse?> test_CAPITAL_DISTRIBUTION_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = QuoteApiService.CAPITAL_DISTRIBUTION,
      ModelValue = new QuoteCapitalModel()
      {
        Symbol = "AAPL",
        Market = Market.US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerDictResponse?> test_CAPITAL_FLOW_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = QuoteApiService.CAPITAL_FLOW,
      ModelValue = new QuoteCapitalFlowModel()
      {
        Symbol = "AAPL",
        Market = Market.US,
        Period = CapitalPeriod.day.Value,
        BeginTime = DateUtil.ConvertTimestamp("2022-12-01", DateUtil.NY_ZONE),
        EndTime = DateUtil.ConvertTimestamp("2022-12-28", DateUtil.NY_ZONE)
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_QUOTE_STOCK_TRADE_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_STOCK_TRADE,
      ModelValue = new QuoteStockTradeModel()
      {
        Symbols = new List<string> { "AAPL", "TSLA" }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_TRADE_TICK_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.TRADE_TICK,
      ModelValue = new QuoteTradeTickModel()
      {
        Symbols = new List<string> { "AAPL" },
        BeginIndex = 0,
        EndIndex = 10
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_QUOTE_DEPTH_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_DEPTH,
      ModelValue = new QuoteDepthModel()
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_KLINE_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.KLINE,
      ModelValue = new QuoteKlineModel()
      {
        Symbols = new List<string> { "AAPL" },
        Period = KLineType.day.Value,
        BeginTime = DateUtil.ConvertTimestamp("2022-12-10", DateUtil.NY_ZONE),
        EndTime = DateUtil.CurrentTimeMillis(),
        Rigth = RightOption.br
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_QUOTE_REAL_TIME_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_REAL_TIME,
      ModelValue = new QuoteSymbolModel()
      {
        Symbols = new List<string> { "AAPL" },
        IncludeHourTrading = true
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_HISTORY_TIMELINE_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.HISTORY_TIMELINE,
      ModelValue = new QuoteHistoryTimelineModel()
      {
        Symbols = new List<string> { "AAPL" },
        Date = "2022-12-22",
        Rigth = RightOption.br
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_TIMELINE_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.TIMELINE,
      ModelValue = new QuoteTimelineModel()
      {
        Symbols = new List<string> { "AAPL" },
        Period = TimeLineType.day,
        BeginTime = DateUtil.ConvertTimestamp("2022-12-07 03:00:00", DateUtil.NY_ZONE)
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_QUOTE_DELAY_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_DELAY,
      ModelValue = new QuoteSymbolModel() { Symbols = new List<string> { "AAPL" } }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_ALL_SYMBOL_NAMES_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.ALL_SYMBOL_NAMES,
      ModelValue = new QuoteMarketModel() { Market = Market.US, Lang = Language.zh_CN }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListStringResponse?> test_ALL_SYMBOLS_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListStringResponse> request = new TigerRequest<TigerListStringResponse>()
    {
      ApiMethodName = QuoteApiService.ALL_SYMBOLS,
      ModelValue = new QuoteMarketModel() { Market = Market.US }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_TRADING_CALENDAR_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.TRADING_CALENDAR,
      ModelValue = new TradeCalendarModel() {
        Market = Market.HK,
        BeginDate = "2022-06-01",
        EndDate = "2022-06-30"
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> test_MARKET_STATE_Async(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.MARKET_STATE,
      ModelValue = new QuoteMarketModel() { Market = Market.HK }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuotePermissionResponse?> test_GET_QUOTE_PERMISSION_Async(QuoteClient quoteClient)
  {
    TigerRequest<QuotePermissionResponse> request = new TigerRequest<QuotePermissionResponse>()
    {
      ApiMethodName = QuoteApiService.GET_QUOTE_PERMISSION
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuotePermissionResponse?> test_GRAB_QUOTE_PERMISSION_Async(QuoteClient quoteClient)
  {
    TigerRequest<QuotePermissionResponse> request = new TigerRequest<QuotePermissionResponse>()
    {
      ApiMethodName = QuoteApiService.GRAB_QUOTE_PERMISSION
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<UserLicenseResponse?> test_USER_LICENSE_Async(QuoteClient quoteClient)
  {
    TigerRequest<UserLicenseResponse> request = new TigerRequest<UserLicenseResponse>()
    {
      ApiMethodName = QuoteApiService.USER_LICENSE
    };
    return await quoteClient.ExecuteAsync(request);
  }
}