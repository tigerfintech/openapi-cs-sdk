using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using NUnit.Framework;
using Sample;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Struct;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Push;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Pb;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Trade;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

class Program
{
  static async Task Main(string[] args)
  {
    Console.WriteLine("Hello, World!");
    TigerConfig.LogDir = "/data0/logs/tiger-openapi-cs";
    ApiLogger.DebugEnabled = true;
    ApiLogger.Info("start");

    //StockPriceTests stockPriceTests = new StockPriceTests();
    //stockPriceTests.TestStockPrice();
    //TestStockPrice();

    // tiger config
    TigerConfig config = new TigerConfig()
    {
      ConfigFilePath = "/data0/tiger_config/prod",
      FailRetryCounts = 2, // (optional) range:[1, 5],  default is 2
      AutoGrabPermission = false,   // (optional) default is true
      AutoRefreshToken = false,
      Language = Language.en_US,   // (optional) default is en_US
      TimeZone = CustomTimeZone.HK_ZONE  // (optional) default is HK_ZONE
    };

    //TigerConfig config = new TigerConfig()
    //{
    //  ConfigFilePath = "/data0/tiger_config/test",
    //  FailRetryCounts = 2, // (optional) range:[1, 5],  default is 2
    //  AutoGrabPermission = false,   // (optional) default is true
    //  AutoRefreshToken = false,
    //  Language = Language.en_US,   // (optional) default is en_US
    //  TimeZone = CustomTimeZone.HK_ZONE,  // (optional) default is HK_ZONE
    //  IsSslSocket = true
    //};
    //ApiLogger.DebugEnabled = true;

    QuoteClient quoteClient = new QuoteClient(config);

    // QuoteApiService.USER_LICENSE
    //TigerResponse? response = await GetUserLicenseAsync(quoteClient);
    //TigerResponse? response = await GrabQuotePermissionAsync(quoteClient);
    //TigerResponse? response = await GetQuotePermissionAsync(quoteClient);

    //TigerResponse? response = await GetMarketStateAsync(quoteClient);
    //TigerResponse? response = await GetTradingCalendarAsync(quoteClient);
    //TigerResponse? response = await GetAllSymbolsAsync(quoteClient);
    //TigerResponse? response = await GetAllSymbolNamesAsync(quoteClient);
    //TigerResponse? response = await GetDelayQuoteAsync(quoteClient);
    //TigerResponse? response = await GetTimelineAsync(quoteClient);
    //TigerResponse? response = await GetHistoryTimelineAsync(quoteClient);
    //TigerResponse? response = await GetRealTimeQuoteAsync(quoteClient);
    //TigerResponse? response = await GetOvernightQuoteAsync(quoteClient);
    //TigerResponse? response = await GetKLineAsync(quoteClient);
    //TigerResponse? response = await GetDepthQuoteAsync(quoteClient);

    //TigerResponse? response = await GetTradeTickAsync(quoteClient);
    //TigerResponse? response = await GetStockTradeInfoAsync(quoteClient);
    //TigerResponse? response = await GetStockCaptialFlowAsync(quoteClient);
    //TigerResponse? response = await GetStockCaptialDistributionAsync(quoteClient);
    //TigerResponse? response = await GetStockBrokerAsync(quoteClient);
    //TigerResponse? response = await GetOptionExpirationAsync(quoteClient);
    //TigerResponse? response = await GetOptionChainAsync(quoteClient);
    //TigerResponse? response = await GetOptionBriefAsync(quoteClient);
    //TigerResponse? response = await GetOptionBriefV2Async(quoteClient);
    //TigerResponse? response = await GetOptionKLineAsync(quoteClient);
    //TigerResponse? response = await GetOptionKLineV2Async(quoteClient);
    //TigerResponse? response = await GetOptionTradeTickAsync(quoteClient);
    //TigerResponse? response = await GetOptionDepthAsync(quoteClient);
    //TigerResponse? response = await GetHKOptionSymbolsAsync(quoteClient);

    //TigerResponse? response = await GetFutureExchangeAsync(quoteClient);
    //TigerResponse? response = await GetFutureContractByExchangeCodeAsync(quoteClient);
    //TigerResponse? response = await GetFutureContractByContractCodeAsync(quoteClient);
    // Query all contracts of a specified product
    //TigerResponse? response = await GetFutureContractsAsync(quoteClient);
    // Query the continuous contract of the specified product
    //TigerResponse? response = await GetFutureContinuousContractsAsync(quoteClient);
    // Query the current contract of the specified product
    //TigerResponse? response = await GetFutureCurrentContractAsync(quoteClient);
    // Query the trading time of the specified futures contract
    //TigerResponse? response = await GetFutureTradingDateAsync(quoteClient);
    //TigerResponse? response = await GetFutureKLineAsync(quoteClient);
    //TigerResponse? response = await GetFutureRealTimeQuoteAsync(quoteClient);
    //TigerResponse? response = await GetFutureTickAsync(quoteClient);
    //TigerResponse? response = await GetFutureHistoryMainContractAsync(quoteClient);
    //TigerResponse? response = await GetQuoteContractAsync(quoteClient);

    // Stock screener
    //TigerResponse? response = await FilterSymbolsAsync(quoteClient);
    // Obtain industry and concept data filtered by multi-label stock screener
    //TigerResponse? response = await GetMultiFieldTags(quoteClient);

    // warrant/cbbc
    //TigerResponse? response = await FilterWarrantAsync(quoteClient);
    //TigerResponse? response = await GetWarrantQuoteAsync(quoteClient);

    // fund quote
    //TigerResponse? response = await GetAllFundSymbolsAsync(quoteClient);
    //TigerResponse? response = await GetFundContractsAsync(quoteClient);
    //TigerResponse? response = await GetFundQuoteAsync(quoteClient);
    //TigerResponse? response = await GetFundHistoryQuoteAsync(quoteClient);

    // fundamental data
    //TigerResponse? response = await GetCorporateDividendAsync(quoteClient);
    //TigerResponse? response = await GetKlineQuotaAsync(quoteClient);
    //TigerResponse? response = await GetFinancialCurrencyAsync(quoteClient);
    //TigerResponse? response = await GetFinancialExchangeRateAsync(quoteClient);
    //QuoteStockFundamentalResponse? fundamentalResponse = await GetStockFundamentalAsync(quoteClient);
    //ApiLogger.Info("response:" + JsonConvert.SerializeObject(fundamentalResponse?.GetStockFundamentalItems()));

    //TigerResponse? response = await GetStockTradeRankAsync(quoteClient);

    //ApiLogger.Info("response:" + JsonConvert.SerializeObject(response));

    // =================================================trade
    TradeClient tradeClient = new TradeClient(config);
    //TigerResponse? response = await GetContractAsync(tradeClient);
    //TigerResponse? response = await GetContractsAsync(tradeClient);
    //TigerResponse? response = await GetAccountsAsync(tradeClient);
    //TigerResponse? response = await GetPositionsAsync(tradeClient);
    //TigerResponse? response = await GetGlobalAssetsAsync(tradeClient);
    //TigerResponse? response = await GetPrimeAssetsAsync(tradeClient);
    //TigerResponse? response = await GetAssetsAnalyticsAsync(tradeClient);

    // =================================================palace order
    //TigerResponse? response = await PlaceOrderAsync(tradeClient);

    //TigerResponse? response = await PlaceForexOrderAsync(tradeClient);

    //TigerResponse? response = await PlaceMarketOrderAsync(tradeClient);
    // response:{"code":0,"message":"success","timestamp":1672900459461,"data":{"id":29360293078500352,"subIds":[],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360293078500352,"orderId":1456,"account":"20200821144442583","action":"BUY","orderType":"MKT","totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":false,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672900459000,"updateTime":1672900459000,"latestTime":1672900459000,"name":"XIAOMI-W","latestPrice":11.62,"attrDesc":"","userMark":"","algoStrategy":"MKT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"y3iIACssSMlurcA3TP+PZQOF0p519WqWPpQG6Y8pYKQTKeePXPv1xZwjq0J97JBxnBr92bL20cZr1J/zQCPvvvtkQNZc3QGRx08dCDfp4AUjoBBzRBuQw+xNSMUsnlY/4G1KbXoOXj5qJ3OycZeFQVxbPeJlSYEt4JJz5LhjBNs="} 

    //TigerResponse? response = await PlaceFundOrderAsync(tradeClient);
    // response:{"data":{"id":31672004552294400,"subIds":[],"orders":[{"symbol":"IE00B464Q616.USD","market":"SG","secType":"FUND","currency":"USD","expiry":null,"strike":null,"right":null,"multiplier":0.0,"identifier":"IE00B464Q616.USD","id":31672004552294400,"orderId":144,"externalId":"144","parentId":0,"account":"13810712","action":"BUY","orderType":"MKT","limitPrice":0.0,"auxPrice":0.0,"trailingPercent":0.0,"totalQuantity":0,"filledQuantity":0,"filledQuantityScale":0,"totalCashAmount":180.0,"filledCashAmount":0.0,"refundCashAmount":0.0,"lastFillPrice":0.0,"avgFillPrice":0.0,"timeInForce":"GTC","expireTime":0,"goodTillDate":null,"outsideRth":false,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"triggerStatus":null,"openTime":1690537418000,"updateTime":1690537418000,"latestTime":1690537418000,"name":"ASIA STRATEGIC INTEREST BOND FUND \"E\" (USD) INC MONTHLY","latestPrice":6.91,"attrDesc":"","userMark":"","attrList":["MONETARY","FRACTIONAL_SHARE","AUTO_SWEEP"],"ocaGroupId":0,"comboLegs":null,"allocAccounts":null,"allocShares":null,"algoStrategy":"MKT","algoParameters":null,"status":"Initial","source":"OpenApi","discount":0.0,"canModify":false,"canCancel":true,"isOpen":true,"comboType":null,"comboTypeDesc":null,"legs":null}]},"code":0,"message":"success","timestamp":1690537302312,"sign":"TMWDEbPXyjl1+GWvC1ZGfkJPDRQfBfiSiWhV7x6ViiQ17zz5b1q6qz6MhZSZElha17JTHFc3HkWyVhbowlbfac5d9GPX/z9AsvKZrTytLdQdMwblNVM8uh40VB5Fzj98ONpKllOBt8zR+kIwvrTbx6mDCeeG1WkTaiZbGI1F7ak="} 

    //TigerResponse? response = await PlaceLimitOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672900550991,"data":{"id":29360305075913728,"subIds":[],"orders":[{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","identifier":"AAPL","id":29360305075913728,"orderId":1457,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":120.0,"totalQuantity":1,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"You order[BUY 1 AAPL] will not be placed until 2023-01-05 04:00:00, local time of the exchange","liquidation":false,"openTime":1672900550000,"updateTime":1672900550000,"latestTime":1672900551000,"name":"Apple","latestPrice":126.625,"attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"vgRX7Z8v2dYNqtzI1RoqD2A7GTOPckQLrN4dOv29l0bcF4GUzNLIRfQd5PPb6o3coV91PfqSPGSlzdRYfUCgMbeZaUPkOtd9v+5KZD6wwyjzT6gviZIYjbPSdboTe64cZ/g8uL3MO/SMLh4SrwLaHbmu9yGf0QgXoL83wjDDgIU="} 
    // response:{"data":{"id":29360305075913728,"subIds":[],"orders":[{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","expiry":null,"strike":null,"right":null,"multiplier":0.0,"identifier":"AAPL","id":29360305075913728,"orderId":1457,"parentId":0,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":120.0,"auxPrice":0.0,"trailingPercent":0.0,"totalQuantity":1,"filledQuantity":0,"cashQuantity":0.0,"lastFillPrice":0.0,"avgFillPrice":0.0,"timeInForce":"DAY","expireTime":0,"goodTillDate":null,"outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"You order[BUY 1 AAPL] will not be placed until 2023-01-05 04:00:00, local time of the exchange","liquidation":false,"openTime":1672900550000,"updateTime":1672900550000,"latestTime":1672900551000,"name":"Apple","latestPrice":126.625,"attrDesc":"","userMark":"","ocaGroupId":0,"comboLegs":null,"allocAccounts":null,"allocShares":null,"algoStrategy":"LMT","algoParameters":null,"status":"Initial","source":null,"discount":0.0,"canModify":true,"canCancel":true}]},"code":0,"message":"success","timestamp":1672900550991,"sign":"vgRX7Z8v2dYNqtzI1RoqD2A7GTOPckQLrN4dOv29l0bcF4GUzNLIRfQd5PPb6o3coV91PfqSPGSlzdRYfUCgMbeZaUPkOtd9v+5KZD6wwyjzT6gviZIYjbPSdboTe64cZ/g8uL3MO/SMLh4SrwLaHbmu9yGf0QgXoL83wjDDgIU="} 

    //TigerResponse? response = await PlaceOddLotOrderAsync(tradeClient);

    //TigerResponse? response = await PlaceAuctionOrderAsync(tradeClient);

    //TigerResponse? response = await PlaceStopOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672900788269,"data":{"id":29360336176021504,"subIds":[],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360336176021504,"orderId":1458,"account":"20200821144442583","action":"SELL","orderType":"STP","auxPrice":10.0,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":false,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672900788000,"updateTime":1672900788000,"latestTime":1672900788000,"name":"XIAOMI-W","latestPrice":11.6,"attrDesc":"","userMark":"","algoStrategy":"STP","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"uuwso0alUH1JQfMMBMNfXvYqNoWw604OVQMWKmwNY2IqRpuoVweYx95FyWgEF/Ey2EZRdCOjRdk9eSjfn5YlC1i507COKbP5NprHnE6QJrgkNnuR1Ap2gGO7iTbF2ZB8I7SF8BQHBBCPF0PA3myAc2ZApbc2bM4XEGcermQKjCU="} 
    // response:{"data":{"id":29360336176021504,"subIds":[],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360336176021504,"orderId":1458,"account":"20200821144442583","action":"SELL","orderType":"STP","auxPrice":10.0,"totalQuantity":200,"timeInForce":"DAY","remark":"","openTime":1672900788000,"updateTime":1672900788000,"latestTime":1672900788000,"name":"XIAOMI-W","latestPrice":11.6,"attrDesc":"","userMark":"","algoStrategy":"STP","status":"Initial","canModify":true,"canCancel":true}]},"message":"success","timestamp":1672900788269,"sign":"uuwso0alUH1JQfMMBMNfXvYqNoWw604OVQMWKmwNY2IqRpuoVweYx95FyWgEF/Ey2EZRdCOjRdk9eSjfn5YlC1i507COKbP5NprHnE6QJrgkNnuR1Ap2gGO7iTbF2ZB8I7SF8BQHBBCPF0PA3myAc2ZApbc2bM4XEGcermQKjCU="}

    //TigerResponse? response = await PlaceStopLimitOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672900884128,"data":{"id":29360348738880512,"subIds":[],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360348738880512,"orderId":1459,"account":"20200821144442583","action":"SELL","orderType":"STP_LMT","limitPrice":10.0,"auxPrice":10.3,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672900884000,"updateTime":1672900884000,"latestTime":1672900884000,"name":"XIAOMI-W","latestPrice":11.6,"attrDesc":"","userMark":"","algoStrategy":"STP_LMT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"LVBbhwQCxltV2BBAPoDtcW+F3DYC9Z4nb7Kou2UvxoNs8HuYNNlGLainFjp5hUzb0dn5Gu7lSj8XPWFiY3so0ScqBZK86Fdy0WqOSVHXvvNRw/fymgNqZVLDPYg6e8MH45XQKsgylSnCPeevcB59J39TWGsHp8MZ76gzpgAtrsw="} 
    // response:{"data":{"id":29360348738880512,"subIds":[],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360348738880512,"orderId":1459,"account":"20200821144442583","action":"SELL","orderType":"STP_LMT","limitPrice":10.0,"auxPrice":10.3,"totalQuantity":200,"timeInForce":"DAY","outsideRth":true,"remark":"","openTime":1672900884000,"updateTime":1672900884000,"latestTime":1672900884000,"name":"XIAOMI-W","latestPrice":11.6,"attrDesc":"","userMark":"","algoStrategy":"STP_LMT","status":"Initial","canModify":true,"canCancel":true}]},"message":"success","timestamp":1672900884128,"sign":"LVBbhwQCxltV2BBAPoDtcW+F3DYC9Z4nb7Kou2UvxoNs8HuYNNlGLainFjp5hUzb0dn5Gu7lSj8XPWFiY3so0ScqBZK86Fdy0WqOSVHXvvNRw/fymgNqZVLDPYg6e8MH45XQKsgylSnCPeevcB59J39TWGsHp8MZ76gzpgAtrsw="} 

    //TigerResponse? response = await PlaceTrailOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672900949385,"data":{"id":29360357293293568,"subIds":[],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360357293293568,"orderId":1460,"account":"20200821144442583","action":"SELL","orderType":"TRAIL","trailingPercent":10.0,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":false,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672900949000,"updateTime":1672900949000,"latestTime":1672900949000,"name":"XIAOMI-W","latestPrice":11.6,"attrDesc":"","userMark":"","algoStrategy":"TRAIL","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"Vn6XRXeDnmdpm8tPlmENjDy+8tjpU1EDJ0QT7mPkmG0PDkxTAUoEhutA1zMIff7D0benQvo+zrVkNktPmrcRbn7OsuC4HUDfZa7nXNkfKMF4It2RxdNGPDFV8qHWeVLNj1czwe8I3OYYo2wKjEELeFMxRXNrDQr7S9rXRGq5jIc="} 
    // response:{"data":{"id":29360357293293568,"subIds":[],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360357293293568,"orderId":1460,"account":"20200821144442583","action":"SELL","orderType":"TRAIL","trailingPercent":10.0,"totalQuantity":200,"timeInForce":"DAY","remark":"","openTime":1672900949000,"updateTime":1672900949000,"latestTime":1672900949000,"name":"XIAOMI-W","latestPrice":11.6,"attrDesc":"","userMark":"","algoStrategy":"TRAIL","status":"Initial","canModify":true,"canCancel":true}]},"message":"success","timestamp":1672900949385,"sign":"Vn6XRXeDnmdpm8tPlmENjDy+8tjpU1EDJ0QT7mPkmG0PDkxTAUoEhutA1zMIff7D0benQvo+zrVkNktPmrcRbn7OsuC4HUDfZa7nXNkfKMF4It2RxdNGPDFV8qHWeVLNj1czwe8I3OYYo2wKjEELeFMxRXNrDQr7S9rXRGq5jIc="} 

    //TigerResponse? response = await PlaceProfitTakerOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672901030957,"data":{"id":29360367983789056,"subIds":[29360367983656960],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360367983656960,"orderId":1462,"parentId":29360367983789056,"account":"20200821144442583","action":"SELL","orderType":"LMT","limitPrice":13.0,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672901030000,"updateTime":1672901030000,"latestTime":1672901031000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360367983789056,"orderId":1461,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":11.0,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672901030000,"updateTime":1672901030000,"latestTime":1672901031000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"Te+3j0bJjy0FpqlIBRHZuPkqZ0HTeL1kmzVRF5AAtfZgL8Fe/ZeIwPUPCN0VZU16avqAx3SNAKor9xF5tHjZVRSJY28cjULAsSCExRur7gLSXkgCj/TgRqsSU28afHAYsT8Xtt2zj5i6+LJFJfUXTUyjDlbyzIhbgkZ+Wspik4g="} 
    // response:{"data":{"id":29360367983789056,"subIds":[29360367983656960],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360367983656960,"orderId":1462,"parentId":29360367983789056,"account":"20200821144442583","action":"SELL","orderType":"LMT","limitPrice":13.0,"totalQuantity":200,"timeInForce":"DAY","outsideRth":true,"remark":"","openTime":1672901030000,"updateTime":1672901030000,"latestTime":1672901031000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Initial","canModify":true,"canCancel":true},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360367983789056,"orderId":1461,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":11.0,"totalQuantity":200,"timeInForce":"DAY","outsideRth":true,"remark":"","openTime":1672901030000,"updateTime":1672901030000,"latestTime":1672901031000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Initial","canModify":true,"canCancel":true}]},"message":"success","timestamp":1672901030957,"sign":"Te+3j0bJjy0FpqlIBRHZuPkqZ0HTeL1kmzVRF5AAtfZgL8Fe/ZeIwPUPCN0VZU16avqAx3SNAKor9xF5tHjZVRSJY28cjULAsSCExRur7gLSXkgCj/TgRqsSU28afHAYsT8Xtt2zj5i6+LJFJfUXTUyjDlbyzIhbgkZ+Wspik4g="} 

    //TigerResponse? response = await PlaceStopLossOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672901122176,"data":{"id":29360379940044800,"subIds":[29360379940570112],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360379940044800,"orderId":1463,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":11.0,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672901122000,"updateTime":1672901122000,"latestTime":1672901122000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"test001","algoStrategy":"LMT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360379940570112,"orderId":1464,"parentId":29360379940044800,"account":"20200821144442583","action":"SELL","orderType":"STP","auxPrice":10.0,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":false,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672901122000,"updateTime":1672901122000,"latestTime":1672901122000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"test001","algoStrategy":"STP","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"S14+11KRYRtBjxuiykwf8CReVTyQvpnTNfMim3gfZleWdum9iaXcWB3qTAsf/Gnpov9K1h0o+qt/JscYTjdqs6JZ5He3TxtiI2cCKqQySZeVQt7o8hR6Tgd90qqklfhloRJfU7k26yguzQ/zWKYqj0GixqhXOhLVRXoTOd8BJS0="} 
    // response:{"data":{"id":29360379940044800,"subIds":[29360379940570112],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360379940044800,"orderId":1463,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":11.0,"totalQuantity":200,"timeInForce":"DAY","outsideRth":true,"remark":"","openTime":1672901122000,"updateTime":1672901122000,"latestTime":1672901122000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"test001","algoStrategy":"LMT","status":"Initial","canModify":true,"canCancel":true},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360379940570112,"orderId":1464,"parentId":29360379940044800,"account":"20200821144442583","action":"SELL","orderType":"STP","auxPrice":10.0,"totalQuantity":200,"timeInForce":"DAY","remark":"","openTime":1672901122000,"updateTime":1672901122000,"latestTime":1672901122000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"test001","algoStrategy":"STP","status":"Initial","canModify":true,"canCancel":true}]},"message":"success","timestamp":1672901122176,"sign":"S14+11KRYRtBjxuiykwf8CReVTyQvpnTNfMim3gfZleWdum9iaXcWB3qTAsf/Gnpov9K1h0o+qt/JscYTjdqs6JZ5He3TxtiI2cCKqQySZeVQt7o8hR6Tgd90qqklfhloRJfU7k26yguzQ/zWKYqj0GixqhXOhLVRXoTOd8BJS0="} 

    //TigerResponse? response = await PlaceStopLossLimitOrderAsync(tradeClient);
    //

    //TigerResponse? response = await PlaceStopLossTrailOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672901251519,"data":{"id":29360396893815808,"subIds":[29360396894077952],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360396893815808,"orderId":1465,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":11.0,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672901251000,"updateTime":1672901251000,"latestTime":1672901251000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"test-attach-stoplosstrail","algoStrategy":"LMT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360396894077952,"orderId":1466,"parentId":29360396893815808,"account":"20200821144442583","action":"SELL","orderType":"TRAIL","trailingPercent":10.0,"totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":false,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672901251000,"updateTime":1672901251000,"latestTime":1672901251000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"test-attach-stoplosstrail","algoStrategy":"TRAIL","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"hMhZcQVUopavskz7pwaXDLq+xmOIyBkmUUm9IeKcq3zUtXjjySKbAgEIYJxEnWmhy3ikIuhB+aau0iwaufHudhaOWamUaNF/2IJHY70ml+a2MsgwNW7mF04vjY7fOcYB6PPZc3n8XlwBB+Ax7eWGsKXeEKnKeotzbtwd+BWS4xw="} 
    // response:{"data":{"id":29360396893815808,"subIds":[29360396894077952],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360396893815808,"orderId":1465,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":11.0,"totalQuantity":200,"timeInForce":"DAY","outsideRth":true,"remark":"","openTime":1672901251000,"updateTime":1672901251000,"latestTime":1672901251000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"test-attach-stoplosstrail","algoStrategy":"LMT","status":"Initial","canModify":true,"canCancel":true},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360396894077952,"orderId":1466,"parentId":29360396893815808,"account":"20200821144442583","action":"SELL","orderType":"TRAIL","trailingPercent":10.0,"totalQuantity":200,"timeInForce":"DAY","remark":"","openTime":1672901251000,"updateTime":1672901251000,"latestTime":1672901251000,"name":"XIAOMI-W","latestPrice":11.58,"attrDesc":"","userMark":"test-attach-stoplosstrail","algoStrategy":"TRAIL","status":"Initial","canModify":true,"canCancel":true}]},"message":"success","timestamp":1672901251519,"sign":"hMhZcQVUopavskz7pwaXDLq+xmOIyBkmUUm9IeKcq3zUtXjjySKbAgEIYJxEnWmhy3ikIuhB+aau0iwaufHudhaOWamUaNF/2IJHY70ml+a2MsgwNW7mF04vjY7fOcYB6PPZc3n8XlwBB+Ax7eWGsKXeEKnKeotzbtwd+BWS4xw="} 

    //TigerResponse? response = await PlaceBracketsOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672906085365,"data":{"id":283514341002915840,"orderId":6083,"subIds":[4536229456082436100,4536229456082436117]},"sign":"ghQp0hr3kB6jHgd4x80AwPfCf/KWoJrTY3BputVciU2bLCtsNANvJAr1iOGUzCsKmSqv7bMg3xb0SUgkPxS2kid13XCHSIZjmeZ98s60H54ka99V2qhdYj7efqozLaHfNNx40+DdmFZnclqZZnkcGgbbKVowujQGUFxqNjdhZkM="} 

    //TigerResponse? response = await PlaceTWAPOrderAsync(tradeClient);
    //TigerResponse? response = await PlaceVWAPOrderAsync(tradeClient);
    //TigerResponse? response = await PlaceMultiLegOrderAsync(tradeClient);
    //TigerResponse? response = await PlaceOCABracketsOrderAsync(tradeClient);

    // =================================================modify/cancel order
    //TigerResponse? response = await ModifyOrderAsync(tradeClient);
    // response:{"data":{"id":29360305075913728},"message":"success","timestamp":1672916823517,"sign":"qVwjFIhphTMTblhMQ2S1Py916Q0YJ/MjlOfUJxyqCKoAYfxfhiKkUGnL6CCO5Sak7IfQkkIgemPorcyTrfOZIXK8hxVayzPB3lYRo/Ip9woJA5Muh8eFYdqcR000GJreBPAEVl6B8t+mzrptjRS798QCxi/uxsbv6WzSCdmd1XY="} 

    //TigerResponse? response = await CancelOrderAsync(tradeClient);
    // response:{"data":{"id":29360305075913728},"message":"success","timestamp":1672917172001,"sign":"QZnYKt55byk4/jwCsniS1y+BExrEUUGHiNGmRMGnBPBnR8nW8Knu15hUjjOfonDhIV+xpGLb3LrBt6hEEp8+dhG/aflx4CCCf5ivMNiOPk1epr4gMGlFmclTmqf9c5Mc3rqGqKxM3b05Qk9bLr4OLCPPk7oA6b86rvnc/ZHymWM="} 

    // =================================================query order
    //TigerResponse? response = await QueryOrderByIdAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672919642606,"data":{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","identifier":"AAPL","id":29360305075913728,"orderId":1457,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":121.0,"totalQuantity":2,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":true,"commission":0.0,"realizedPnl":0.0,"liquidation":false,"openTime":1672900550000,"updateTime":1672917172000,"latestTime":1672917172000,"name":"Apple","attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Cancelled","discount":0.0,"canModify":false,"canCancel":false},"sign":"JKLW5HivW+CF4kbRlEkNMgj2MM74wCaG1pUlkMy9FBZzQiD4PZN/rTAvg6bYOIkrpXZO2PmQgRY8o/Bs73a3nRutLq5y7i04zeUbWu7Zh9k3wsx2/iQiVv6RtXQaWwrzTru4+NsYqq6F3gqoT6WHXDifywqCSLmqu8UJAzysSb4="} 
    // response:{"data":{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","identifier":"AAPL","id":29360305075913728,"orderId":1457,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":121.0,"totalQuantity":2,"timeInForce":"DAY","outsideRth":true,"openTime":1672900550000,"updateTime":1672917172000,"latestTime":1672917172000,"name":"Apple","attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Cancelled"},"message":"success","timestamp":1672919642606,"sign":"JKLW5HivW+CF4kbRlEkNMgj2MM74wCaG1pUlkMy9FBZzQiD4PZN/rTAvg6bYOIkrpXZO2PmQgRY8o/Bs73a3nRutLq5y7i04zeUbWu7Zh9k3wsx2/iQiVv6RtXQaWwrzTru4+NsYqq6F3gqoT6WHXDifywqCSLmqu8UJAzysSb4="} 

    //TigerResponse? response = await QueryOrderAsync(tradeClient);
    // response:{"data":{"nextPageToken":"b3JkZXJzfDE2NzE0NjU2MDAwMDB8MTY3MjkyMDk2MTU5MXwyOTM2MDM2Nzk4MzY1Njk2MA==","items":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360396894077952,"orderId":1466,"parentId":29360396893815808,"account":"20200821144442583","action":"SELL","orderType":"TRAIL","trailingPercent":10.0,"totalQuantity":200,"timeInForce":"DAY","remark":"The primary order has expired","openTime":1672901251000,"updateTime":1672901251000,"latestTime":1672901252000,"name":"XIAOMI-W","attrDesc":"","userMark":"test-attach-stoplosstrail","algoStrategy":"TRAIL","status":"Invalid"},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360396893815808,"orderId":1465,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":11.0,"totalQuantity":200,"timeInForce":"DAY","outsideRth":true,"remark":"The current order is cross-trading with your pending sell order of the same holders account","openTime":1672901251000,"updateTime":1672901251000,"latestTime":1672901252000,"name":"XIAOMI-W","attrDesc":"","userMark":"test-attach-stoplosstrail","algoStrategy":"LMT","status":"Invalid"},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360379940570112,"orderId":1464,"parentId":29360379940044800,"account":"20200821144442583","action":"SELL","orderType":"STP","auxPrice":10.0,"totalQuantity":200,"timeInForce":"DAY","remark":"The primary order has expired","openTime":1672901122000,"updateTime":1672901122000,"latestTime":1672901122000,"name":"XIAOMI-W","attrDesc":"","userMark":"test001","algoStrategy":"STP","status":"Invalid"},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360379940044800,"orderId":1463,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":11.0,"totalQuantity":200,"timeInForce":"DAY","outsideRth":true,"remark":"The current order is cross-trading with your pending sell order of the same holders account","openTime":1672901122000,"updateTime":1672901122000,"latestTime":1672901122000,"name":"XIAOMI-W","attrDesc":"","userMark":"test001","algoStrategy":"LMT","status":"Invalid"},{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360367983656960,"orderId":1462,"parentId":29360367983789056,"account":"20200821144442583","action":"SELL","orderType":"LMT","limitPrice":13.0,"totalQuantity":200,"timeInForce":"DAY","outsideRth":true,"remark":"The primary order has expired","openTime":1672901030000,"updateTime":1672901030000,"latestTime":1672901031000,"name":"XIAOMI-W","attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Invalid"}]},"message":"success","timestamp":1672920984579,"sign":"iHos8Cj27VV64rMsmHweSOk8n6dAPgGWoRaGVqzA0+MLiNZv1MlBLfhg+UdhGyL4znuALK+KtRZ8TI0LhrcCHsZWeqVIajYN6Vyvx7vn9lshUmzhZ8f+a7DLrk3lkqLay8HnMM+j6BuRSaDaUWfsIHeT/1mYM586nhskIrZnFVU="}

    //TigerResponse? response = await QueryFilledOrderAsync(tradeClient);
    // response:{"data":{"items":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29358459894498304,"orderId":1442,"account":"20200821144442583","action":"BUY","orderType":"MKT","totalQuantity":200,"filledQuantity":200,"avgFillPrice":11.7,"timeInForce":"DAY","commission":22.36,"remark":"","openTime":1672886473000,"updateTime":1672886473000,"latestTime":1672886474000,"name":"XIAOMI-W","attrDesc":"","userMark":"","algoStrategy":"MKT","status":"Filled"}]},"message":"success","timestamp":1672973293876,"sign":"dfpwbO8D3hz43IuW7Hlm2vHD+ZLsY4gV/yFYJBoL4ZqyFvz0WVt0FscodcMRPKPR6H5UoWNMm+nzoEKojKpp93HGtt7eDO6pu6DUs8nu59MvNUKjFzlfJobZVkPFFKZURJdOzlWXodMP7KSOAr0VOxGKKcqSJ0vb7sZ32zpQIc8="} 

    //TigerResponse? response = await QueryActiveOrderAsync(tradeClient);

    //TigerResponse? response = await QueryInactiveOrderAsync(tradeClient);
    // response:{"data":{"nextPageToken":"b3JkZXJzfDE2NzE0NjU2MDAwMDB8MTY3Mjk3MzY5MTE5OHwyOTI5MjQ4NzM2NDE4MjAxNg==","items":[{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","identifier":"AAPL","id":29306883734374400,"orderId":639,"account":"572386","action":"BUY","orderType":"LMT","limitPrice":101.0,"totalQuantity":1,"timeInForce":"DAY","outsideRth":true,"remark":"Order is expired","openTime":1672492978000,"updateTime":1672794605000,"latestTime":1672794605000,"name":"Apple","attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Cancelled","source":"OpenApi"},{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","identifier":"AAPL","id":29306880086114304,"orderId":638,"account":"572386","action":"BUY","orderType":"LMT","limitPrice":101.0,"totalQuantity":1,"timeInForce":"DAY","outsideRth":true,"remark":"Order is expired","openTime":1672492950000,"updateTime":1672794605000,"latestTime":1672794605000,"name":"Apple","attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Cancelled","source":"OpenApi"},{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","identifier":"AAPL","id":29292487364182016,"orderId":637,"account":"572386","action":"BUY","orderType":"LMT","limitPrice":100.0,"totalQuantity":1,"timeInForce":"DAY","outsideRth":true,"remark":"Order is expired","openTime":1672383142000,"updateTime":1672449011000,"latestTime":1672449011000,"name":"Apple","attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Cancelled","source":"OpenApi"}]},"message":"success","timestamp":1672973709154,"sign":"rVLFKRHPCJvcO5XbbCmiYiyILoXnEwepSxy35Jyk91CLFvzdGhGZF4xu2nXW/T2DGyHLNSFZCYAsRgYupD2e/3ewUqXm7ikgiVv6DsyZjq+MC6BKSA5tQgrA/jIYmFUjf8lruR8Sq+9sBDnjIaq4jzSMjUb+xUFW/V90YybmsNg="} 

    //TigerResponse? response = await QueryOrderTransactionsAsync(tradeClient);
    // response:{"data":{"items":[{"id":28805813759117312,"orderId":28805688059365376,"accountId":"572386","secType":"STK","market":"HK","currency":"HKD","symbol":"01810","right":"PUT","action":"SELL","filledQuantity":200,"filledPrice":10.32,"filledAmount":2064.0,"transactedAt":"2022-11-17 15:28:37","transactionTime":1668670117000}]},"message":"success","timestamp":1672977133300,"sign":"mfQ7wBB785UReYysC2TcD+1Wo6+sz8l5NzQKofvxD5uSNdAs+Jl/qaiYSwEobQBE1gvJ3bH1JPynlN2DyEG3E6WfD1Lbsqdy4XDcO2UKWIbUpbioW0SLT0WTT/Wr9hX6/uH1xgg3FitL40IX7sR2e40+fa1AmyTaRZkRrGIENk0="} 

    // segment fund transfer
    //TigerResponse? response = await QueryAvailableSegmentFundAsync(tradeClient);

    //TigerResponse? response = await TransferSegmentFundAsync(tradeClient);

    //TigerResponse? response = await CancelSegmentFundAsync(tradeClient, 30359957871001600L);

    //TigerResponse? response = await QueryTransferFundsAsync(tradeClient);

    //TigerResponse? response = await GetMaxTradableQuantityAsync(tradeClient);
    // response:{"data":{"tradableQuantity":15987.0,"financingQuantity":51481.0,"positionQuantity":4.0,"tradablePositionQuantity":4.0},"code":0,"message":"success","timestamp":1681372230837,"sign":"ZwrIOjOZCrpJIoW1FEbTTR1sqq+9CxxSZupMhUOedCC79telTq0jRN2NnaHw74UdXKI+gid/JGd8wMo6xJU8l3dUzmyGjVuPLhN36zEA3B0aB9L6l4pX5aRrhtcAd7x9xlWm7KL6CqRX+dZFibqknHvC+y9u+rkFCoQNqUErZMU="} 

    //TigerResponse? response = await QueryDepositWithdrawFundsAsync(tradeClient);

    //ApiLogger.Info("response:" + JsonConvert.SerializeObject(response));
    //QueryOrderUsePageTokenAsync(tradeClient);
    Thread.Sleep(1000);


    // =================================================option fundamentals
    //GetOptionFundamentals(quoteClient);

    // =================================================Push
    await SubscribePushAsync();

    ApiLogger.Info("end");
  }

