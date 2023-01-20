using System;
using TigerOpenAPI.Quote.Pb;
using static TigerOpenAPI.Quote.Pb.SocketCommon.Types;

namespace TigerOpenAPI.Common.Util
{
  public class QuoteDataUtil
  {
    private QuoteDataUtil()
    {
    }

    public static QuoteBBOData? ConvertToAskBidData(QuoteData quoteData)
    {
      if (quoteData == null || quoteData.Type == QuoteType.None)
      {
        return null;
      }
      QuoteType type = quoteData.Type;
      if (QuoteType.All != type && QuoteType.Bbo != type)
      {
        return null;
      }
      QuoteBBOData quoteBBOData = new QuoteBBOData()
      {
        Symbol = quoteData.Symbol,
        Type = QuoteType.Bbo,
        Timestamp = quoteData.Timestamp,
        AskPrice = quoteData.AskPrice,
        AskSize = quoteData.AskSize,
        BidPrice = quoteData.BidPrice,
        BidSize = quoteData.BidSize
      };
      if (quoteData.HasAskTimestamp)
      {
        quoteBBOData.AskTimestamp = quoteData.AskTimestamp;
      }
      if (quoteData.HasBidTimestamp)
      {
        quoteBBOData.BidTimestamp = quoteData.BidTimestamp;
      }

      return quoteBBOData;
    }

    public static QuoteBasicData? ConvertToBasicData(QuoteData quoteData)
    {
      if (quoteData == null || quoteData.Type == QuoteType.None)
      {
        return null;
      }
      QuoteType type = quoteData.Type;
      if (QuoteType.All != type && QuoteType.Basic != type)
      {
        return null;
      }

      QuoteBasicData quoteBasicData = new QuoteBasicData();
      quoteBasicData.Symbol = quoteData.Symbol;
      quoteBasicData.Type = QuoteType.Basic;
      quoteBasicData.Timestamp = quoteData.Timestamp;
      if (quoteData.HasServerTimestamp)
      {
        quoteBasicData.ServerTimestamp = quoteData.ServerTimestamp;
      }
      if (quoteData.HasAvgPrice)
      {
        quoteBasicData.AvgPrice = quoteData.AvgPrice;
      }
      quoteBasicData.LatestPrice = quoteData.LatestPrice;
      if (quoteData.HasLatestPriceTimestamp)
      {
        quoteBasicData.LatestPriceTimestamp = quoteData.LatestPriceTimestamp;
      }
      quoteBasicData.LatestTime = quoteData.LatestTime;
      quoteBasicData.PreClose = quoteData.PreClose;
      quoteBasicData.Volume = quoteData.Volume;
      if (quoteData.HasAmount)
      {
        quoteBasicData.Amount = quoteData.Amount;
      }

      if (quoteData.HasOpen)
      {
        quoteBasicData.Open = quoteData.Open;
      }
      if (quoteData.HasHigh)
      {
        quoteBasicData.High = quoteData.High;
      }
      if (quoteData.HasLow)
      {
        quoteBasicData.Low = quoteData.Low;
      }

      if (quoteData.HasHourTradingTag)
      {
        quoteBasicData.HourTradingTag = quoteData.HourTradingTag;
      }
      if (quoteData.HasMarketStatus)
      {
        quoteBasicData.MarketStatus = quoteData.MarketStatus;
      }

      // only Options support
      if (quoteData.HasIdentifier)
      {
        quoteBasicData.Identifier = quoteData.Identifier;
      }
      // open interest, only Options support
      if (quoteData.HasOpenInt)
      {
        quoteBasicData.OpenInt = quoteData.OpenInt;
      }
      // latest trad time, only Futures support
      if (quoteData.HasTradeTime)
      {
        quoteBasicData.TradeTime = quoteData.TradeTime;
      }
      // previous settlement price, only Futures support
      if (quoteData.HasPreSettlement)
      {
        quoteBasicData.PreSettlement = quoteData.PreSettlement;
      }
      // min tick, only Futures support
      if (quoteData.HasMinTick)
      {
        quoteBasicData.MinTick = quoteData.MinTick;
      }

      if (quoteData.Mi is not null)
      {
        quoteBasicData.Mi = quoteData.Mi;
      }

      return quoteBasicData;
    }
  }
}

