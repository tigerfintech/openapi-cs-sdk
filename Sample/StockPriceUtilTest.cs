using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Trade.Response;

namespace Sample
{
  [TestFixture]
  public class StockPriceTests
  {
    [Test]
    public void TestStockPrice()
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

      Assert.Equals(10.34m, StockPriceUtil.FixPriceByTickSize(10.34m, tickSizeItemList));
      Assert.Equals(10.34m, StockPriceUtil.FixPriceByTickSize(10.35m, tickSizeItemList));
      Assert.Equals(10.36m, StockPriceUtil.FixPriceByTickSize(10.36m, tickSizeItemList));
      Assert.Equals(10.36d, StockPriceUtil.FixPriceByTickSize(10.35m, tickSizeItemList, true));

      Assert.Equals(true, StockPriceUtil.MatchTickSize(10.34m, tickSizeItemList));
      Assert.Equals(false, StockPriceUtil.MatchTickSize(10.35m, tickSizeItemList));
      Assert.Equals(true, StockPriceUtil.MatchTickSize(10.36m, tickSizeItemList));
      //
      Assert.Equals(true, StockPriceUtil.MatchTickSize(10.360m, tickSizeItemList));
      Assert.Equals(false, StockPriceUtil.MatchTickSize(10.361m, tickSizeItemList));
    }
  }
}