  static void GetOptionFundamentals(QuoteClient quoteClient)
  {
    try
    {
      OptionFundamentals? optionFundamentals = OptionCalcUtil.GetOptionFundamentals(
          quoteClient, "TSLA", "CALL", "255.0", "2023-08-11");
      ApiLogger.Info("response:" + JsonConvert.SerializeObject(optionFundamentals));
    }
    catch (Exception e)
    {
      ApiLogger.Error("GetOptionFundamentals failed.", e);
    }
  }

  static async Task SubscribePushAsync()
  {
    // tiger config
    TigerConfig config = new TigerConfig()
    {
      ConfigFilePath = "/data0/tiger_config/prod",
      FailRetryCounts = 2, // (optional) range:[1, 5],  default is 2
      AutoGrabPermission = true,   // (optional) default is true
      AutoRefreshToken = false,
      Language = Language.en_US,   // (optional) default is en_US
      TimeZone = CustomTimeZone.HK_ZONE,  // (optional) default is HK_ZONE
      UseFullTick = false,
      IsSslSocket = true
    };
    ApiLogger.DebugEnabled = false;

    IApiComposeCallback callback = new DefaultApiComposeCallback();
    PushClient client = PushClient.GetInstance().Config(config)
      .ApiComposeCallback(callback);
    ApiLogger.Info($"======================{client.GetUrl()}");
    await client.ConnectAsync();

    SubscribeAsset();
    //SubscribeQuote();
    //SubscribeTradeTick();
    //SubscribeStockTop();
    //SubscribeOptionTop();
    //SubscribeKline();
  }

