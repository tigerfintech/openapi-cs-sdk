using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for StockPriceUtil tick-size rounding logic.
  /// Migrated from Sample/StockPriceUtilTest.cs (fixed Assert.Equals -> Assert.That).
  /// </summary>
  [TestFixture]
  public class StockPriceUtilTest
  {
    private List<TickSizeItem>? _tickSizes;

    [SetUp]
    public void SetUp()
    {
      const string content =
          "[{\"begin\":\"0\",\"end\":\"0.25\",\"type\":\"CLOSED\",\"tickSize\":0.001},"
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
      _tickSizes = JsonConvert.DeserializeObject<List<TickSizeItem>>(content);
    }

    [Test]
    public void FixPriceByTickSize_RoundsDown_WhenExactMatch()
    {
      Assert.That(StockPriceUtil.FixPriceByTickSize(10.34m, _tickSizes), Is.EqualTo(10.34m));
    }

    [Test]
    public void FixPriceByTickSize_RoundsDown_WhenNotOnTick()
    {
      Assert.That(StockPriceUtil.FixPriceByTickSize(10.35m, _tickSizes), Is.EqualTo(10.34m));
    }

    [Test]
    public void FixPriceByTickSize_ReturnsExact_WhenOnTick()
    {
      Assert.That(StockPriceUtil.FixPriceByTickSize(10.36m, _tickSizes), Is.EqualTo(10.36m));
    }

    [Test]
    public void FixPriceByTickSize_RoundsUp_WhenUpTrue()
    {
      Assert.That(StockPriceUtil.FixPriceByTickSize(10.35m, _tickSizes, true), Is.EqualTo(10.36m));
    }

    [Test]
    public void MatchTickSize_True_WhenOnTick()
    {
      Assert.That(StockPriceUtil.MatchTickSize(10.34m, _tickSizes), Is.True);
      Assert.That(StockPriceUtil.MatchTickSize(10.36m, _tickSizes), Is.True);
      Assert.That(StockPriceUtil.MatchTickSize(10.360m, _tickSizes), Is.True);
    }

    [Test]
    public void MatchTickSize_False_WhenOffTick()
    {
      Assert.That(StockPriceUtil.MatchTickSize(10.35m, _tickSizes), Is.False);
      Assert.That(StockPriceUtil.MatchTickSize(10.361m, _tickSizes), Is.False);
    }
  }
}
