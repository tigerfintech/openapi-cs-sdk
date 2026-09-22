using System;
using System.Collections.Generic;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Push
{
  public interface ISubscribeAsyncApi
  {

    /**
     * subscribe trade data, include Order / Position / Asset / OrderTransaction
     *
     * @param subject trade subject
     * @return request id
     */
    uint Subscribe(Subject subject);

    /**
     * subscribe trade data, include Order / Position / Asset / OrderTransaction
     *
     * @param subject trade subject
     * @param account subscribe account
     * @return request id
     */
    uint Subscribe(Subject subject, string account);

    /**
     * subscribe trade data with the specified asset quote type.
     * assetQuoteType only takes effect on Asset / Position subject,
     * the enum name (ETH / RTH / OVERNIGHT) is sent to the server.
     *
     * The default implementation keeps source compatibility for existing implementors
     * (including test mocks) by falling back to {@link #Subscribe(Subject, string)},
     * which ignores assetQuoteType. Implementations that need the asset quote type
     * must override this method.
     *
     * @param subject trade subject
     * @param account subscribe account, nullable
     * @param assetQuoteType asset quote type, nullable(the field is not sent when null)
     * @return request id
     */
    uint Subscribe(Subject subject, string? account, AssetQuoteType? assetQuoteType)
      => Subscribe(subject, account!);

    /**
     * cancel subscribe all trade data, include Order / Position / Asset / OrderTransaction
     *
     * @param subject trade subject
     * @return request id
     */
    uint CancelSubscribe(Subject subject);

    /**
     * subscrie quote
     *
     * @param symbols symbol list
     * @return request id
     */
    uint SubscribeQuote(ISet<string> symbols);

    /**
     * cancel subscribe quote data,include stock / option / futures
     *
     * @param symbols symbol list
     * @return request id
     */
    uint CancelSubscribeQuote(ISet<string> symbols);

    /**
     * subscribe stock trade tick data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint SubscribeTradeTick(ISet<string> symbols);

    /**
     * cancel subscribe stock trade tick data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint CancelSubscribeTradeTick(ISet<string> symbols);

    /**
     * subscribe option data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint SubscribeOption(ISet<string> symbols);

    /**
     * cancel subscribe option data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint CancelSubscribeOption(ISet<string> symbols);

    /**
     * subscribe futures data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint SubscribeFuture(ISet<string> symbols);

    /**
     * cancel subscribe futures data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint CancelSubscribeFuture(ISet<string> symbols);

    /**
     * subscribe depth data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint SubscribeDepthQuote(ISet<string> symbols);

    /**
     * cancel subscribe depth data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint CancelSubscribeDepthQuote(ISet<string> symbols);

    /**
     * subscribe bar data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint SubscribeKline(ISet<string> symbols);

    /**
     * cancel subscribe bar data
     *
     * @param symbols symbol list
     * @return request id
     */
    uint CancelSubscribeKline(ISet<string> symbols);

    /**
     * subscribe quote-data of the specified market
     * @param market Market
     * @param subject QuoteSubject
     * @return request id
     */
    uint SubscribeMarketQuote(Market market, QuoteSubject subject);

    /**
     * cancel subscribe quote-data of the specified market
     * @param market Market
     * @param subject QuoteSubject
     * @return request id
     */
    uint CancelSubscribeMarketQuote(Market market, QuoteSubject subject);

    /**
     * subscribe stock-top-data of the specified market
     * @param market Market
     * @param indicators stock top quote's indicator
     * @return
     */
    uint SubscribeStockTop(Market market, ISet<Indicator>? indicators = null);

    /**
     * cancel subscribe stock-top-data of the specified market
     * @param market Market
     * @param indicators stock top quote's indicator
     * @return
     */
    uint CancelSubscribeStockTop(Market market, ISet<Indicator>? indicators = null);

    /**
     * subscribe option-top-data of the specified market
     * @param market Market
     * @param indicators option top quote's indicator
     * @return
     */
    uint SubscribeOptionTop(Market market, ISet<Indicator>? indicators = null);

    /**
     * cancel subscribe option-top-data of the specified market
     * @param market Market
     * @param indicators option top quote's indicator
     * @return
     */
    uint CancelSubscribeOptionTop(Market market, ISet<Indicator>? indicators = null);

    /**
     * query subscribed symbol list
     *
     * @return request id
     */
    uint GetSubscribedSymbols();
  }
}

