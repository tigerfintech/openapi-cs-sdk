// Doc verification test — runs all documented read-only API examples
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Trade;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

class DocTest
{
  static int pass = 0, fail = 0;

  static void Log(string name, bool ok, string detail = "")
  {
    string status = ok ? "PASS" : "FAIL";
    Console.WriteLine($"[{status}] {name}{(string.IsNullOrEmpty(detail) ? "" : " — " + detail)}");
    if (ok) pass++; else fail++;
  }

  static async Task Run<T>(string name, Func<Task<T?>> fn) where T : TigerResponse
  {
    try
    {
      T? r = await fn();
      bool ok = r != null && r.IsSuccess();
      Log(name, ok, r != null ? $"code={r.Code} msg={r.Message}" : "null response");
    }
    catch (Exception e)
    {
      Log(name, false, e.Message);
    }
  }

  public static async Task RunAsync()
  {
    Console.WriteLine("=== C# SDK Doc Verification ===");
    string configPath =
      Environment.GetEnvironmentVariable("TIGER_CONFIG_PATH")
      ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".tigeropen");

    TigerConfig config = new TigerConfig()
    {
      ConfigFilePath = configPath,
      FailRetryCounts = 1,
      AutoGrabPermission = false,
      AutoRefreshToken = false,
      Language = Language.en_US,
      TimeZone = CustomTimeZone.HK_ZONE
    };

    QuoteClient qc = new QuoteClient(config);
    TradeClient tc = new TradeClient(config);
    string account = tc.GetDefaultAccount;
    Console.WriteLine($"account={account}");
    Console.WriteLine();

    Console.WriteLine("--- QUOTE ---");

    // marketState
    await Run("quote/marketState US", async () => {
      var req = new TigerRequest<MarketStateResponse>() {
        ApiMethodName = QuoteApiService.MARKET_STATE,
        ModelValue = new QuoteMarketModel() { Market = Market.US }
      };
      return await qc.ExecuteAsync(req);
    });

    await Run("quote/marketState HK", async () => {
      var req = new TigerRequest<MarketStateResponse>() {
        ApiMethodName = QuoteApiService.MARKET_STATE,
        ModelValue = new QuoteMarketModel() { Market = Market.HK }
      };
      return await qc.ExecuteAsync(req);
    });

