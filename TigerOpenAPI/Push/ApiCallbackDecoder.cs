using System;
using DotNetty.Common.Utilities;
using Google.Protobuf.WellKnownTypes;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Push.Model;
using TigerOpenAPI.Quote.Pb;
using TigerOpenAPI.Trade.Response;
using static TigerOpenAPI.Quote.Pb.SocketCommon.Types;
using Newtonsoft.Json;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Push
{
  public class ApiCallbackDecoder
  {
    private IApiComposeCallback callback;

    public ApiCallbackDecoder(IApiComposeCallback callback)
    {
      this.callback = callback;
    }

    public IApiComposeCallback GetCallback() => callback;

    public void Handle(Response msg)
    {
      ProtocolCodeType code = (ProtocolCodeType)msg.Code;

      switch (code)
      {
        case ProtocolCodeType.GET_SUB_SYMBOLS_END:
          ProcessGetSubscribedSymbols(msg);
          break;
        case ProtocolCodeType.GET_SUBSCRIBE_END:
          ProcessSubscribeEnd(msg);
          break;
        case ProtocolCodeType.GET_CANCEL_SUBSCRIBE_END:
          ProcessCancelSubscribeEnd(msg);
          break;
        case ProtocolCodeType.ERROR_END:
          ProcessErrorEnd(msg);
          break;
        default:
          ProcessSubscribeDataChange(msg);
          break;
      }
    }

    private void ProcessSubscribeDataChange(Response msg)
    {
      PushData pushData = msg.Body;
      if (pushData == null || pushData.DataType == DataType.Unknown)
      {
        return;
      }
      QuoteBasicData? basicData = null;
      QuoteBBOData? bboData = null;
      DataType dataType = pushData.DataType;
      switch (dataType)
      {
        case DataType.Quote:
          basicData = QuoteDataUtil.ConvertToBasicData(pushData.QuoteData);
          if (null != basicData)
          {
            callback.QuoteChange(basicData);
          }
          bboData = QuoteDataUtil.ConvertToAskBidData(pushData.QuoteData);
          if (null != bboData)
          {
            callback.QuoteAskBidChange(bboData);
          }
          break;
        case DataType.Option:
          basicData = QuoteDataUtil.ConvertToBasicData(pushData.QuoteData);
          if (null != basicData)
          {
            callback.OptionChange(basicData);
          }
          bboData = QuoteDataUtil.ConvertToAskBidData(pushData.QuoteData);
          if (null != bboData)
          {
            callback.OptionAskBidChange(bboData);
          }
          break;
        case DataType.Future:
          basicData = QuoteDataUtil.ConvertToBasicData(pushData.QuoteData);
          if (null != basicData)
          {
            callback.FutureChange(basicData);
          }
          bboData = QuoteDataUtil.ConvertToAskBidData(pushData.QuoteData);
          if (null != bboData)
          {
            callback.FutureAskBidChange(bboData);
          }
          break;
        case DataType.TradeTick:
          callback.TradeTickChange(TradeTickUtil.Convert(pushData.TradeTickData));
          break;
        case DataType.QuoteDepth:
          callback.DepthQuoteChange(pushData.QuoteDepthData);
          break;
        case DataType.Asset:
          callback.AssetChange(pushData.AssetData);
          break;
        case DataType.Position:
          callback.PositionChange(pushData.PositionData);
          break;
        case DataType.OrderStatus:
          callback.OrderStatusChange(pushData.OrderStatusData);
          break;
        case DataType.OrderTransaction:
          callback.OrderTransactionChange(pushData.OrderTransactionData);
          break;
        case DataType.StockTop:
          callback.StockTopPush(pushData.StockTopData);
          break;
        case DataType.OptionTop:
          callback.OptionTopPush(pushData.OptionTopData);
          break;
        default:
          ApiLogger.Info("push data cannot be processed. {}", msg);
          break;
      }
    }

    private void ProcessGetSubscribedSymbols(Response msg)
    {
      SubscribedSymbol? subscribedSymbol = JsonConvert.DeserializeObject<SubscribedSymbol>(msg.Msg);
      if (subscribedSymbol is not null)
        callback.GetSubscribedSymbolEnd(subscribedSymbol);
    }

    private void ProcessSubscribeEnd(Response msg)
    {
      DataType dataType = msg.Body.DataType;
      callback.SubscribeEnd((int)msg.Id, dataType.ToString(), msg.Msg);
    }

    private void ProcessCancelSubscribeEnd(Response msg)
    {
      DataType dataType = msg.Body.DataType;
      callback.CancelSubscribeEnd((int)msg.Id, dataType.ToString(), msg.Msg);
    }

    private void ProcessErrorEnd(Response msg)
    {
      if (!string.IsNullOrWhiteSpace(msg.Msg))
      {
        callback.Error(msg.Msg);
      }
      else
      {
        callback.Error("unknown error");
      }
    }

    private void ProcessDefault(Response msg)
    {
      ApiLogger.Info("receive message's code:{} cannot be processed.", msg.Code);
    }

    public void ProcessHeartBeat(string content)
    {
      callback.HearBeat(content);
    }

    public void ServerHeartBeatTimeOut(string channelId)
    {
      callback.ServerHeartBeatTimeOut(channelId);
    }
  }
}
