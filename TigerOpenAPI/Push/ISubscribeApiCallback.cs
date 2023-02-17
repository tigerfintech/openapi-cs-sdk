using System;
using TigerOpenAPI.Push.Model;
using TigerOpenAPI.Quote.Pb;

namespace TigerOpenAPI.Push
{
  public interface ISubscribeApiCallback
  {

    void OrderStatusChange(OrderStatusData data);

    void OrderTransactionChange(OrderTransactionData data);

    void PositionChange(PositionData data);

    void AssetChange(AssetData data);

    void TradeTickChange(TradeTick data);

    void QuoteChange(QuoteBasicData data);
    void QuoteAskBidChange(QuoteBBOData data);

    void OptionChange(QuoteBasicData data);
    void OptionAskBidChange(QuoteBBOData data);

    void FutureChange(QuoteBasicData data);
    void FutureAskBidChange(QuoteBBOData data);

    void DepthQuoteChange(QuoteDepthData data);

    void SubscribeEnd(int id, string subject, string result);

    void CancelSubscribeEnd(int id, string subject, string result);

    void GetSubscribedSymbolEnd(SubscribedSymbol subscribedSymbol);
  }
}