    // tradingCalendar
    await Run("quote/tradingCalendar", async () => {
      var req = new TigerRequest<TradeCalendarResponse>() {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new TradeCalendarModel() {
          Market = Market.US,
          BeginDate = "2025-01-01",
          EndDate = "2025-01-31"
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // allSymbols
    await Run("quote/allSymbols US STK", async () => {
      var req = new TigerRequest<SymbolNameResponse>() {
        ApiMethodName = QuoteApiService.ALL_SYMBOLS,
        ModelValue = new QuoteMarketModel() { Market = Market.US }
      };
      return await qc.ExecuteAsync(req);
    });

    // allSymbolNames
    await Run("quote/allSymbolNames", async () => {
      var req = new TigerRequest<SymbolNameResponse>() {
        ApiMethodName = QuoteApiService.ALL_SYMBOL_NAMES,
        ModelValue = new QuoteMarketModel() { Market = Market.US }
      };
      return await qc.ExecuteAsync(req);
    });

    // realTimeQuote
    await Run("quote/realTimeQuote", async () => {
      var req = new TigerRequest<QuoteRealTimeQuoteResponse>() {
        ApiMethodName = QuoteApiService.QUOTE_REAL_TIME,
        ModelValue = new QuoteSymbolModel() {
          Symbols = new List<string> { "AAPL", "00700" }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // delayQuote
    await Run("quote/delayQuote", async () => {
      var req = new TigerRequest<QuoteDelayResponse>() {
        ApiMethodName = QuoteApiService.QUOTE_DELAY,
        ModelValue = new QuoteSymbolModel() {
          Symbols = new List<string> { "AAPL" }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // overnightQuote
    await Run("quote/overnightQuote", async () => {
      var req = new TigerRequest<QuoteOvernightResponse>() {
        ApiMethodName = QuoteApiService.QUOTE_OVERNIGHT,
        ModelValue = new QuoteSymbolModel() {
          Symbols = new List<string> { "AAPL" }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // kLine
    await Run("quote/kLine daily", async () => {
      var req = new TigerRequest<QuoteKlineResponse>() {
        ApiMethodName = QuoteApiService.KLINE,
        ModelValue = new QuoteKlineModel() {
          Symbols = new List<string> { "AAPL" },
          Period = KLineType.day.Value,
          BeginTime = DateUtil.ConvertTimestamp("2025-01-01", CustomTimeZone.HK_ZONE),
          EndTime = DateUtil.ConvertTimestamp("2025-03-01", CustomTimeZone.HK_ZONE),
          Limit = 20
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // timeline
    await Run("quote/timeline", async () => {
      var req = new TigerRequest<QuoteTimelineResponse>() {
        ApiMethodName = QuoteApiService.TIMELINE,
        ModelValue = new QuoteTimelineModel() {
          Symbols = new List<string> { "AAPL" }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // historyTimeline
    await Run("quote/historyTimeline", async () => {
      var req = new TigerRequest<QuoteHistoryTimelineResponse>() {
        ApiMethodName = QuoteApiService.HISTORY_TIMELINE,
        ModelValue = new QuoteHistoryTimelineModel() {
          Symbols = new List<string> { "AAPL" },
          Date = "2025-03-03"
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // tradeTick
    await Run("quote/tradeTick", async () => {
      var req = new TigerRequest<QuoteTradeTickResponse>() {
        ApiMethodName = QuoteApiService.TRADE_TICK,
        ModelValue = new QuoteTradeTickModel() {
          Symbols = new List<string> { "AAPL" },
          BeginIndex = 0,
          EndIndex = 10
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // depthQuote
    await Run("quote/depthQuote", async () => {
      var req = new TigerRequest<QuoteDepthResponse>() {
        ApiMethodName = QuoteApiService.QUOTE_DEPTH,
        ModelValue = new QuoteDepthModel() {
          Symbols = new List<string> { "AAPL" },
          Market = Market.US
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // stockTradeInfo
    await Run("quote/stockTradeInfo", async () => {
      var req = new TigerRequest<QuoteStockTradeResponse>() {
        ApiMethodName = QuoteApiService.QUOTE_STOCK_TRADE,
        ModelValue = new QuoteStockTradeModel() {
          Symbols = new List<string> { "AAPL", "00700" }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // stockBroker (HK only)
    await Run("quote/stockBroker HK", async () => {
      var req = new TigerRequest<QuoteStockBrokerResponse>() {
        ApiMethodName = QuoteApiService.STOCK_BROKER,
        ModelValue = new QuoteStockBrokerModel() {
          Symbol = "00700",
          Limit = 10
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // capitalFlow
    await Run("quote/capitalFlow", async () => {
      var req = new TigerRequest<QuoteCapitalFlowResponse>() {
        ApiMethodName = QuoteApiService.CAPITAL_FLOW,
        ModelValue = new QuoteCapitalFlowModel() {
          Symbol = "AAPL",
          Market = Market.US,
          Period = CapitalPeriod.day.Value,
          BeginTime = DateUtil.ConvertTimestamp("2025-03-01", CustomTimeZone.NY_ZONE),
          EndTime = DateUtil.ConvertTimestamp("2025-03-10", CustomTimeZone.NY_ZONE)
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // capitalDistribution
    await Run("quote/capitalDistribution", async () => {
      var req = new TigerRequest<QuoteCapitalDistributionResponse>() {
        ApiMethodName = QuoteApiService.CAPITAL_DISTRIBUTION,
        ModelValue = new QuoteCapitalModel() {
          Symbol = "AAPL",
          Market = Market.US
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // optionExpiration
    await Run("quote/optionExpiration", async () => {
      var req = new TigerRequest<OptionExpirationResponse>() {
        ApiMethodName = QuoteApiService.OPTION_EXPIRATION,
        ModelValue = new OptionExpirationModel() {
          Symbols = new List<string> { "AAPL" },
          Market = Market.US
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // optionChain
    await Run("quote/optionChain", async () => {
      var req = new TigerRequest<OptionChainResponse>() {
        ApiMethodName = QuoteApiService.OPTION_CHAIN,
        ModelValue = new OptionChainV3Model() {
          Market = Market.US,
          OptionBasic = new List<OptionChainModel>() {
            new OptionChainModel() {
              Symbol = "AAPL",
              Expiry = DateUtil.ConvertTimestamp("2025-07-18", CustomTimeZone.NY_ZONE)
            }
          }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // optionBrief
    await Run("quote/optionBrief", async () => {
      var req = new TigerRequest<OptionBriefResponse>() {
        ApiMethodName = QuoteApiService.OPTION_BRIEF,
        ModelValue = new OptionBasicModel() {
          Market = Market.US,
          OptionBasic = new List<OptionCommonModel>() {
            new OptionCommonModel() {
              Symbol = "AAPL",
              Right = "CALL",
              Strike = "200.0",
              Expiry = DateUtil.ConvertTimestamp("2025-07-18", CustomTimeZone.NY_ZONE)
            }
          }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // optionKline
    await Run("quote/optionKLine", async () => {
      var req = new TigerRequest<OptionKlineResponse>() {
        ApiMethodName = QuoteApiService.OPTION_KLINE,
        ModelValue = new OptionKlineV2Model() {
          Market = Market.US,
          OptionQuery = new List<OptionKlineModel>() {
            new OptionKlineModel() {
              Symbol = "AAPL",
              Right = "CALL",
              Strike = "200.0",
              Expiry = DateUtil.ConvertTimestamp("2025-07-18", CustomTimeZone.NY_ZONE),
              BeginTime = DateUtil.ConvertTimestamp("2025-06-01", CustomTimeZone.NY_ZONE),
              EndTime = DateUtil.ConvertTimestamp("2025-06-15", CustomTimeZone.NY_ZONE),
              Period = OptionKType.day.Value,
              Limit = 10
            }
          }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // futureExchange
    await Run("quote/futureExchange", async () => {
      var req = new TigerRequest<FutureExchangeResponse>() {
        ApiMethodName = QuoteApiService.FUTURE_EXCHANGE,
        ModelValue = new FutureExchangeModel() { SecType = SecType.FUT.ToString() }
      };
      return await qc.ExecuteAsync(req);
    });

    // futureContractByExchange
    await Run("quote/futureContractByExchange CME", async () => {
      var req = new TigerRequest<FutureContractsResponse>() {
        ApiMethodName = QuoteApiService.FUTURE_CONTRACT_BY_EXCHANGE_CODE,
        ModelValue = new FutureContractByExchCodeModel() { ExchangeCode = "CME" }
      };
      return await qc.ExecuteAsync(req);
    });

    // futureKline
    await Run("quote/futureKLine", async () => {
      var req = new TigerRequest<FutureKlineResponse>() {
        ApiMethodName = QuoteApiService.FUTURE_KLINE,
        ModelValue = new FutureKlineModel() {
          ContractCodes = new List<string> { "CL2508" },
          Period = FutureKType.day.Value,
          BeginTime = DateUtil.ConvertTimestamp("2025-04-01", CustomTimeZone.HK_ZONE),
          EndTime = DateUtil.ConvertTimestamp("2025-06-01", CustomTimeZone.HK_ZONE),
          Limit = 10
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // futureRealTimeQuote
    await Run("quote/futureRealTimeQuote", async () => {
      var req = new TigerRequest<FutureRealTimeQuoteResponse>() {
        ApiMethodName = QuoteApiService.FUTURE_REAL_TIME_QUOTE,
        ModelValue = new FutureContractCodesModel() {
          ContractCodes = new List<string> { "CL2508" }
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // klineQuota
    await Run("quote/klineQuota", async () => {
      var req = new TigerRequest<KlineQuotaResponse>() {
        ApiMethodName = QuoteApiService.KLINE_QUOTA,
        ModelValue = new KlineQuotaModel() { WithDetails = true }
      };
      return await qc.ExecuteAsync(req);
    });

    // financialExchangeRate
    await Run("quote/financialExchangeRate", async () => {
      var req = new TigerRequest<FinancialExchangeRateResponse>() {
        ApiMethodName = QuoteApiService.FINANCIAL_EXCHANGE_RATE,
        ModelValue = new FinancialExchangeRateModel() {
          CurrencyList = new List<string> { "USD", "HKD", "CNY" },
          BeginDate = DateUtil.ConvertTimestamp("2025-01-01", CustomTimeZone.HK_ZONE),
          EndDate = DateUtil.ConvertTimestamp("2025-01-07", CustomTimeZone.HK_ZONE)
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // corporateDividend
    await Run("quote/corporateDividend", async () => {
      var req = new TigerRequest<CorporateDividendResponse>() {
        ApiMethodName = QuoteApiService.CORPORATE_ACTION,
        ModelValue = new CorporateActionModel() {
          ActionType = CorporateActionType.DIVIDEND,
          Symbols = new List<string> { "AAPL" },
          Market = Market.US,
          BeginDate = DateUtil.ConvertTimestamp("2024-01-01", CustomTimeZone.HK_ZONE),
          EndDate = DateUtil.ConvertTimestamp("2025-01-01", CustomTimeZone.HK_ZONE)
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // corporateSymbolChange
    await Run("quote/corporateSymbolChange", async () => {
      var req = new TigerRequest<CorporateSymbolChangeResponse>() {
        ApiMethodName = QuoteApiService.CORPORATE_ACTION,
        ModelValue = new CorporateActionModel() {
          ActionType = CorporateActionType.SYMBOL_CHANGE,
          Symbols = new List<string> { "META" },
          Market = Market.US,
          BeginDate = DateUtil.ConvertTimestamp("2022-01-01", CustomTimeZone.HK_ZONE),
          EndDate = DateUtil.ConvertTimestamp("2023-01-01", CustomTimeZone.HK_ZONE)
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // corporateDelisting
    await Run("quote/corporateDelisting", async () => {
      var req = new TigerRequest<CorporateDelistingResponse>() {
        ApiMethodName = QuoteApiService.CORPORATE_ACTION,
        ModelValue = new CorporateActionModel() {
          ActionType = CorporateActionType.DELISTING,
          Symbols = new List<string> { "TWTR" },
          Market = Market.US,
          BeginDate = DateUtil.ConvertTimestamp("2022-01-01", CustomTimeZone.HK_ZONE),
          EndDate = DateUtil.ConvertTimestamp("2023-01-01", CustomTimeZone.HK_ZONE)
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // corporateIpo
    await Run("quote/corporateIpo", async () => {
      var req = new TigerRequest<CorporateIpoResponse>() {
        ApiMethodName = QuoteApiService.CORPORATE_ACTION,
        ModelValue = new CorporateActionModel() {
          ActionType = CorporateActionType.IPO,
          Symbols = new List<string> { "RIVN" },
          Market = Market.US,
          BeginDate = DateUtil.ConvertTimestamp("2021-01-01", CustomTimeZone.HK_ZONE),
          EndDate = DateUtil.ConvertTimestamp("2022-01-01", CustomTimeZone.HK_ZONE)
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // stockFundamental
    await Run("quote/stockFundamental", async () => {
      var req = new TigerRequest<QuoteStockFundamentalResponse>() {
        ApiMethodName = QuoteApiService.STOCK_FUNDAMENTAL,
        ModelValue = new QuoteStockFundamentalModel() {
          Symbols = new List<string> { "AAPL" },
          Market = Market.US
        }
      };
      return await qc.ExecuteAsync(req);
    });

    // marketScanner (filterSymbols)
    await Run("quote/marketScanner", async () => {
      var req = new TigerRequest<MarketScannerResponse>() {
        ApiMethodName = QuoteApiService.MARKET_SCANNER,
        ModelValue = new MarketScannerModel() {
          Market = Market.US,
          Page = 1,
          PageSize = 5
        }
      };
      return await qc.ExecuteAsync(req);
    });

    Console.WriteLine();
    Console.WriteLine("--- TRADE ---");
    Console.WriteLine();

    // accounts
    await Run("trade/accounts", async () => {
      var req = new TigerRequest<AccountsResponse>() {
        ApiMethodName = TradeApiService.ACCOUNTS,
        ModelValue = new ApiModel() { }
      };
      return await tc.ExecuteAsync(req);
    });

    // contract (stock)
    await Run("trade/contract AAPL", async () => {
      var req = new TigerRequest<ContractResponse>() {
        ApiMethodName = TradeApiService.CONTRACT,
        ModelValue = new ContractModel() { Symbol = "AAPL" }
      };
      return await tc.ExecuteAsync(req);
    });

    await Run("trade/contract 00700 HK", async () => {
      var req = new TigerRequest<ContractResponse>() {
        ApiMethodName = TradeApiService.CONTRACT,
        ModelValue = new ContractModel() {
          Symbol = "00700",
          Currency = Currency.HKD.ToString()
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // contracts (batch)
    await Run("trade/contracts batch", async () => {
      var req = new TigerRequest<ContractsResponse>() {
        ApiMethodName = TradeApiService.CONTRACTS,
        ModelValue = new ContractsModel() {
          SecType = SecType.STK.ToString(),
          Symbols = new List<string> { "AAPL", "TSLA" }
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // positions
    await Run("trade/positions", async () => {
      var req = new TigerRequest<PositionsResponse>() {
        ApiMethodName = TradeApiService.POSITIONS,
        ModelValue = new PositionsModel() { Account = account }
      };
      return await tc.ExecuteAsync(req);
    });

    // assets
    await Run("trade/assets", async () => {
      var req = new TigerRequest<TigerDictResponse>() {
        ApiMethodName = TradeApiService.ASSETS,
        ModelValue = new GlobalAssetsModel() { Account = account }
      };
      return await tc.ExecuteAsync(req);
    });

    // orders (all)
    await Run("trade/orders all", async () => {
      long now = DateUtil.CurrentTimeMillis();
      var req = new TigerRequest<OrderBatchResponse>() {
        ApiMethodName = TradeApiService.ORDERS,
        ModelValue = new QueryOrderModel() {
          Account = account,
          StartDate = now - 30L * 24 * 3600 * 1000,
          EndDate = now,
          Limit = 5
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // active orders
    await Run("trade/activeOrders", async () => {
      long now = DateUtil.CurrentTimeMillis();
      var req = new TigerRequest<OrderBatchResponse>() {
        ApiMethodName = TradeApiService.ACTIVE_ORDERS,
        ModelValue = new QueryOrderModel() {
          Account = account,
          StartDate = now - 30L * 24 * 3600 * 1000,
          EndDate = now,
          Limit = 5
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // inactive orders
    await Run("trade/inactiveOrders", async () => {
      long now = DateUtil.CurrentTimeMillis();
      var req = new TigerRequest<OrderBatchResponse>() {
        ApiMethodName = TradeApiService.INACTIVE_ORDERS,
        ModelValue = new QueryOrderModel() {
          Account = account,
          StartDate = now - 30L * 24 * 3600 * 1000,
          EndDate = now,
          Limit = 5
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // filled orders
    await Run("trade/filledOrders", async () => {
      long now = DateUtil.CurrentTimeMillis();
      var req = new TigerRequest<OrderBatchResponse>() {
        ApiMethodName = TradeApiService.FILLED_ORDERS,
        ModelValue = new QueryOrderModel() {
          Account = account,
          StartDate = now - 30L * 24 * 3600 * 1000,
          EndDate = now,
          Limit = 5
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // orderTransactions (by symbol — symbol is required on the wire)
    await Run("trade/orderTransactions by symbol", async () => {
      long now = DateUtil.CurrentTimeMillis();
      var req = new TigerRequest<OrderTransactionsResponse>() {
        ApiMethodName = TradeApiService.ORDER_TRANSACTIONS,
        ModelValue = new OrderTransactionsModel() {
          Account = account,
          Symbol = "01810",
          StartDate = now - 180L * 24 * 3600 * 1000,
          EndDate = now,
          Limit = 20
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // estimateTradableQuantity
    await Run("trade/estimateTradableQuantity", async () => {
      var req = new TigerRequest<EstimateTradableQuantityResponse>() {
        ApiMethodName = TradeApiService.ESTIMATE_TRADABLE_QUANTITY,
        ModelValue = new EstimateTradableQuantityModel() {
          Account = account,
          SecType = SecType.STK,
          Symbol = "AAPL",
          Action = ActionType.BUY,
          OrderType = OrderType.LMT,
          LimitPrice = 150
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // segmentFundAvailable
    await Run("trade/segmentFundAvailable", async () => {
      var req = new TigerRequest<SegmentFundAvailableResponse>() {
        ApiMethodName = TradeApiService.SEGMENT_FUND_AVAILABLE,
        ModelValue = new SegmentFundModel() {
          Account = account,
          FromSegment = SegmentType.SEC,
          Currency = Currency.USD
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // segmentFundHistory
    await Run("trade/segmentFundHistory", async () => {
      var req = new TigerRequest<SegmentFundsResponse>() {
        ApiMethodName = TradeApiService.SEGMENT_FUND_HISTORY,
        ModelValue = new SegmentFundModel() {
          Account = account,
          Limit = 10
        }
      };
      return await tc.ExecuteAsync(req);
    });

    // iceberg place → query → modify → cancel
    {
      ContractItem icebergContract = ContractItem.BuildStockContract("AAPL", Currency.USD.ToString());
      PlaceOrderModel placeModel = PlaceOrderModel.BuildIcebergOrder(
          account, icebergContract, ActionType.BUY, 100, 1.0,
          20, 10, null, PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT, null, null);

      var placeReq = new TigerRequest<PlaceOrderResponse>() {
        ApiMethodName = TradeApiService.PLACE_ORDER,
        ModelValue = placeModel
      };
      PlaceOrderResponse? placeResp = await tc.ExecuteAsync(placeReq);
      bool placeOk = placeResp != null && placeResp.IsSuccess();
      Log("trade/icebergOrder place", placeOk, placeResp != null ? $"code={placeResp.Code} msg={placeResp.Message}" : "null");

      if (placeOk && placeResp!.Data?.Id != null)
      {
        long orderId = placeResp.Data.Id;
        Thread.Sleep(500);

        var queryReq = new TigerRequest<SingleOrderResponse>() {
          ApiMethodName = TradeApiService.ORDERS,
          ModelValue = new QueryOrderModel() { Account = account, Id = orderId }
        };
        SingleOrderResponse? queryResp = await tc.ExecuteAsync(queryReq);
        Log("trade/icebergOrder query", queryResp != null && queryResp.IsSuccess(),
            queryResp != null ? $"status={queryResp.Data?.Status}" : "null");

        var modifyReq = new TigerRequest<TigerDictResponse>() {
          ApiMethodName = TradeApiService.MODIFY_ORDER,
          ModelValue = new ModifyOrderModel() {
            Account = account, Id = orderId,
            LimitPrice = 1.01, TotalQuantity = 100,
            DisplaySize = 30, MinDisplaySize = 15,
            PriceType = PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT,
            OrderType = OrderType.ICEBERG,
          }
        };
        TigerDictResponse? modifyResp = await tc.ExecuteAsync(modifyReq);
        Log("trade/icebergOrder modify", modifyResp != null && modifyResp.IsSuccess(),
            modifyResp != null ? $"code={modifyResp.Code} msg={modifyResp.Message}" : "null");

        Thread.Sleep(500);

        var cancelReq = new TigerRequest<TigerDictResponse>() {
          ApiMethodName = TradeApiService.CANCEL_ORDER,
          ModelValue = new CancelOrderModel() { Account = account, Id = orderId }
        };
        TigerDictResponse? cancelResp = await tc.ExecuteAsync(cancelReq);
        Log("trade/icebergOrder cancel", cancelResp != null && cancelResp.IsSuccess(),
            cancelResp != null ? $"code={cancelResp.Code} msg={cancelResp.Message}" : "null");
      }
    }

    Console.WriteLine();
    Console.WriteLine($"=== Results: {pass} PASS, {fail} FAIL, {pass+fail} total ===");
  }
}
