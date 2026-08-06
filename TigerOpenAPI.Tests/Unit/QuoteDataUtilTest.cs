using NUnit.Framework;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Quote.Pb;
using static TigerOpenAPI.Quote.Pb.SocketCommon.Types;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for QuoteDataUtil.ConvertToAskBidData / ConvertToBasicData.
  /// Covers null guards, QuoteType filtering, field mapping (including Has-flag
  /// gated optional fields), and the All-type passthrough for both converters.
  /// </summary>
  [TestFixture]
  public class QuoteDataUtilTest
  {
    // ---------- ConvertToAskBidData ----------

    [Test]
    public void ConvertToAskBidData_WhenNull_ReturnsNull()
    {
      Assert.That(QuoteDataUtil.ConvertToAskBidData(null!), Is.Null);
    }

    [Test]
    public void ConvertToAskBidData_WhenTypeNone_ReturnsNull()
    {
      QuoteData qd = new QuoteData { Symbol = "AAPL", Type = QuoteType.None };
      Assert.That(QuoteDataUtil.ConvertToAskBidData(qd), Is.Null);
    }

    [Test]
    public void ConvertToAskBidData_WhenTypeBasic_ReturnsNull()
    {
      // Basic is neither All nor Bbo -> filtered out
      QuoteData qd = new QuoteData { Symbol = "AAPL", Type = QuoteType.Basic };
      Assert.That(QuoteDataUtil.ConvertToAskBidData(qd), Is.Null);
    }

    [Test]
    public void ConvertToAskBidData_WhenTypeBbo_MapsRequiredFields()
    {
      QuoteData qd = new QuoteData
      {
        Symbol = "AAPL",
        Type = QuoteType.Bbo,
        Timestamp = 1700000000000UL,
        AskPrice = 150.25,
        AskSize = 100L,
        BidPrice = 150.20,
        BidSize = 200L
      };

      QuoteBBOData? result = QuoteDataUtil.ConvertToAskBidData(qd);

      Assert.That(result, Is.Not.Null);
      Assert.That(result!.Symbol, Is.EqualTo("AAPL"));
      Assert.That(result.Type, Is.EqualTo(QuoteType.Bbo));
      Assert.That(result.Timestamp, Is.EqualTo(1700000000000UL));
      Assert.That(result.AskPrice, Is.EqualTo(150.25));
      Assert.That(result.AskSize, Is.EqualTo(100L));
      Assert.That(result.BidPrice, Is.EqualTo(150.20));
      Assert.That(result.BidSize, Is.EqualTo(200L));
      // timestamps not set -> Has flags false -> not propagated
      Assert.That(result.HasAskTimestamp, Is.False);
      Assert.That(result.HasBidTimestamp, Is.False);
    }

    [Test]
    public void ConvertToAskBidData_WhenAskBidTimestampsSet_PropagatesThem()
    {
      QuoteData qd = new QuoteData
      {
        Symbol = "AAPL",
        Type = QuoteType.Bbo,
        Timestamp = 1UL,
        AskPrice = 1.0,
        AskSize = 1L,
        BidPrice = 1.0,
        BidSize = 1L,
        AskTimestamp = 111UL,
        BidTimestamp = 222UL
      };

      QuoteBBOData? result = QuoteDataUtil.ConvertToAskBidData(qd);

      Assert.That(result, Is.Not.Null);
      Assert.That(result!.HasAskTimestamp, Is.True);
      Assert.That(result.HasBidTimestamp, Is.True);
      Assert.That(result.AskTimestamp, Is.EqualTo(111UL));
      Assert.That(result.BidTimestamp, Is.EqualTo(222UL));
    }

    [Test]
    public void ConvertToAskBidData_WhenTypeAll_ReturnsBboResult()
    {
      QuoteData qd = new QuoteData
      {
        Symbol = "TSLA",
        Type = QuoteType.All,
        Timestamp = 5UL,
        AskPrice = 250.0,
        AskSize = 10L,
        BidPrice = 249.0,
        BidSize = 20L
      };

      QuoteBBOData? result = QuoteDataUtil.ConvertToAskBidData(qd);

      Assert.That(result, Is.Not.Null);
      // All is accepted; result type is normalized to Bbo
      Assert.That(result!.Type, Is.EqualTo(QuoteType.Bbo));
      Assert.That(result.Symbol, Is.EqualTo("TSLA"));
    }

    // ---------- ConvertToBasicData ----------

    [Test]
    public void ConvertToBasicData_WhenNull_ReturnsNull()
    {
      Assert.That(QuoteDataUtil.ConvertToBasicData(null!), Is.Null);
    }

    [Test]
    public void ConvertToBasicData_WhenTypeNone_ReturnsNull()
    {
      QuoteData qd = new QuoteData { Symbol = "AAPL", Type = QuoteType.None };
      Assert.That(QuoteDataUtil.ConvertToBasicData(qd), Is.Null);
    }

    [Test]
    public void ConvertToBasicData_WhenTypeBbo_ReturnsNull()
    {
      // Bbo is neither All nor Basic -> filtered out
      QuoteData qd = new QuoteData { Symbol = "AAPL", Type = QuoteType.Bbo };
      Assert.That(QuoteDataUtil.ConvertToBasicData(qd), Is.Null);
    }

    [Test]
    public void ConvertToBasicData_WhenTypeBasic_MapsAllSetFields()
    {
      QuoteData qd = new QuoteData
      {
        Symbol = "AAPL",
        Type = QuoteType.Basic,
        Timestamp = 1700000000000UL,
        ServerTimestamp = 1700000000001UL,
        AvgPrice = 149.5,
        LatestPrice = 150.0,
        LatestPriceTimestamp = 1700000000002UL,
        LatestTime = "2023-11-14",
        PreClose = 148.0,
        Volume = 1000L,
        Amount = 150000.0,
        Open = 149.0,
        High = 151.0,
        Low = 148.5,
        HourTradingTag = "PRE",
        MarketStatus = "OPEN",
        Identifier = "AAPL240120C00150000",
        OpenInt = 500L,
        TradeTime = 1700000000003UL,
        PreSettlement = 147.0,
        MinTick = 0.01f
      };

      QuoteBasicData? result = QuoteDataUtil.ConvertToBasicData(qd);

      Assert.That(result, Is.Not.Null);
      Assert.That(result!.Symbol, Is.EqualTo("AAPL"));
      Assert.That(result.Type, Is.EqualTo(QuoteType.Basic));
      Assert.That(result.Timestamp, Is.EqualTo(1700000000000UL));
      Assert.That(result.HasServerTimestamp, Is.True);
      Assert.That(result.ServerTimestamp, Is.EqualTo(1700000000001UL));
      Assert.That(result.HasAvgPrice, Is.True);
      Assert.That(result.AvgPrice, Is.EqualTo(149.5));
      Assert.That(result.LatestPrice, Is.EqualTo(150.0));
      Assert.That(result.HasLatestPrice, Is.True);
      Assert.That(result.HasLatestPriceTimestamp, Is.True);
      Assert.That(result.LatestPriceTimestamp, Is.EqualTo(1700000000002UL));
      Assert.That(result.LatestTime, Is.EqualTo("2023-11-14"));
      Assert.That(result.PreClose, Is.EqualTo(148.0));
      Assert.That(result.Volume, Is.EqualTo(1000L));
      Assert.That(result.HasAmount, Is.True);
      Assert.That(result.Amount, Is.EqualTo(150000.0));
      Assert.That(result.HasOpen, Is.True);
      Assert.That(result.Open, Is.EqualTo(149.0));
      Assert.That(result.HasHigh, Is.True);
      Assert.That(result.High, Is.EqualTo(151.0));
      Assert.That(result.HasLow, Is.True);
      Assert.That(result.Low, Is.EqualTo(148.5));
      Assert.That(result.HasHourTradingTag, Is.True);
      Assert.That(result.HourTradingTag, Is.EqualTo("PRE"));
      Assert.That(result.HasMarketStatus, Is.True);
      Assert.That(result.MarketStatus, Is.EqualTo("OPEN"));
      Assert.That(result.HasIdentifier, Is.True);
      Assert.That(result.Identifier, Is.EqualTo("AAPL240120C00150000"));
      Assert.That(result.HasOpenInt, Is.True);
      Assert.That(result.OpenInt, Is.EqualTo(500L));
      Assert.That(result.HasTradeTime, Is.True);
      Assert.That(result.HasPreSettlement, Is.True);
      Assert.That(result.PreSettlement, Is.EqualTo(147.0));
      Assert.That(result.HasMinTick, Is.True);
      Assert.That(result.MinTick, Is.EqualTo(0.01f));
    }

    [Test]
    public void ConvertToBasicData_WhenOptionalFieldsUnset_HasFlagsFalse()
    {
      QuoteData qd = new QuoteData
      {
        Symbol = "AAPL",
        Type = QuoteType.Basic,
        Timestamp = 1UL,
        LatestPrice = 100.0,
        LatestTime = "",
        PreClose = 0.0,
        Volume = 0L
      };

      QuoteBasicData? result = QuoteDataUtil.ConvertToBasicData(qd);

      Assert.That(result, Is.Not.Null);
      // Has-gated fields not set on the source -> Has flags stay false
      Assert.That(result!.HasServerTimestamp, Is.False);
      Assert.That(result.HasAvgPrice, Is.False);
      Assert.That(result.HasLatestPriceTimestamp, Is.False);
      Assert.That(result.HasAmount, Is.False);
      Assert.That(result.HasOpen, Is.False);
      Assert.That(result.HasHigh, Is.False);
      Assert.That(result.HasLow, Is.False);
      Assert.That(result.HasHourTradingTag, Is.False);
      Assert.That(result.HasMarketStatus, Is.False);
      Assert.That(result.HasIdentifier, Is.False);
      Assert.That(result.HasOpenInt, Is.False);
      Assert.That(result.HasTradeTime, Is.False);
      Assert.That(result.HasPreSettlement, Is.False);
      Assert.That(result.HasMinTick, Is.False);
    }

    [Test]
    public void ConvertToBasicData_WhenTypeAll_ReturnsBasicResult()
    {
      QuoteData qd = new QuoteData
      {
        Symbol = "TSLA",
        Type = QuoteType.All,
        Timestamp = 5UL,
        LatestPrice = 250.0,
        LatestTime = "",
        PreClose = 240.0,
        Volume = 100L
      };

      QuoteBasicData? result = QuoteDataUtil.ConvertToBasicData(qd);

      Assert.That(result, Is.Not.Null);
      // All is accepted; result type normalized to Basic
      Assert.That(result!.Type, Is.EqualTo(QuoteType.Basic));
      Assert.That(result.Symbol, Is.EqualTo("TSLA"));
    }

    [Test]
    public void ConvertToBasicData_WhenMinuteSet_PropagatesMi()
    {
      QuoteData.Types.Minute minute = new QuoteData.Types.Minute { P = 1.0, A = 1.0, V = 1L, O = 1.0, H = 1.0, L = 1.0 };
      QuoteData qd = new QuoteData
      {
        Symbol = "AAPL",
        Type = QuoteType.Basic,
        Timestamp = 1UL,
        LatestPrice = 1.0,
        LatestTime = "",
        PreClose = 1.0,
        Volume = 1L,
        Mi = minute
      };

      QuoteBasicData? result = QuoteDataUtil.ConvertToBasicData(qd);

      Assert.That(result, Is.Not.Null);
      Assert.That(result!.Mi, Is.Not.Null);
      Assert.That(result.Mi.P, Is.EqualTo(1.0));
    }

    [Test]
    public void ConvertToBasicData_WhenMinuteUnset_MiStaysNull()
    {
      QuoteData qd = new QuoteData
      {
        Symbol = "AAPL",
        Type = QuoteType.Basic,
        Timestamp = 1UL,
        LatestPrice = 1.0,
        LatestTime = "",
        PreClose = 1.0,
        Volume = 1L
      };

      QuoteBasicData? result = QuoteDataUtil.ConvertToBasicData(qd);

      Assert.That(result, Is.Not.Null);
      Assert.That(result!.Mi, Is.Null);
    }
  }
}