  public static void SubscribeKline()
  {
    PushClient client = PushClient.GetInstance();

    ISet<string> symbols = new HashSet<string>();
    symbols.Add("AAPL");
    ApiLogger.Info($"SubscribeKline:{client.SubscribeKline(symbols)}");
    Sleep(90);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    ApiLogger.Info($"CancelSubscribeKline:{client.CancelSubscribeKline(symbols)}");
    Sleep(2);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(2);
  }

  public static void SubscribeAsset()
  {
    PushClient client = PushClient.GetInstance();
    ApiLogger.Info($"Subscribe Asset:{client.Subscribe(Subject.Asset)}");
    Sleep(30);
    ApiLogger.Info($"CancelSubscribe Asset:{client.CancelSubscribe(Subject.Asset)}");
    Sleep(2);
  }

  public static void SubscribeQuote()
  {
    PushClient client = PushClient.GetInstance();

    ISet<string> symbols = new HashSet<string>();
    symbols.Add("AAPL");
    symbols.Add("TSLA");
    ApiLogger.Info($"SubscribeQuote:{client.SubscribeQuote(symbols)}");
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(30);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    // Cancel all quote subscriptions(Security, Options, Futures)
    ApiLogger.Info($"CancelSubscribeQuote:{client.CancelSubscribeQuote()}");
    //ApiLogger.Info($"CancelSubscribeQuote:{client.CancelSubscribeQuote(symbols)}");
    Sleep(2);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(2);
  }

