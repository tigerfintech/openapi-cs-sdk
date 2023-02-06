using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sample;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Push;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
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

    // tiger config
    TigerConfig config = new TigerConfig()
    {
      License = License.TBNZ,// must
      TigerId = "20150899", // must
      DefaultAccount = "572386",// (optional) prime accout:572386, paper account: 20200821144442583
      PrivateKey = TigerConfig.ReadPrivateKey("/Users/tiger/dev/liutp_account/rsa_private_key_pkcs8_prod.pem"),// must
      FailRetryCounts = 2, // (optional) range:[1, 5],  default is 2
      AutoGrabPermission = false,   // (optional) default is true
      Language = Language.en_US,   // (optional) default is en_US
      TimeZone = CustomTimeZone.HK_ZONE  // (optional) default is HK_ZONE
    };
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
    //TigerResponse? response = await GetOptionKLineAsync(quoteClient);
    //TigerResponse? response = await GetOptionTradeTickAsync(quoteClient);

    //TigerResponse? response = await GetFutureExchangeAsync(quoteClient);
    //TigerResponse? response = await GetFutureContractByExchangeCodeAsync(quoteClient);
    //TigerResponse? response = await GetFutureContractByContractCodeAsync(quoteClient);
    // 查询指定品种的全部合约
    //TigerResponse? response = await GetFutureContractsAsync(quoteClient);
    // 查询指定品种的连续合约(main合约)
    //TigerResponse? response = await GetFutureContinuousContractsAsync(quoteClient);
    // 查询指定品种的当前合约
    //TigerResponse? response = await GetFutureCurrentContractAsync(quoteClient);
    // 查询指定期货合约的交易时间
    //TigerResponse? response = await GetFutureTradingDateAsync(quoteClient);
    //TigerResponse? response = await GetFutureKLineAsync(quoteClient);
    //TigerResponse? response = await GetFutureRealTimeQuoteAsync(quoteClient);
    //TigerResponse? response = await GetFutureTickAsync(quoteClient);
    //TigerResponse? response = await GetQuoteContractAsync(quoteClient);

    // 选股器
    //TigerResponse? response = await FilterSymbolsAsync(quoteClient);

    // warrant/cbbc
    //TigerResponse? response = await FilterWarrantAsync(quoteClient);
    TigerResponse? response = await GetWarrantQuoteAsync(quoteClient);

    ApiLogger.Info("response:" + JsonConvert.SerializeObject(response));

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

    //TigerResponse? response = await PlaceMarketOrderAsync(tradeClient);
    // response:{"code":0,"message":"success","timestamp":1672900459461,"data":{"id":29360293078500352,"subIds":[],"orders":[{"symbol":"01810","market":"HK","secType":"STK","currency":"HKD","identifier":"01810","id":29360293078500352,"orderId":1456,"account":"20200821144442583","action":"BUY","orderType":"MKT","totalQuantity":200,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":false,"commission":0.0,"realizedPnl":0.0,"remark":"","liquidation":false,"openTime":1672900459000,"updateTime":1672900459000,"latestTime":1672900459000,"name":"XIAOMI-W","latestPrice":11.62,"attrDesc":"","userMark":"","algoStrategy":"MKT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"y3iIACssSMlurcA3TP+PZQOF0p519WqWPpQG6Y8pYKQTKeePXPv1xZwjq0J97JBxnBr92bL20cZr1J/zQCPvvvtkQNZc3QGRx08dCDfp4AUjoBBzRBuQw+xNSMUsnlY/4G1KbXoOXj5qJ3OycZeFQVxbPeJlSYEt4JJz5LhjBNs="} 

    //TigerResponse? response = await PlaceLimitOrderAsync(tradeClient);
    // result:{"code":0,"message":"success","timestamp":1672900550991,"data":{"id":29360305075913728,"subIds":[],"orders":[{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","identifier":"AAPL","id":29360305075913728,"orderId":1457,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":120.0,"totalQuantity":1,"filledQuantity":0,"avgFillPrice":0.0,"timeInForce":"DAY","outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"You order[BUY 1 AAPL] will not be placed until 2023-01-05 04:00:00, local time of the exchange","liquidation":false,"openTime":1672900550000,"updateTime":1672900550000,"latestTime":1672900551000,"name":"Apple","latestPrice":126.625,"attrDesc":"","userMark":"","algoStrategy":"LMT","status":"Initial","discount":0.0,"canModify":true,"canCancel":true}]},"sign":"vgRX7Z8v2dYNqtzI1RoqD2A7GTOPckQLrN4dOv29l0bcF4GUzNLIRfQd5PPb6o3coV91PfqSPGSlzdRYfUCgMbeZaUPkOtd9v+5KZD6wwyjzT6gviZIYjbPSdboTe64cZ/g8uL3MO/SMLh4SrwLaHbmu9yGf0QgXoL83wjDDgIU="} 
    // response:{"data":{"id":29360305075913728,"subIds":[],"orders":[{"symbol":"AAPL","market":"US","secType":"STK","currency":"USD","expiry":null,"strike":null,"right":null,"multiplier":0.0,"identifier":"AAPL","id":29360305075913728,"orderId":1457,"parentId":0,"account":"20200821144442583","action":"BUY","orderType":"LMT","limitPrice":120.0,"auxPrice":0.0,"trailingPercent":0.0,"totalQuantity":1,"filledQuantity":0,"cashQuantity":0.0,"lastFillPrice":0.0,"avgFillPrice":0.0,"timeInForce":"DAY","expireTime":0,"goodTillDate":null,"outsideRth":true,"commission":0.0,"realizedPnl":0.0,"remark":"You order[BUY 1 AAPL] will not be placed until 2023-01-05 04:00:00, local time of the exchange","liquidation":false,"openTime":1672900550000,"updateTime":1672900550000,"latestTime":1672900551000,"name":"Apple","latestPrice":126.625,"attrDesc":"","userMark":"","ocaGroupId":0,"comboLegs":null,"allocAccounts":null,"allocShares":null,"algoStrategy":"LMT","algoParameters":null,"status":"Initial","source":null,"discount":0.0,"canModify":true,"canCancel":true}]},"code":0,"message":"success","timestamp":1672900550991,"sign":"vgRX7Z8v2dYNqtzI1RoqD2A7GTOPckQLrN4dOv29l0bcF4GUzNLIRfQd5PPb6o3coV91PfqSPGSlzdRYfUCgMbeZaUPkOtd9v+5KZD6wwyjzT6gviZIYjbPSdboTe64cZ/g8uL3MO/SMLh4SrwLaHbmu9yGf0QgXoL83wjDDgIU="} 

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

    //ApiLogger.Info("response:" + JsonConvert.SerializeObject(response, TigerClient.JsonSet));
    //QueryOrderUsePageTokenAsync(tradeClient);
    Thread.Sleep(1000);

    // =================================================Push
    //SubscribePush();

    ApiLogger.Info("end");
  }

  static void SubscribePush()
  {
    // tiger config
    TigerConfig config = new TigerConfig()
    {
      License = License.TBNZ,// must
      TigerId = "20151141", // must
      DefaultAccount = "13810712",// (optional) prime accout:572386, paper account: 20200821144442583
      PrivateKey = TigerConfig.ReadPrivateKey("/Users/tiger/dev/liutp_account/rsa_private_key_pkcs8_sandbox.pem"),// must
      FailRetryCounts = 2, // (optional) range:[1, 5],  default is 2
      AutoGrabPermission = false,   // (optional) default is true
      Language = Language.en_US,   // (optional) default is en_US
      TimeZone = CustomTimeZone.HK_ZONE,  // (optional) default is HK_ZONE
      IsSslSocket = true,
      Environment = Env.TEST
    };
    ApiLogger.DebugEnabled = false;

    IApiComposeCallback callback = new DefaultApiComposeCallback();
    PushClient client = PushClient.GetInstance().Config(config)
      .ApiComposeCallback(callback);
    ApiLogger.Info($"======================{client.GetUrl()}");
    client.Connect();

    //SubscribeAsset();
    //SubscribeQuote();
    SubscribeTradeTick();

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
    ApiLogger.Info($"CancelSubscribeQuote:{client.CancelSubscribeQuote(symbols)}");
    Sleep(2);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(2);
  }

  public static void SubscribeTradeTick()
  {
    PushClient client = PushClient.GetInstance();

    ISet<string> symbols = new HashSet<string>();
    symbols.Add("00700");
    ApiLogger.Info($"SubscribeTradeTick:{client.SubscribeTradeTick(symbols)}");
    Sleep(30);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    ApiLogger.Info($"CancelSubscribeTradeTick:{client.CancelSubscribeTradeTick(symbols)}");
    Sleep(2);
    ApiLogger.Info($"GetSubscribedSymbols:{client.GetSubscribedSymbols()}");
    Sleep(2);
  }

  static void Sleep(int seconds)
  {
    Thread.Sleep(TimeSpan.FromSeconds(seconds));
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
        Account = "20200821144442583",// tradeClient.GetDefaultAccount,
        Id = 29360305075913728
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

  static async Task<PlaceOrderResponse?> PlaceBracketsOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.buildStockContract("01810", Currency.HKD.ToString());
    PlaceOrderModel placeOrder = PlaceOrderModel.buildLimitOrder(
        "U10010705", // only support Global Account
        contract,
        ActionType.BUY,
        200, 11.0
      );
    // addBracketsOrder
    placeOrder.addBracketsOrder(13.0, TimeInForce.DAY, true, 10.0, TimeInForce.DAY);

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = placeOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceStopLossTrailOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.buildStockContract("01810", Currency.HKD.ToString());
    PlaceOrderModel placeOrder = PlaceOrderModel.buildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        200, 11.0
      );
    // addStopLossTrailOrder ('stopLossTrailingPercent' = 10%)
    placeOrder.addStopLossTrailOrder(10.0, 0, TimeInForce.DAY);
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
    ContractItem contract = ContractItem.buildOptionContract("AAPL", "20230127", 135.0, "CALL");
    PlaceOrderModel placeOrder = PlaceOrderModel.buildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        1, 1.30
      );
    // addStopLossLimitOrder
    placeOrder.addStopLossLimitOrder(1.0, 0.9, TimeInForce.DAY);
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
    ContractItem contract = ContractItem.buildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.buildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        200, 11.0
      ).addStopLossOrder(10.0, TimeInForce.DAY) // addStopLossOrder(not support options)
    };
    // set other parameter
    ((PlaceOrderModel)request.ModelValue).UserMark = "test001";
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceProfitTakerOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.buildStockContract("01810", Currency.HKD.ToString());
    
    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.buildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        200, 11.0
      ).addProfitTakerOrder(13.0, TimeInForce.DAY, true) // addProfitTakerOrder
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceTrailOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.buildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.buildTrailOrder(
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
    ContractItem contract = ContractItem.buildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.buildStopLimitOrder(
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
    ContractItem contract = ContractItem.buildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.buildStopOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.SELL,
        200, 10.0
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceLimitOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.buildStockContract("AAPL", Currency.USD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.buildLimitOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        1, 120.0
      )
    };
    // set other parameter
    // ((PlaceOrderModel)request.ModelValue).UserMark = "test-lmt";
    // ((PlaceOrderModel)request.ModelValue).TimeInForce = TimeInForce.GTD;
    // ((PlaceOrderModel)request.ModelValue).ExpireTime = DateUtil.ConvertTimestamp(
    //  "2023-01-20 23:59:59", SymbolUtil.getZoneIdBySymbol("AAPL", tradeClient.GetConfigTimeZone));
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PlaceOrderResponse?> PlaceMarketOrderAsync(TradeClient tradeClient)
  {
    ContractItem contract = ContractItem.buildStockContract("01810", Currency.HKD.ToString());

    TigerRequest<PlaceOrderResponse> request = new TigerRequest<PlaceOrderResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = PlaceOrderModel.buildMarketOrder(
        "20200821144442583", // tradeClient.GetDefaultAccount,
        contract,
        ActionType.BUY,
        200
      )
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<TigerDictResponse?> PlaceOrderAsync(TradeClient tradeClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = TradeApiService.PLACE_ORDER,
      ModelValue = new PlaceOrderModel() {
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
        StartDate = "2022-12-26"
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PrimeAssetResponse?> GetPrimeAssetsAsync(TradeClient tradeClient)
  {
    TigerRequest<PrimeAssetResponse> request = new TigerRequest<PrimeAssetResponse>()
    {
      ApiMethodName = TradeApiService.PRIME_ASSETS,
      ModelValue = new TradeModel()
      {
        Account = "572386"
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<TigerDictResponse?> GetGlobalAssetsAsync(TradeClient tradeClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = TradeApiService.ASSETS,
      ModelValue = new GlobalAssetsModel() {
        Account = "U10010705",
        Segment = true, MarketValue = true
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<PositionsResponse?> GetPositionsAsync(TradeClient tradeClient)
  {
    TigerRequest<PositionsResponse> request = new TigerRequest<PositionsResponse>()
    {
      ApiMethodName = TradeApiService.POSITIONS,
      ModelValue = new PositionsModel() { SecType = SecType.STK, Market = Market.US }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<AccountsResponse?> GetAccountsAsync(TradeClient tradeClient)
  {
    TigerRequest<AccountsResponse> request = new TigerRequest<AccountsResponse>()
    {
      ApiMethodName = TradeApiService.ACCOUNTS,
      ModelValue = new ApiModel() {}
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
        Symbols = new List<string> { "AAPL", "TSLA" }
      }
    };
    return await tradeClient.ExecuteAsync(request);
  }

  static async Task<ContractResponse?> GetContractAsync(TradeClient tradeClient)
  {
    TigerRequest<ContractResponse> request = new TigerRequest<ContractResponse>()
    {
      ApiMethodName = TradeApiService.CONTRACT,
      ModelValue = new ContractModel()
      {
        Symbol = "AAPL"
      }
    };
    return await tradeClient.ExecuteAsync(request);
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
        Lang = Language.zh_CN
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
        Lang = Language.zh_CN,
        SortFieldName = "expireDate",
        SortDir = SortDir.SortDir_Descend,
        WarrantType = new HashSet<Int32>() { (int)WarrantType.Bull },
        IssuerName = "高盛",
        Strike = new Range<double>(300, 320.0),
        Page = 0,
        PageSize = 10
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
        Expiry = "20230320"
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
        ContractCode = "ES2303",
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
        ContractCodes = new List<string> { "CL2303" }
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
        ContractCodes = new List<string> { "ES2303" },
        Period = FutureKType.min15.Value,
        BeginTime = DateUtil.ConvertTimestamp("2022-12-28 09:00:00", CustomTimeZone.NY_ZONE),
        EndTime = DateUtil.ConvertTimestamp("2022-12-28 20:00:00", CustomTimeZone.NY_ZONE),
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
      ModelValue = new FutureTradingDateModel() {
        ContractCode = "ES2303",
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
      ModelValue = new FutureContractByTypeModel() { FutureType = "ES" }
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
      ModelValue = new FutureContractByConCodeModel() { ContractCode = "ES2303" }
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
          new OptionCommonModel() { Symbol = "AAPL", Right = "PUT", Strike = "130.0",
            Expiry = DateUtil.ConvertTimestamp("2023-01-06", CustomTimeZone.NY_ZONE)}
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetOptionKLineAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_KLINE,
      ModelValue = new BatchApiModel<OptionKlineModel>()
      {
        Items = new List<OptionKlineModel>()
        {
          new OptionKlineModel() { Symbol = "AAPL", Right = "PUT", Strike = "130.0",
            Expiry = DateUtil.ConvertTimestamp("2023-01-06", CustomTimeZone.NY_ZONE),
            BeginTime = DateUtil.ConvertTimestamp("2022-12-15", CustomTimeZone.NY_ZONE),
            EndTime = DateUtil.ConvertTimestamp("2022-12-30", CustomTimeZone.NY_ZONE),
          }
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetOptionBriefAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_BRIEF,
      ModelValue = new BatchApiModel<OptionCommonModel>()
      {
        Items = new List<OptionCommonModel>()
        {
          new OptionCommonModel() { Symbol = "AAPL", Right = "PUT", Strike = "130.0",
            Expiry = DateUtil.ConvertTimestamp("2023-01-06", CustomTimeZone.NY_ZONE)}
        }
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetOptionChainAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.OPTION_CHAIN,
      ModelValue = new OptionChainV3Model()
      {
        OptionBasic = new List<OptionChainModel>()
        {
          new OptionChainModel() { Symbol = "AAPL", Expiry = DateUtil.ConvertTimestamp("2023-01-27", CustomTimeZone.NY_ZONE)}
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

  static async Task<TigerListResponse?> GetOptionExpirationAsync(QuoteClient quoteClient)
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

  static async Task<TigerDictResponse?> GetStockBrokerAsync(QuoteClient quoteClient)
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

  static async Task<TigerDictResponse?> GetStockCaptialDistributionAsync(QuoteClient quoteClient)
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

  static async Task<TigerDictResponse?> GetStockCaptialFlowAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = QuoteApiService.CAPITAL_FLOW,
      ModelValue = new QuoteCapitalFlowModel()
      {
        Symbol = "AAPL",
        Market = Market.US,
        Period = CapitalPeriod.day.Value,
        BeginTime = DateUtil.ConvertTimestamp("2022-12-01", CustomTimeZone.NY_ZONE),
        EndTime = DateUtil.ConvertTimestamp("2022-12-28", CustomTimeZone.NY_ZONE)
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetStockTradeInfoAsync(QuoteClient quoteClient)
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

  static async Task<TigerListResponse?> GetTradeTickAsync(QuoteClient quoteClient)
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

  static async Task<TigerListResponse?> GetDepthQuoteAsync(QuoteClient quoteClient)
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

  static async Task<TigerListResponse?> GetKLineAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.KLINE,
      ModelValue = new QuoteKlineModel()
      {
        Symbols = new List<string> { "AAPL" },
        Period = KLineType.day.Value,
        BeginTime = DateUtil.ConvertTimestamp("2022-12-10", CustomTimeZone.NY_ZONE),
        EndTime = DateUtil.CurrentTimeMillis(),
        Rigth = RightOption.br
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetRealTimeQuoteAsync(QuoteClient quoteClient)
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

  static async Task<TigerListResponse?> GetHistoryTimelineAsync(QuoteClient quoteClient)
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

  static async Task<TigerListResponse?> GetTimelineAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.TIMELINE,
      ModelValue = new QuoteTimelineModel()
      {
        Symbols = new List<string> { "AAPL" },
        Period = TimeLineType.day,
        BeginTime = DateUtil.ConvertTimestamp("2022-12-07 03:00:00", CustomTimeZone.NY_ZONE)
      }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetDelayQuoteAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.QUOTE_DELAY,
      ModelValue = new QuoteSymbolModel() { Symbols = new List<string> { "AAPL" } }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetAllSymbolNamesAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
    {
      ApiMethodName = QuoteApiService.ALL_SYMBOL_NAMES,
      ModelValue = new QuoteMarketModel() { Market = Market.US, Lang = Language.zh_CN }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListStringResponse?> GetAllSymbolsAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListStringResponse> request = new TigerRequest<TigerListStringResponse>()
    {
      ApiMethodName = QuoteApiService.ALL_SYMBOLS,
      ModelValue = new QuoteMarketModel() { Market = Market.US }
    };
    return await quoteClient.ExecuteAsync(request);
  }

  static async Task<TigerListResponse?> GetTradingCalendarAsync(QuoteClient quoteClient)
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

  static async Task<TigerListResponse?> GetMarketStateAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
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
}