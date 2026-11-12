using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Quote.Pb;

namespace TigerOpenAPI.Tests.Unit
{
  public class GthQuoteTest
  {
    // Fixtures generated from stock-openapi feat_gth commit 33df7142f.
    [TestCase("0a185350585720323032363037323320373338302e3020505554100118a4fb9df1f83331000000000000f83f3889fa9df1f833499a9999999999f13f500e5900000000000029407a095072654d61726b6574e9010000000000001c40f001d00ff901000000008087c340820203475448", 1.5, 1784796380425UL, 7L, 12.5)]
    [TestCase("0a185350585720323032363037323320373338302e3020505554100118a4fb9df1f8333100000000000000003800499a9999999999f13f50005900000000000000007a095072654d61726b6574e9010000000000000000f001d00ff901000000008087c340820203475448", 0.0, 0UL, 0L, 0.0)]
    public void ServerWireSurvivesBasicCallbackConversion(string hex, double price, ulong time, long volume, double amount)
    {
      var q = QuoteData.Parser.ParseFrom(Convert.FromHexString(hex));
      var basic = QuoteDataUtil.ConvertToBasicData(q)!;
      Assert.That(q.HasLatestPrice, Is.True);
      Assert.That(q.LatestPrice, Is.EqualTo(price));
      Assert.That(basic.HasLatestPrice, Is.True);
      Assert.That(basic.LatestPrice, Is.EqualTo(price));
      Assert.That(basic.HasLatestPriceTimestamp, Is.True);
      Assert.That(basic.LatestPriceTimestamp, Is.EqualTo(time));
      Assert.That(basic.HasVolume, Is.True);
      Assert.That(basic.Volume, Is.EqualTo(volume));
      Assert.That(basic.HasVolumeDecimal, Is.True);
      Assert.That(basic.VolumeDecimal, Is.EqualTo((double)volume));
      Assert.That(basic.HasAmount, Is.True);
      Assert.That(basic.Amount, Is.EqualTo(amount));
      Assert.That(basic.HasCumVolume, Is.True);
      Assert.That(basic.CumVolume, Is.EqualTo(1000));
      Assert.That(basic.HasCumAmount, Is.True);
      Assert.That(basic.CumAmount, Is.EqualTo(9999));
      Assert.That(basic.Session, Is.EqualTo("GTH"));
      Assert.That(basic.HourTradingTag, Is.EqualTo("PreMarket"));
      Assert.That(basic.PreClose, Is.EqualTo(1.1));
      Assert.That(QuoteData.Descriptor.FindFieldByName("hourTrading"), Is.Null);
      Assert.That(QuoteBasicData.Descriptor.FindFieldByName("hourTrading"), Is.Null);
    }

    [Test]
    public void BasicConversionKeepsMissingTradeFieldsAndSessionAbsent()
    {
      var basic = QuoteDataUtil.ConvertToBasicData(new QuoteData { Type = SocketCommon.Types.QuoteType.Basic })!;
      Assert.That(basic.HasLatestPrice, Is.False);
      Assert.That(basic.HasLatestPriceTimestamp, Is.False);
      Assert.That(basic.HasLatestTime, Is.False);
      Assert.That(basic.HasPreClose, Is.False);
      Assert.That(basic.HasVolume, Is.False);
      Assert.That(basic.HasVolumeDecimal, Is.False);
      Assert.That(basic.HasAmount, Is.False);
      Assert.That(basic.HasCumVolume, Is.False);
      Assert.That(basic.HasCumAmount, Is.False);
      Assert.That(basic.HasSession, Is.False);
      Assert.That(basic.HasHourTradingTag, Is.False);
    }

    [TestCase(null)]
    [TestCase(true)]
    [TestCase(false)]
    public void KlineFlagPreservesOmissionAndFalse(bool? flag)
    {
      foreach (var model in new object[] {
        new OptionKlineModel { WithHourTrading = flag },
        new OptionKlineQueryItem { WithHourTrading = flag } })
      {
        var json = JObject.Parse(JsonConvert.SerializeObject(model));
        if (flag is null) Assert.That(json.ContainsKey("with_hour_trading"), Is.False);
        else Assert.That(json.Value<bool>("with_hour_trading"), Is.EqualTo(flag.Value));
      }
    }

    [Test]
    public void HttpOptionResponsesPreserveNestedFields()
    {
      const string json = "{\"cumVolume\":0,\"cumAmount\":0,\"hourTrading\":{\"session\":\"GTH\",\"latestTimestamp\":1784796380425,\"volume\":0}}";
      var brief = JsonConvert.DeserializeObject<OptionBriefItem>(json)!;
      var leg = JsonConvert.DeserializeObject<OptionRealTimeQuote>(json)!;
      Assert.That(brief.CumVolume, Is.EqualTo(0));
      Assert.That(brief.HourTrading!.LatestTimestamp, Is.EqualTo(1784796380425L));
      Assert.That(leg.HourTrading!.Volume, Is.EqualTo(0));
      Assert.That(JsonConvert.DeserializeObject<OptionBriefItem>("{\"hourTrading\":null}")!.HourTrading, Is.Null);
    }
  }
}