  public static void SubscribeTradeTick()
  {
    PushClient client = PushClient.GetInstance();

    ISet<string> symbols = new HashSet<string>();
    symbols.Add("AAPL");
    ApiLogger.Info($"SubscribeTradeTick:{client.SubscribeTradeTick(symbols)}");
    Sleep(30);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    ApiLogger.Info($"CancelSubscribeTradeTick:{client.CancelSubscribeTradeTick(symbols)}");
    Sleep(2);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(2);
  }

  public static void SubscribeStockTop()
  {
    PushClient client = PushClient.GetInstance();

    Market market = Market.US;
    ISet<Indicator> indicators = new HashSet<Indicator>();
    indicators.Add(StockRankingIndicator.Amplitude);
    indicators.Add(StockRankingIndicator.TurnoverRate);
    ApiLogger.Info($"SubscribeStockTop:{client.SubscribeStockTop(market)}");
    Sleep(10);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(100);
    ApiLogger.Info($"CancelSubscribeStockTop:{client.CancelSubscribeStockTop(market)}");
    Sleep(2);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(2);
  }


  public static void SubscribeOptionTop()
  {
    PushClient client = PushClient.GetInstance();

    Market market = Market.US;
    ISet<Indicator> indicators = new HashSet<Indicator>();
    indicators.Add(OptionRankingIndicator.Amount);
    indicators.Add(OptionRankingIndicator.OpenInt);
    ApiLogger.Info($"SubscribeStockTop:{client.SubscribeOptionTop(market)}");
    Sleep(10);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(100);
    ApiLogger.Info($"CancelSubscribeStockTop:{client.CancelSubscribeOptionTop(market)}");
    Sleep(2);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(2);
  }

  static void Sleep(int seconds)
  {
    Thread.Sleep(TimeSpan.FromSeconds(seconds));
  }

