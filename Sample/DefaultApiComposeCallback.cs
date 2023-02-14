using System;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Push;
using TigerOpenAPI.Push.Model;
using TigerOpenAPI.Quote.Pb;

namespace Sample
{
  public class DefaultApiComposeCallback : IApiComposeCallback
  {
    public DefaultApiComposeCallback()
    {
    }

    void IApiComposeCallback.ConnectionAck()
    {
      ApiLogger.Info("connect success.");
    }

    void IApiComposeCallback.ConnectionAck(int serverSendInterval, int serverReceiveInterval)
    {
      ApiLogger.Info($"connect success. serverSendInterval:{serverSendInterval}," +
        $" serverReceiveInterval:{serverReceiveInterval}");
    }

    void IApiComposeCallback.HearBeat(string heartBeatContent)
    {
      ApiLogger.Info("HearBeat:" + heartBeatContent);
    }

    void IApiComposeCallback.ServerHeartBeatTimeOut(string channelId)
    {
      ApiLogger.Warn("ServerHeartBeatTimeOut:" + channelId);
    }

    void IApiComposeCallback.ConnectionClosed()
    {
      ApiLogger.Info("connection closed.");
    }

    void IApiComposeCallback.ConnectionKickout(int errorCode, string errorMsg)
    {
      ApiLogger.Info($"ConnectionKickout, errorCode:{errorCode}, errorMsg:{errorMsg}");
    }

    void IApiComposeCallback.Error(string errorMsg)
    {
      ApiLogger.Error("receive error:" + errorMsg);
    }

    void IApiComposeCallback.Error(int id, int errorCode, string errorMsg)
    {
      ApiLogger.Error($"receive error, id:{id}, errorCode:{errorCode}, errorMsg:{errorMsg}");
    }





    void ISubscribeApiCallback.GetSubscribedSymbolEnd(SubscribedSymbol subscribedSymbol)
    {
      ApiLogger.Info("GetSubscribedSymbolEnd:"
        + JsonConvert.SerializeObject(subscribedSymbol, TigerClient.JsonSet));
    }

    void ISubscribeApiCallback.SubscribeEnd(int id, string subject, string result)
    {
      ApiLogger.Info($"SubscribeEnd, {subject}, id:{id}, result:{result}");
    }

    void ISubscribeApiCallback.CancelSubscribeEnd(int id, string subject, string result)
    {
      ApiLogger.Info($"CancelSubscribeEnd, {subject}, id:{id}, result:{result}");
    }




    void ISubscribeApiCallback.AssetChange(AssetData data)
    {
      ApiLogger.Info("AssetChange:" + data);
    }

    void ISubscribeApiCallback.PositionChange(PositionData data)
    {
      ApiLogger.Info("PositionChange:" + data);
    }

    void ISubscribeApiCallback.OrderStatusChange(OrderStatusData data)
    {
      ApiLogger.Info("OrderStatusChange:" + data);
    }

    void ISubscribeApiCallback.OrderTransactionChange(OrderTransactionData data)
    {
      ApiLogger.Info("OrderTransactionChange:" + data);
    }




    void ISubscribeApiCallback.QuoteAskBidChange(QuoteBBOData data)
    {
      ApiLogger.Info("QuoteAskBidChange:" + data);
    }

    void ISubscribeApiCallback.QuoteChange(QuoteBasicData data)
    {
      ApiLogger.Info("QuoteChange:" + data);
    }

    void ISubscribeApiCallback.TradeTickChange(TradeTick data)
    {
      ApiLogger.Info("TradeTickChange:"
        + JsonConvert.SerializeObject(data, TigerClient.JsonSet));
    }

    void ISubscribeApiCallback.DepthQuoteChange(QuoteDepthData data)
    {
      ApiLogger.Info("DepthQuoteChange:" + data);
    }

    void ISubscribeApiCallback.FutureAskBidChange(QuoteBBOData data)
    {
      ApiLogger.Info("FutureAskBidChange:" + data);
    }

    void ISubscribeApiCallback.FutureChange(QuoteBasicData data)
    {
      ApiLogger.Info("FutureChange:" + data);
    }

    void ISubscribeApiCallback.OptionAskBidChange(QuoteBBOData data)
    {
      ApiLogger.Info("OptionAskBidChange:" + data);
    }

    void ISubscribeApiCallback.OptionChange(QuoteBasicData data)
    {
      ApiLogger.Info("OptionChange:" + data);
    }
  }
}