  static async Task<QuoteTradeRankResponse?> GetStockTradeRankAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteTradeRankResponse> request = new TigerRequest<QuoteTradeRankResponse>()
    {
      ApiMethodName = QuoteApiService.TRADE_RANK,
      ModelValue = new QuoteTradeRankModel()
      {
        Market = Market.US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteStockFundamentalResponse?> GetStockFundamentalAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteStockFundamentalResponse> request = new TigerRequest<QuoteStockFundamentalResponse>()
    {
      ApiMethodName = QuoteApiService.STOCK_FUNDAMENTAL,
      ModelValue = new QuoteStockFundamentalModel()
      {
        Symbols = new List<string> { "AAPL", "MSFT" },
        Market = Market.US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FinancialExchangeRateResponse?> GetFinancialExchangeRateAsync(QuoteClient quoteClient)
  {
    List<string> currencyList = new List<string>();
    currencyList.Add("USD");
    currencyList.Add("HKD");
    currencyList.Add("CNY");
    Int64 begin = DateUtil.ConvertTimestamp("2023-08-10", CustomTimeZone.HK_ZONE);
    Int64 end = DateUtil.ConvertTimestamp("2023-08-14", CustomTimeZone.HK_ZONE);
    TigerRequest<FinancialExchangeRateResponse> request = new TigerRequest<FinancialExchangeRateResponse>()
    {
      ApiMethodName = QuoteApiService.FINANCIAL_EXCHANGE_RATE,
      ModelValue = new FinancialExchangeRateModel()
      {
        CurrencyList = currencyList,
        BeginDate = begin,
        EndDate = end
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FinancialCurrencyResponse?> GetFinancialCurrencyAsync(QuoteClient quoteClient)
  {
    List<string> symbols = new List<string>();
    symbols.Add("AAPL");
    symbols.Add("BABA");
    TigerRequest<FinancialCurrencyResponse> request = new TigerRequest<FinancialCurrencyResponse>()
    {
      ApiMethodName = QuoteApiService.FINANCIAL_CURRENCY,
      ModelValue = new FinancialCurrencyModel()
      {
        Symbols = symbols,
        Market = Market.US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<CorporateDividendResponse?> GetCorporateDividendAsync(QuoteClient quoteClient)
  {
    List<string> symbols = new List<string>();
    symbols.Add("ALB");
    Int64 begin = DateUtil.ConvertTimestamp("2023-06-15", CustomTimeZone.HK_ZONE);
    Int64 end = DateUtil.ConvertTimestamp("2023-06-15", CustomTimeZone.HK_ZONE);
    TigerRequest<CorporateDividendResponse> request = new TigerRequest<CorporateDividendResponse>()
    {
      ApiMethodName = QuoteApiService.CORPORATE_ACTION,
      ModelValue = new CorporateActionModel()
      {
        ActionType = CorporateActionType.DIVIDEND,
        Symbols = symbols,
        Market = Market.US,
        BeginDate = begin,
        EndDate = end
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<KlineQuotaResponse?> GetKlineQuotaAsync(QuoteClient quoteClient)
  {
    TigerRequest<KlineQuotaResponse> request = new TigerRequest<KlineQuotaResponse>()
    {
      ApiMethodName = QuoteApiService.KLINE_QUOTA,
      ModelValue = new KlineQuotaModel() { WithDetails = true }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<DepositWithdrawResponse?> QueryDepositWithdrawFundsAsync(TradeClient tradeClient)
  {
    TigerRequest<DepositWithdrawResponse> request = new TigerRequest<DepositWithdrawResponse>()
    {
      ApiMethodName = TradeApiService.TRANSFER_FUND,
      ModelValue = new DepositWithdrawModel()
      {
        Account = tradeClient.GetDefaultAccount,// "20200821144442583",
        SegType = SegmentType.SEC,
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<EstimateTradableQuantityResponse?> GetMaxTradableQuantityAsync(TradeClient tradeClient)
  {
    TigerRequest<EstimateTradableQuantityResponse> request = new TigerRequest<EstimateTradableQuantityResponse>()
    {
      ApiMethodName = TradeApiService.ESTIMATE_TRADABLE_QUANTITY,
      ModelValue = new EstimateTradableQuantityModel()
      {
        Account = tradeClient.GetDefaultAccount,// "20200821144442583",
        SecType = SecType.STK,
        Symbol = "AAPL",
        Action = ActionType.BUY,
        OrderType = OrderType.LMT,
        LimitPrice = 150,
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<SegmentFundsResponse?> QueryTransferFundsAsync(TradeClient tradeClient)
  {
    TigerRequest<SegmentFundsResponse> request = new TigerRequest<SegmentFundsResponse>()
    {
      ApiMethodName = TradeApiService.SEGMENT_FUND_HISTORY,
      ModelValue = new SegmentFundModel()
      {
        Account = tradeClient.GetDefaultAccount,// "20200821144442583",
        Limit = 50,
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<SegmentFundResponse?> CancelSegmentFundAsync(TradeClient tradeClient, Int64 id)
  {
    TigerRequest<SegmentFundResponse> request = new TigerRequest<SegmentFundResponse>()
    {
      ApiMethodName = TradeApiService.CANCEL_SEGMENT_FUND,
      ModelValue = new SegmentFundModel()
      {
        Account = tradeClient.GetDefaultAccount,// "20200821144442583",
        Id = id,
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<SegmentFundResponse?> TransferSegmentFundAsync(TradeClient tradeClient)
  {
    TigerRequest<SegmentFundResponse> request = new TigerRequest<SegmentFundResponse>()
    {
      ApiMethodName = TradeApiService.TRANSFER_SEGMENT_FUND,
      ModelValue = new SegmentFundModel()
      {
        Account = tradeClient.GetDefaultAccount,// "20200821144442583",
        FromSegment = SegmentType.SEC,
        ToSegment = SegmentType.FUT,
        Currency = Currency.HKD,
        Amount = 1000D,
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<SegmentFundAvailableResponse?> QueryAvailableSegmentFundAsync(TradeClient tradeClient)
  {
    TigerRequest<SegmentFundAvailableResponse> request = new TigerRequest<SegmentFundAvailableResponse>()
    {
      ApiMethodName = TradeApiService.SEGMENT_FUND_AVAILABLE,
      ModelValue = new SegmentFundModel()
      {
        Account = tradeClient.GetDefaultAccount,// "20200821144442583",
        FromSegment = SegmentType.SEC,
        Currency = Currency.HKD,
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<OrderTransactionsResponse?> QueryOrderTransactionsAsync(TradeClient tradeClient)
  {
    TigerRequest<OrderTransactionsResponse> request = new TigerRequest<OrderTransactionsResponse>()
    {
      ApiMethodName = TradeApiService.ORDER_TRANSACTIONS,
      ModelValue = new OrderTransactionsModel()
      {
        Account = tradeClient.GetDefaultAccount,// "20200821144442583",
        //OrderId = 29358459894498304,
        Symbol = "01810",
        StartDate = DateUtil.ConvertTimestamp("2022-11-01 00:00:00", tradeClient.GetConfigTimeZone),
        EndDate = DateUtil.CurrentTimeMillis(),
        Limit = 20
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<OrderBatchResponse?> QueryInactiveOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<OrderBatchResponse> request = new TigerRequest<OrderBatchResponse>()
    {
      ApiMethodName = TradeApiService.INACTIVE_ORDERS,
      ModelValue = new QueryOrderModel()
      {
        Account = tradeClient.GetDefaultAccount, // "20200821144442583",
        StartDate = DateUtil.ConvertTimestamp("2022-12-20 00:00:00", tradeClient.GetConfigTimeZone),
        EndDate = DateUtil.CurrentTimeMillis(),
        SecType = SecType.STK,
        SortBy = OrderSortBy.LATEST_CREATED,
        Limit = 3
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<OrderBatchResponse?> QueryActiveOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<OrderBatchResponse> request = new TigerRequest<OrderBatchResponse>()
    {
      ApiMethodName = TradeApiService.ACTIVE_ORDERS,
      ModelValue = new QueryOrderModel()
      {
        Account = "20200821144442583",// tradeClient.GetDefaultAccount,
        StartDate = DateUtil.ConvertTimestamp("2022-12-20 00:00:00", tradeClient.GetConfigTimeZone),
        EndDate = DateUtil.CurrentTimeMillis(),
        SortBy = OrderSortBy.LATEST_CREATED,
        Limit = 3
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<OrderBatchResponse?> QueryFilledOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<OrderBatchResponse> request = new TigerRequest<OrderBatchResponse>()
    {
      ApiMethodName = TradeApiService.FILLED_ORDERS,
      ModelValue = new QueryOrderModel()
      {
        Account = "20200821144442583",// tradeClient.GetDefaultAccount,
        StartDate = DateUtil.ConvertTimestamp("2022-12-20 00:00:00", tradeClient.GetConfigTimeZone),
        EndDate = DateUtil.CurrentTimeMillis(),
        SortBy = OrderSortBy.LATEST_CREATED,
        Limit = 3
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static void QueryOrderUsePageTokenAsync(TradeClient tradeClient)
  {
    QueryOrderModel queryOrder = new QueryOrderModel()
    {
      Account = "20200821144442583",// tradeClient.GetDefaultAccount,
      StartDate = DateUtil.ConvertTimestamp("2022-12-25 00:00:00", tradeClient.GetConfigTimeZone),
      EndDate = DateUtil.CurrentTimeMillis(),
      SortBy = OrderSortBy.LATEST_CREATED,
      Limit = 7
    };

    OrderBatchResponse? response = null;
    int num = 0;
    int totalCount = 0;
    do
    {
      // set next page token
      if (response != null && response.IsSuccess()
        && !string.IsNullOrWhiteSpace(response.Data.NextPageToken))
      {
        queryOrder.PageToken = response.Data.NextPageToken;
      }

      TigerRequest<OrderBatchResponse> request = new TigerRequest<OrderBatchResponse>()
      {
        ApiMethodName = TradeApiService.ORDERS,
        ModelValue = queryOrder
      };

      num++;
      response = tradeClient.Execute(request);
      if (response != null && response.IsSuccess())
      {
        if (response.Data.Items.Count == 0)
        {
          break;
        }
        else
        {
          totalCount += response.Data.Items.Count;
          foreach (TradeOrder order in response.Data.Items)
          {
            ApiLogger.Info($"----{num} query return id:{order.Id}, opentime:{order.OpenTime}");
          }
        }
      }
    } while (response != null && response.IsSuccess()
        && !string.IsNullOrWhiteSpace(response.Data.NextPageToken));
    ApiLogger.Info($"total order count:{totalCount}");
  }

  static async Task<OrderBatchResponse?> QueryOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<OrderBatchResponse> request = new TigerRequest<OrderBatchResponse>()
    {
      ApiMethodName = TradeApiService.ORDERS,
      ModelValue = new QueryOrderModel()
      {
        Account = "20200821144442583",// tradeClient.GetDefaultAccount,
        StartDate = DateUtil.ConvertTimestamp("2022-12-20 00:00:00", tradeClient.GetConfigTimeZone),
        EndDate = DateUtil.CurrentTimeMillis(),
        SortBy = OrderSortBy.LATEST_CREATED,
        Limit = 5
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<SingleOrderResponse?> QueryOrderByIdAsync(TradeClient tradeClient)
  {
    TigerRequest<SingleOrderResponse> request = new TigerRequest<SingleOrderResponse>()
    {
      ApiMethodName = TradeApiService.ORDERS,
      ModelValue = new QueryOrderModel()
      {
        Account = "13810712",// tradeClient.GetDefaultAccount,
        Id = 35070272121733120,
        IsShowCharges = true
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<TigerDictResponse?> CancelOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = TradeApiService.CANCEL_ORDER,
      ModelValue = new CancelOrderModel()
      {
        Account = "20200821144442583",// tradeClient.GetDefaultAccount,
        Id = 29360305075913728
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<TigerDictResponse?> ModifyOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = TradeApiService.MODIFY_ORDER,
      ModelValue = new ModifyOrderModel()
      {// 29360305075913728 120 1 limit
        Account = "20200821144442583",// tradeClient.GetDefaultAccount,
        Id = 29360305075913728,
        LimitPrice = 121.0,
        TotalQuantity = 2
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceOCABracketsOrderAsync(TradeClient tradeClient)
  {
    // place OCA Brackets order
    ContractItem contract = ContractItem.BuildStockContract("BILI", Currency.USD.ToString());
    PlaceOrderModel placeOrder = PlaceOrderModel.BuildOCABracketsOrder(
      "13810712", contract, ActionType.SELL, 1,
      17.0, TimeInForce.DAY, true,
      12.0, TimeInForce.DAY, false);
    placeOrder.Lang = Language.en_US;
    placeOrder.UserMark = "test-oca";

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = placeOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceMultiLegOrderAsync(TradeClient tradeClient)
  {
    // place option multi-leg order
    ContractLeg leg1 = new ContractLeg()
    {
      SecType = SecType.OPT.ToString(),
      Symbol = "AAPL",
      Strike = "175.0",
      Expiry = "20231013",
      Right = Right.CALL.ToString(),
      Action = ActionType.BUY.ToString(),
      Ratio = 1
    };
    ContractLeg leg2 = new ContractLeg()
    {
      SecType = SecType.OPT.ToString(),
      Symbol = "AAPL",
      Strike = "180.0",
      Expiry = "20231013",
      Right = Right.CALL.ToString(),
      Action = ActionType.SELL.ToString(),
      Ratio = 1
    };
    List<ContractLeg> legs = new List<ContractLeg>() { leg1, leg2 };
    PlaceOrderModel placeOrder = PlaceOrderModel.BuildMultiLegOrder(
      "13810712", legs, ComboType.VERTICAL, ActionType.BUY, 1,
      OrderType.LMT, 0.6, null, null);

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = placeOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceVWAPOrderAsync(TradeClient tradeClient)
  {
    // place VWAP order
    PlaceOrderModel placeOrder = PlaceOrderModel.BuildVWAPOrder(
      "13810712", "AAPL", ActionType.BUY, 1000,
      DateUtil.ConvertTimestamp("2023-06-20 10:30:00", CustomTimeZone.NY_ZONE),
      DateUtil.ConvertTimestamp("2023-06-20 12:30:00", CustomTimeZone.NY_ZONE),
      0.5, 160.0);

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = placeOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceTWAPOrderAsync(TradeClient tradeClient)
  {
    // place TWAP order
    PlaceOrderModel placeOrder = PlaceOrderModel.BuildTWAPOrder(
      "13810712", "AAPL", ActionType.BUY, 1000,
      DateUtil.ConvertTimestamp("2023-06-20 10:30:00", CustomTimeZone.NY_ZONE),
      DateUtil.ConvertTimestamp("2023-06-20 12:30:00", CustomTimeZone.NY_ZONE),
      160.0);

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = placeOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceBracketsOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("01810", Currency.HKD.ToString());
    PlaceOrderModel placeOrder = PlaceOrderModel.BuildLimitOrder(
        "U10010705",
        contract,
        ActionType.BUY,
        200, 11.0
      );
    // addBracketsOrder
    placeOrder.AddBracketsOrder(13.0, TimeInForce.DAY, true, 10.0, TimeInForce.DAY);

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = placeOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceStopLossTrailOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("01810", Currency.HKD.ToString());
    PlaceOrderModel placeOrder = PlaceOrderModel.BuildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        200, 11.0
      );
    // addStopLossTrailOrder ('stopLossTrailingPercent' = 10%)
    placeOrder.AddStopLossTrailOrder(10.0, 0, TimeInForce.DAY);
    // set other parameter
    placeOrder.UserMark = "test-attach-stoplosstrail";

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = placeOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceStopLossLimitOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildOptionContract("AAPL", "20230127", 135.0, "CALL");
    PlaceOrderModel placeOrder = PlaceOrderModel.BuildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        1, 1.30
      );
    // addStopLossLimitOrder
    placeOrder.AddStopLossLimitOrder(1.0, 0.9, TimeInForce.DAY);
    // set other parameter
    placeOrder.UserMark = "test-attach-stoplosslimit";

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = placeOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceStopLossOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        200, 11.0
      ).AddStopLossOrder(10.0, TimeInForce.DAY) // addStopLossOrder(not support options)
    };
    // set other parameter
    ((PlaceOrderModel)request.ModelValue).UserMark = "test001";
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceProfitTakerOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        200, 11.0
      ).AddProfitTakerOrder(13.0, TimeInForce.DAY, true) // addProfitTakerOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceTrailOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildTrailOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.SELL,
        200, 10.0, 0 // use 'trailing_percent' = 10%
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceStopLimitOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildStopLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.SELL,
        200, 10.0, 10.3
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceStopOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildStopOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.SELL,
        200, 10.0
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceAuctionOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("00700", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildAuctionOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        100,
        350.0,
        // pre-mardet auciton order: AM or AL + OPG(If there is no transaction, continue to participate in intraday trading); after-hours auction order: AM or AL + DAY
        OrderType.AL,  // AL(auction limit order) or AM(auction market order)
        TimeInForce.OPG // participate in the pre-market auction
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceOddLotOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("AAPL", Currency.USD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        1234, 110.0, 0.002,
        3 // totalQuantityScale is 3, actual quantity is 1.234
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceLimitOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("AAPL", Currency.USD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        1, 120.0, 0.002
      )
    };
    // set other parameter
    // ((PlaceOrderModel)request.ModelValue).UserMark = "test-lmt";
    // ((PlaceOrderModel)request.ModelValue).TimeInForce = TimeInForce.GTD;
    // ((PlaceOrderModel)request.ModelValue).ExpireTime = DateUtil.ConvertTimestamp(
    //  "2023-01-20 23:59:59", SymbolUtil.getZoneIdBySymbol("AAPL", tradeClient.GetConfigTimeZone));
    ((PlaceOrderModel)request.ModelValue).TradingSessionType = TradingSessionType.FULL;
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceFundOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildFundContract("IE00B464Q616.USD", Currency.USD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildAmountOrder(
        "13810712", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        180.0
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceMarketOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.BuildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.BuildMarketOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        200
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<ForexTradeOrderResponse?> PlaceForexOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<ForexTradeOrderResponse> request = new TigerRequest<ForexTradeOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_FOREX_ORDER,
      ModelValue = new ForexTradeOrderModel()
      {
        Account = "20200821144442583",
        SegType = SegmentType.SEC,
        SourceCurrency = Currency.HKD,
        SourceAmount = 10000.0,
        TargetCurrency = Currency.USD,
        TimeInForce = TimeInForce.DAY,
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = new PlaceOrderModel()
      {
        Account = "20200821144442583",
        SecType = SecType.STK,
        Symbol = "AAPL",
        OrderType = OrderType.LMT,
        Action = ActionType.BUY,
        LimitPrice = 124.0,
        TotalQuantity = 1
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PrimeAnalyticsAssetResponse?> GetAssetsAnalyticsAsync(TradeClient tradeClient)
  {
    TigerRequest<PrimeAnalyticsAssetResponse> request = new TigerRequest<PrimeAnalyticsAssetResponse>()
    {
      ApiMethodName = TradeApiService.ANALYTICS_ASSET,
      ModelValue = new PrimeAnalyticsAssetModel()
      {
        Account = "572386",
        StartDate = "2023-02-26"
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PrimeAssetResponse?> GetPrimeAssetsAsync(TradeClient tradeClient)
  {
    TigerRequest<PrimeAssetResponse> request = new TigerRequest<PrimeAssetResponse>()
    {
      ApiMethodName = TradeApiService.PRIME_ASSETS,
      ModelValue = new PrimeAssetsModel()
      {
        Account = "572386",
        BaseCurrency = Currency.USD.ToString(),
        Consolidated = true
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<TigerDictResponse?> GetGlobalAssetsAsync(TradeClient tradeClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = TradeApiService.ASSETS,
      ModelValue = new GlobalAssetsModel()
      {
        Account = "U10010705",
        Segment = true,
        MarketValue = true
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PositionsResponse?> GetPositionsAsync(TradeClient tradeClient)
  {
    TigerRequest<PositionsResponse> request = new TigerRequest<PositionsResponse>()
    {
      ApiMethodName = TradeApiService.POSITIONS,
      ModelValue = new PositionsModel()
      {
        Account = "13810712",
        SecType = SecType.STK,
        Market = Market.US
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<AccountsResponse?> GetAccountsAsync(TradeClient tradeClient)
  {
    TigerRequest<AccountsResponse> request = new TigerRequest<AccountsResponse>()
    {
      ApiMethodName = TradeApiService.ACCOUNTS,
      ModelValue = new ApiModel() { }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<ContractsResponse?> GetContractsAsync(TradeClient tradeClient)
  {
    TigerRequest<ContractsResponse> request = new TigerRequest<ContractsResponse>()
    {
      ApiMethodName = TradeApiService.CONTRACTS,
      ModelValue = new ContractsModel()
      {
        SecType = SecType.STK.ToString(),
        Symbols = new List<string> { "AAPL", "TSLA" },
        Account = "13810712"
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<ContractResponse?> GetContractAsync(TradeClient tradeClient)
  {
    // get stock contract
    TigerRequest<ContractResponse> request = new TigerRequest<ContractResponse>()
    {
      ApiMethodName = TradeApiService.CONTRACT,
      ModelValue = new ContractModel()
      {
        Symbol = "AAPL"
      }
    };
    ContractResponse? response = await tradeClient.ExecuteAsync(request);
    ApiLogger.Info("response:" + JsonConvert.SerializeObject(response, TigerClient.JsonSet));

    // get options contract
    request = new TigerRequest<ContractResponse>()
    {
      ApiMethodName = TradeApiService.CONTRACT,
      ModelValue = new ContractModel()
      {
        SecType = SecType.OPT.ToString(),
        Symbol = "AAPL",
        Strike = 150.0,
        Expiry = "20230616",
        Right = Right.CALL.ToString(),
        Currency = Currency.USD.ToString()
      }
    };
    response = await tradeClient.ExecuteAsync(request);
    ApiLogger.Info("options response:" + JsonConvert.SerializeObject(response, TigerClient.JsonSet));

    // get warrant contract
    request = new TigerRequest<ContractResponse>()
    {
      ApiMethodName = TradeApiService.CONTRACT,
      ModelValue = new ContractModel()
      {
        SecType = SecType.WAR.ToString(),
        Symbol = "29263",
        Strike = 351.567,
        Expiry = "20230330",
        Right = Right.CALL.ToString()
      }
    };
    response = await tradeClient.ExecuteAsync(request);
    ApiLogger.Info("warrants response:" + JsonConvert.SerializeObject(response, TigerClient.JsonSet));

    // get futures contract
    request = new TigerRequest<ContractResponse>()
    {
      ApiMethodName = TradeApiService.CONTRACT,
      ModelValue = new ContractModel()
      {
        SecType = SecType.FUT.ToString(),
        Symbol = "JPY2306"
      }
    };
    response = await tradeClient.ExecuteAsync(request);
    ApiLogger.Info("futures response:" + JsonConvert.SerializeObject(response, TigerClient.JsonSet));

    return response;
  }

  static async Task<FundHistoryQuoteResponse?> GetFundHistoryQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<FundHistoryQuoteResponse> request = new TigerRequest<FundHistoryQuoteResponse>()
    {
      ApiMethodName = QuoteApiService.FUND_HISTORY_QUOTE,
      ModelValue = new FundQuoteHistoryModel()
      {
        Symbols = new List<string>()
        {
          "HK0000910932.HKD",
          "IE00B464Q616.USD"
        },
        BeginTime = DateUtil.ConvertTimestamp("2023-07-01 09:00:00", CustomTimeZone.HK_ZONE),
        EndTime = DateUtil.ConvertTimestamp("2023-07-26 20:00:00", CustomTimeZone.HK_ZONE),
        Limit = 10,
        Lang = Language.en_US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FundQuoteResponse?> GetFundQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<FundQuoteResponse> request = new TigerRequest<FundQuoteResponse>()
    {
      ApiMethodName = QuoteApiService.FUND_QUOTE,
      ModelValue = new FundSymbolModel()
      {
        Symbols = new List<string>()
        {
          "HK0000910932.HKD",
          "IE00B464Q616.USD"
        },
        Lang = Language.en_US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FundContractsResponse?> GetFundContractsAsync(QuoteClient quoteClient)
  {
    TigerRequest<FundContractsResponse> request = new TigerRequest<FundContractsResponse>()
    {
      ApiMethodName = QuoteApiService.FUND_CONTRACTS,
      ModelValue = new FundSymbolModel()
      {
        Symbols = new List<string>()
        {
          "HK0000910932.HKD",
          "IE00B464Q616.USD"
        },
        Lang = Language.en_US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListStringResponse?> GetAllFundSymbolsAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListStringResponse> request = new TigerRequest<TigerListStringResponse>()
    {
      ApiMethodName = QuoteApiService.FUND_ALL_SYMBOLS,
      ModelValue = new FundSymbolModel()
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<WarrantQuoteResponse?> GetWarrantQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<WarrantQuoteResponse> request = new TigerRequest<WarrantQuoteResponse>()
    {
      ApiMethodName = QuoteApiService.WARRANT_REAL_TIME_QUOTE,
      ModelValue = new WarrantQuoteModel()
      {
        Symbols = new List<string>()
        {
          "68723",
          "68722"
        },
        Lang = Language.en_US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<WarrantFilterResponse?> FilterWarrantAsync(QuoteClient quoteClient)
  {
    TigerRequest<WarrantFilterResponse> request = new TigerRequest<WarrantFilterResponse>()
    {
      ApiMethodName = QuoteApiService.WARRANT_FILTER,
      ModelValue = new WarrantFilterModel()
      {
        Symbol = "00700",
        Lang = Language.en_US,
        SortFieldName = "expireDate",
        SortDir = SortDir.SortDir_Descend,
        WarrantType = new HashSet<Int32>() { (int)WarrantType.Bull },
        IssuerName = "GS", //"高盛",
        Strike = new Range<double>(300, 320.0),
        Page = 0,
        PageSize = 10
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<MarketScannerTagsResponse?> GetMultiFieldTags(QuoteClient quoteClient)
  {
    TigerRequest<MarketScannerTagsResponse> request = new TigerRequest<MarketScannerTagsResponse>()
    {
      ApiMethodName = QuoteApiService.MARKET_SCANNER_TAGS,
      ModelValue = new MarketScannerTagsModel()
      {
        Market = Market.HK,
        MultiTagFieldList = new List<string>()
        {
          // only support MultiTagField_Industry and MultiTagField_Concept
          nameof(MultiTagField.MultiTagField_Industry)
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<MarketScannerResponse?> FilterSymbolsAsync(QuoteClient quoteClient)
  {
    TigerRequest<MarketScannerResponse> request = new TigerRequest<MarketScannerResponse>()
    {
      ApiMethodName = QuoteApiService.MARKET_SCANNER,
      ModelValue = new MarketScannerModel()
      {
        Market = Market.HK,
        BaseFilterList = new List<BaseFilter>()
        {
          new BaseFilter()
          {
            FieldName = StockField.StockField_MarketValue,
            FilterMin = 1000000000D,
            FilterMax = 2000000000D
          }
        },
        Page = 1,
        PageSize = 20
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteContractResponse?> GetQuoteContractAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteContractResponse> request = new TigerRequest<QuoteContractResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_CONTRACT,
      ModelValue = new QuoteContractsModel()
      {
        Symbol = "00700",
        SecType = SecType.WAR,
        Expiry = "20230417"
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureHistoryMainContractResponse?> GetFutureHistoryMainContractAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureHistoryMainContractResponse> request = new TigerRequest<FutureHistoryMainContractResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_HISTORY_MAIN_CONTRACT,
      ModelValue = new FutureHistoryMainContractModel()
      {
        ContractCodes = new List<string> { "ESmain" },
        BeginTime = DateUtil.ConvertTimestamp("2023-08-08 00:00:00", CustomTimeZone.NY_ZONE),
        EndTime = DateUtil.ConvertTimestamp("2023-10-05 23:59:00", CustomTimeZone.NY_ZONE),
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureTickResponse?> GetFutureTickAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureTickResponse> request = new TigerRequest<FutureTickResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_TICK,
      ModelValue = new FutureTickModel()
      {
        ContractCode = "ES2306",
        BeginIndex = 10,
        EndIndex = 100,
        Limit = 20
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureRealTimeQuoteResponse?> GetFutureRealTimeQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureRealTimeQuoteResponse> request = new TigerRequest<FutureRealTimeQuoteResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_REAL_TIME_QUOTE,
      ModelValue = new FutureContractCodesModel()
      {
        ContractCodes = new List<string> { "CL2306" }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureKlineResponse?> GetFutureKLineAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureKlineResponse> request = new TigerRequest<FutureKlineResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_KLINE,
      ModelValue = new FutureKlineModel()
      {
        ContractCodes = new List<string> { "ES2306" },
        Period = FutureKType.min15.Value,
        BeginTime = DateUtil.ConvertTimestamp("2023-03-06 09:00:00", CustomTimeZone.NY_ZONE),
        EndTime = DateUtil.ConvertTimestamp("2023-03-06 20:00:00", CustomTimeZone.NY_ZONE),
        Limit = 20
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureTradingDateResponse?> GetFutureTradingDateAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureTradingDateResponse> request = new TigerRequest<FutureTradingDateResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_TRADING_DATE,
      ModelValue = new FutureTradingDateModel()
      {
        ContractCode = "ES2306",
        TradingDate = DateUtil.CurrentTimeMillis()
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractResponse?> GetFutureCurrentContractAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractResponse> request = new TigerRequest<FutureContractResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CURRENT_CONTRACT,
      ModelValue = new FutureContractByTypeModel() { FutureType = "CL" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractResponse?> GetFutureContinuousContractsAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractResponse> request = new TigerRequest<FutureContractResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CONTINUOUS_CONTRACTS,
      ModelValue = new FutureContractByTypeModel() { FutureType = "ES" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractsResponse?> GetFutureContractsAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractsResponse> request = new TigerRequest<FutureContractsResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CONTRACTS,
      ModelValue = new FutureContractByTypeModel() { FutureType = "CL" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractResponse?> GetFutureContractByContractCodeAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractResponse> request = new TigerRequest<FutureContractResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CONTRACT_BY_CONTRACT_CODE,
      ModelValue = new FutureContractByConCodeModel() { ContractCode = "ES2306" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureContractsResponse?> GetFutureContractByExchangeCodeAsync(QuoteClient quoteClient)
  {
    TigerRequest<FutureContractsResponse> request = new TigerRequest<FutureContractsResponse>()
    {
      ApiMethodName = QuoteApiService.FUTURE_CONTRACT_BY_EXCHANGE_CODE,
      ModelValue = new FutureContractByExchCodeModel() { ExchangeCode = "CME" }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<FutureExchangeResponse?> GetFutureExchangeAsync(QuoteClient quoteClient)
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

  static async Task<OptionTradeTickResponse?> GetOptionTradeTickAsync(QuoteClient quoteClient)
  {
    TigerRequest<OptionTradeTickResponse> request = new TigerRequest<OptionTradeTickResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_TRADE_TICK,
      ModelValue = new BatchApiModel<OptionCommonModel>()
      {
        Items = new List<OptionCommonModel>()
        {
          new OptionCommonModel() { Symbol = "AAPL", Right = "PUT", Strike = "100.0",
            Expiry = DateUtil.ConvertTimestamp("2023-03-17", CustomTimeZone.NY_ZONE)}
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionKlineResponse?> GetOptionKLineV2Async(QuoteClient quoteClient)
  {
    TigerRequest<OptionKlineResponse> request = new TigerRequest<OptionKlineResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_KLINE,
      ModelValue = new OptionKlineV2Model()
      {
        Market = Market.US,
        OptionQuery = new List<OptionKlineModel>()
        {
          new OptionKlineModel() {
            Symbol = "AAPL", Right = "CALL", Strike = "170.0",
            Expiry = DateUtil.ConvertTimestamp("2024-06-28", CustomTimeZone.NY_ZONE),
            BeginTime = DateUtil.ConvertTimestamp("2024-06-24", CustomTimeZone.NY_ZONE),
            EndTime = DateUtil.ConvertTimestamp("2024-06-26", CustomTimeZone.NY_ZONE),
            Period = OptionKType.min60.Value,
            SortDir = SortDir.SortDir_Descend,
            Limit = 10
          }
        }
        //Market = Market.HK,
        //OptionQuery = new List<OptionKlineModel>()
        //{
        //  new OptionKlineModel() {
        //    Symbol = "TCH.HK", Right = "CALL", Strike = "370.0",
        //    Expiry = DateUtil.ConvertTimestamp("2024-06-27", CustomTimeZone.HK_ZONE),
        //    BeginTime = DateUtil.ConvertTimestamp("2024-05-22", CustomTimeZone.HK_ZONE),
        //    EndTime = DateUtil.ConvertTimestamp("2024-05-24", CustomTimeZone.HK_ZONE),
        //    Period = OptionKType.min60.Value,
        //    Limit = 300
        //  }
        //}
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionKlineResponse?> GetOptionKLineAsync(QuoteClient quoteClient)
  {
    TigerRequest<OptionKlineResponse> request = new TigerRequest<OptionKlineResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_KLINE,
      ModelValue = new BatchApiModel<OptionKlineModel>()
      {
        Items = new List<OptionKlineModel>()
        {
          new OptionKlineModel() {
            Symbol = "AAPL", Right = "CALL", Strike = "170.0",
            Expiry = DateUtil.ConvertTimestamp("2024-06-07", CustomTimeZone.NY_ZONE),
            BeginTime = DateUtil.ConvertTimestamp("2024-05-22", CustomTimeZone.NY_ZONE),
            EndTime = DateUtil.ConvertTimestamp("2024-05-24", CustomTimeZone.NY_ZONE),
            Period = OptionKType.min60.Value,
            Limit = 300
          }
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionBriefResponse?> GetOptionBriefV2Async(QuoteClient quoteClient)
  {
    TigerRequest<OptionBriefResponse> request = new TigerRequest<OptionBriefResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_BRIEF,
      ModelValue = new OptionBasicModel()
      {
        Market = Market.US,
        OptionBasic = new List<OptionCommonModel>()
          {
            new OptionCommonModel() {
              Symbol = "AAPL",
              Right = "CALL",
              Strike = "160.0",
              Expiry = DateUtil.ConvertTimestamp("2024-06-28", CustomTimeZone.NY_ZONE)
            }
          }
        //Market = Market.HK,
        //OptionBasic = new List<OptionCommonModel>()
        //{
        //  new OptionCommonModel() {
        //    Symbol = "TCH.HK",
        //    Right = "CALL",
        //    Strike = "370.0",
        //    Expiry = DateUtil.ConvertTimestamp("2024-06-27", CustomTimeZone.HK_ZONE)
        //  }
        //}
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionBriefResponse?> GetOptionBriefAsync(QuoteClient quoteClient)
  {
    TigerRequest<OptionBriefResponse> request = new TigerRequest<OptionBriefResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_BRIEF,
      ModelValue = new BatchApiModel<OptionCommonModel>()
      {
        Items = new List<OptionCommonModel>()
        {
          new OptionCommonModel() { Symbol = "AAPL", Right = "PUT", Strike = "150.0",
            Expiry = DateUtil.ConvertTimestamp("2024-06-07", CustomTimeZone.NY_ZONE)}
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionSymbolResponse?> GetHKOptionSymbolsAsync(QuoteClient quoteClient)
  {
    TigerRequest<OptionSymbolResponse> request = new TigerRequest<OptionSymbolResponse>()
    {
      ApiMethodName = QuoteApiService.ALL_HK_OPTION_SYMBOLS,
      ModelValue = new OptionModel()
      {
        Market = Market.HK,
        Lang = Language.en_US
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionDepthResponse?> GetOptionDepthAsync(QuoteClient quoteClient)
  {
    TigerRequest<OptionDepthResponse> request = new TigerRequest<OptionDepthResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_DEPTH,
      ModelValue = new OptionBasicModel()
      {
        Market = Market.US,
        OptionBasic = new List<OptionCommonModel>()
        {
          new OptionCommonModel() {
            Symbol = "AAPL",
            Right = "PUT",
            Strike = "210.0",
            Expiry = DateUtil.ConvertTimestamp("2024-06-28", CustomTimeZone.NY_ZONE)
          }
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionChainResponse?> GetOptionChainAsync(QuoteClient quoteClient)
  {
    TigerRequest<OptionChainResponse> request = new TigerRequest<OptionChainResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_CHAIN,
      ModelValue = new OptionChainV3Model()
      {
        Market = Market.US,
        OptionBasic = new List<OptionChainModel>()
        {
          new OptionChainModel() {
            Symbol = "AAPL",
            Expiry = DateUtil.ConvertTimestamp("2024-07-26", CustomTimeZone.NY_ZONE)
          }
        },
        OptionFilter = new OptionChainFilterModel()
        {
          InTheMoney = true,
          ImpliedVolatility = new Range<Double>(0.1537, 0.8282),
          OpenInterest = new Range<int>(10, 50000),
          Greeks = new Greeks()
          {
            Delta = new Range<Double>(-0.8, 0.6),
            Gamma = new Range<double>(0.024, 0.3),
            Vega = new Range<double>(0.019, 0.343),
            Theta = new Range<double>(-0.1, 0.1),
            Rho = new Range<double>(-0.096, 0.101)
          }
        }

        //Market = Market.HK,
        //OptionBasic = new List<OptionChainModel>()
        //{
        //  new OptionChainModel() {
        //    Symbol = "PAI.HK",
        //    Expiry = DateUtil.ConvertTimestamp("2024-06-27", CustomTimeZone.NY_ZONE)
        //  }
        //},
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<OptionExpirationResponse?> GetOptionExpirationAsync(QuoteClient quoteClient)
  {
    TigerRequest<OptionExpirationResponse> request = new TigerRequest<OptionExpirationResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_EXPIRATION,
      ModelValue = new OptionExpirationModel()
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US
        //Symbols = new List<string> { "PAI.HK" },
        //Market = Market.HK
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteStockBrokerResponse?> GetStockBrokerAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteStockBrokerResponse> request = new TigerRequest<QuoteStockBrokerResponse>()
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

  static async Task<QuoteCapitalDistributionResponse?> GetStockCaptialDistributionAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteCapitalDistributionResponse> request = new TigerRequest<QuoteCapitalDistributionResponse>()
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

  static async Task<QuoteCapitalFlowResponse?> GetStockCaptialFlowAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteCapitalFlowResponse> request = new TigerRequest<QuoteCapitalFlowResponse>()
    {
      ApiMethodName = QuoteApiService.CAPITAL_FLOW,
      ModelValue = new QuoteCapitalFlowModel()
      {
        Symbol = "AAPL",
        Market = Market.US,
        Period = CapitalPeriod.day.Value,
        BeginTime = DateUtil.ConvertTimestamp("2023-02-25", CustomTimeZone.NY_ZONE),
        EndTime = DateUtil.ConvertTimestamp("2023-03-06", CustomTimeZone.NY_ZONE)
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteStockTradeResponse?> GetStockTradeInfoAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteStockTradeResponse> request = new TigerRequest<QuoteStockTradeResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_STOCK_TRADE,
      ModelValue = new QuoteStockTradeModel()
      {
        Symbols = new List<string> { "AAPL", "TSLA" }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteTradeTickResponse?> GetTradeTickAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteTradeTickResponse> request = new TigerRequest<QuoteTradeTickResponse>()
    {
      ApiMethodName = QuoteApiService.TRADE_TICK,
      ModelValue = new QuoteTradeTickModel()
      {
        Symbols = new List<string> { "00700" },
        // TradeSession = TradeSession.Regular.ToString(), //only for US market stock
        BeginIndex = 0,
        EndIndex = 10
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteDepthResponse?> GetDepthQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteDepthResponse> request = new TigerRequest<QuoteDepthResponse>()
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

  static async Task<QuoteKlineResponse?> GetKLineAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteKlineResponse> request = new TigerRequest<QuoteKlineResponse>()
    {
      ApiMethodName = QuoteApiService.KLINE,
      ModelValue = new QuoteKlineModel()
      {
        Symbols = new List<string> { "AAPL" },
        Period = KLineType.min3.Value,
        BeginTime = DateUtil.ConvertTimestamp("2024-04-23", CustomTimeZone.NY_ZONE),
        EndTime = DateUtil.CurrentTimeMillis(),
        // TradeSession = TradeSession.AfterHours.ToString(), //only for US market stock
        Rigth = RightOption.br
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteOvernightResponse?> GetOvernightQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteOvernightResponse> request = new TigerRequest<QuoteOvernightResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_OVERNIGHT,
      ModelValue = new QuoteSymbolModel()
      {
        Symbols = new List<string> { "AAPL" }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteRealTimeQuoteResponse?> GetRealTimeQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteRealTimeQuoteResponse> request = new TigerRequest<QuoteRealTimeQuoteResponse>()
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

  static async Task<QuoteHistoryTimelineResponse?> GetHistoryTimelineAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteHistoryTimelineResponse> request = new TigerRequest<QuoteHistoryTimelineResponse>()
    {
      ApiMethodName = QuoteApiService.HISTORY_TIMELINE,
      ModelValue = new QuoteHistoryTimelineModel()
      {
        Symbols = new List<string> { "AAPL" },
        Date = "2023-03-03",
        Rigth = RightOption.br
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteTimelineResponse?> GetTimelineAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteTimelineResponse> request = new TigerRequest<QuoteTimelineResponse>()
    {
      ApiMethodName = QuoteApiService.TIMELINE,
      ModelValue = new QuoteTimelineModel()
      {
        Symbols = new List<string> { "AAPL" },
        Period = TimeLineType.day,
        TradeSession = TradeSession.Regular.ToString(),
        BeginTime = DateUtil.ConvertTimestamp("2023-03-03 03:00:00", CustomTimeZone.NY_ZONE)
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuoteDelayResponse?> GetDelayQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuoteDelayResponse> request = new TigerRequest<QuoteDelayResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_DELAY,
      ModelValue = new QuoteSymbolModel() { Symbols = new List<string> { "AAPL" } }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<SymbolNameResponse?> GetAllSymbolNamesAsync(QuoteClient quoteClient)
  {
    TigerRequest<SymbolNameResponse> request = new TigerRequest<SymbolNameResponse>()
    {
      ApiMethodName = QuoteApiService.ALL_SYMBOL_NAMES,
      ModelValue = new QuoteMarketModel() { Market = Market.US, Lang = Language.zh_CN, IncludeOTC = false }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListStringResponse?> GetAllSymbolsAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListStringResponse> request = new TigerRequest<TigerListStringResponse>()
    {
      ApiMethodName = QuoteApiService.ALL_SYMBOLS,
      ModelValue = new QuoteMarketModel() { Market = Market.US, IncludeOTC = false }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetTradingCalendarAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.TRADING_CALENDAR,
      ModelValue = new TradeCalendarModel()
      {
        Market = Market.HK,
        BeginDate = "2023-03-01",
        EndDate = "2023-03-15"
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<MarketStateResponse?> GetMarketStateAsync(QuoteClient quoteClient)
  {
    TigerRequest<MarketStateResponse> request = new TigerRequest<MarketStateResponse>()
    {
      ApiMethodName = QuoteApiService.MARKET_STATE,
      ModelValue = new QuoteMarketModel() { Market = Market.HK }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuotePermissionResponse?> GetQuotePermissionAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuotePermissionResponse> request = new TigerRequest<QuotePermissionResponse>()
    {
      ApiMethodName = QuoteApiService.GET_QUOTE_PERMISSION
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<QuotePermissionResponse?> GrabQuotePermissionAsync(QuoteClient quoteClient)
  {
    TigerRequest<QuotePermissionResponse> request = new TigerRequest<QuotePermissionResponse>()
    {
      ApiMethodName = QuoteApiService.GRAB_QUOTE_PERMISSION
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<UserLicenseResponse?> GetUserLicenseAsync(QuoteClient quoteClient)
  {
    TigerRequest<UserLicenseResponse> request = new TigerRequest<UserLicenseResponse>()
    {
      ApiMethodName = QuoteApiService.USER_LICENSE
    };
    return await quoteClient.ExecuteAsync(request);
  }

  public static void TestStockPrice()
  {
    string content = "[{\"begin\":\"0\",\"end\":\"0.25\",\"type\":\"CLOSED\",\"tickSize\":0.001},"
        + "{\"begin\":\"0.25\",\"end\":\"0.5\",\"type\":\"OPEN_CLOSED\",\"tickSize\":0.005},"
        + "{\"begin\":\"0.5\",\"end\":\"10\",\"type\":\"OPEN_CLOSED\",\"tickSize\":0.01},"
        + "{\"begin\":\"10\",\"end\":\"20\",\"type\":\"OPEN_CLOSED\",\"tickSize\":0.02},"
        + "{\"begin\":\"20\",\"end\":\"100\",\"type\":\"OPEN_CLOSED\",\"tickSize\":0.05},"
        + "{\"begin\":\"100\",\"end\":\"200\",\"type\":\"OPEN_CLOSED\",\"tickSize\":0.1},"
        + "{\"begin\":\"200\",\"end\":\"500\",\"type\":\"OPEN_CLOSED\",\"tickSize\":0.2},"
        + "{\"begin\":\"500\",\"end\":\"1000\",\"type\":\"OPEN_CLOSED\",\"tickSize\":0.5},"
        + "{\"begin\":\"1000\",\"end\":\"2000\",\"type\":\"OPEN_CLOSED\",\"tickSize\":1.0},"
        + "{\"begin\":\"2000\",\"end\":\"5000\",\"type\":\"OPEN_CLOSED\",\"tickSize\":2.0},"
        + "{\"begin\":\"5000\",\"end\":\"Infinity\",\"type\":\"OPEN\",\"tickSize\":5.0}]";

    List<TickSizeItem>? tickSizeItemList = JsonConvert.DeserializeObject<List<TickSizeItem>>(content);

    ApiLogger.Info("input: 0.34m output expect: 10.34m, result: " + (10.34m == StockPriceUtil.FixPriceByTickSize(10.34m, tickSizeItemList)));
    ApiLogger.Info("input: 0.35m output expect: 10.34m, result: " + (10.34m == StockPriceUtil.FixPriceByTickSize(10.35m, tickSizeItemList)));
    ApiLogger.Info("input: 0.36m output expect: 10.36m, result: " + (10.36m == StockPriceUtil.FixPriceByTickSize(10.36m, tickSizeItemList)));
    ApiLogger.Info("input: 0.35m output expect: 10.36m, result: " + (10.36m == StockPriceUtil.FixPriceByTickSize(10.35m, tickSizeItemList, true)));

    ApiLogger.Info("input: 0.34m output expect: true, result: " + StockPriceUtil.MatchTickSize(10.34m, tickSizeItemList));
    ApiLogger.Info("input: 0.35m output expect: false, result: " + StockPriceUtil.MatchTickSize(10.35m, tickSizeItemList));
    ApiLogger.Info("input: 0.36m output expect: true, result: " + StockPriceUtil.MatchTickSize(10.36m, tickSizeItemList));

    ApiLogger.Info("input: 0.360m output expect: true, result: " + StockPriceUtil.MatchTickSize(10.360m, tickSizeItemList));
    ApiLogger.Info("input: 0.361m output expect: fasle, result: " + StockPriceUtil.MatchTickSize(10.361m, tickSizeItemList));

  }
}