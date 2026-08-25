using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests JSON deserialization for all Quote/Trade Response classes not covered by
  /// QuoteResponseTest / TradeResponseTest. Zero network — pure deserialization assertions.
  /// </summary>
  [TestFixture]
  public class QuoteTradeResponseTest
  {
    // ---------------------------------------------------------------- Warrant

    [Test]
    public void WarrantQuoteResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":{""items"":[{""symbol"":""1052.HK"",""name"":""Bull"",
        ""exchange"":""HKFE"",""secType"":""WAR"",""market"":""HK"",""currency"":""HKD"",
        ""expiry"":""2026-12-30"",""strike"":""30000"",""right"":""CALL"",""multiplier"":100.0,
        ""lastTradingDate"":1700000000,""entitlementRatio"":10.0,""entitlementPrice"":0.5,
        ""minTick"":0.01,""listingDate"":1600000000,""callPrice"":1.2,""halted"":""Normal"",
        ""underlyingSymbol"":""HSI"",""timestamp"":1700000001,""latestPrice"":1.5,
        ""preClose"":1.4,""open"":1.45,""high"":1.6,""low"":1.3,""volume"":100000,
        ""amount"":150000.0,""premium"":0.02141,""outstandingRatio"":0.0019,
        ""impliedVolatility"":0.3,""inOutPrice"":0.20744,""delta"":0.5,
        ""leverageRatio"":5.0,""breakevenPoint"":29500.0}]}}";
      var resp = JsonConvert.DeserializeObject<WarrantQuoteResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Items.Count, Is.EqualTo(1));
      var q = resp.Data.Items[0];
      Assert.That(q.Symbol, Is.EqualTo("1052.HK"));
      Assert.That(q.Name, Is.EqualTo("Bull"));
      Assert.That(q.Exchange, Is.EqualTo("HKFE"));
      Assert.That(q.SecType, Is.EqualTo("WAR"));
      Assert.That(q.Market, Is.EqualTo("HK"));
      Assert.That(q.Currency, Is.EqualTo("HKD"));
      Assert.That(q.Expiry, Is.EqualTo("2026-12-30"));
      Assert.That(q.Strike, Is.EqualTo("30000"));
      Assert.That(q.Right, Is.EqualTo("CALL"));
      Assert.That(q.Multiplier, Is.EqualTo(100.0));
      Assert.That(q.LastTradingDate, Is.EqualTo(1700000000));
      Assert.That(q.EntitlementRatio, Is.EqualTo(10.0));
      Assert.That(q.EntitlementPrice, Is.EqualTo(0.5));
      Assert.That(q.MinTick, Is.EqualTo(0.01));
      Assert.That(q.ListingDate, Is.EqualTo(1600000000));
      Assert.That(q.CallPrice, Is.EqualTo(1.2));
      Assert.That(q.Halted, Is.EqualTo(HaltedStatus.Normal));
      Assert.That(q.UnderlyingSymbol, Is.EqualTo("HSI"));
      Assert.That(q.Timestamp, Is.EqualTo(1700000001));
      Assert.That(q.LatestPrice, Is.EqualTo(1.5));
      Assert.That(q.PreClose, Is.EqualTo(1.4));
      Assert.That(q.Open, Is.EqualTo(1.45));
      Assert.That(q.High, Is.EqualTo(1.6));
      Assert.That(q.Low, Is.EqualTo(1.3));
      Assert.That(q.Volume, Is.EqualTo(100000));
      Assert.That(q.Amount, Is.EqualTo(150000.0));
      Assert.That(q.Premium, Is.EqualTo(0.02141));
      Assert.That(q.OutstandingRatio, Is.EqualTo(0.0019));
      Assert.That(q.ImpliedVolatility, Is.EqualTo(0.3));
      Assert.That(q.InOutPrice, Is.EqualTo(0.20744));
      Assert.That(q.Delta, Is.EqualTo(0.5));
      Assert.That(q.LeverageRatio, Is.EqualTo(5.0));
      Assert.That(q.BreakevenPoint, Is.EqualTo(29500.0));
    }

    [Test]
    public void WarrantFilterResponse_Deserialize_WithBoundsAndItems()
    {
      string json = @"{""code"":0,""data"":{""page"":1,""totalPage"":3,""totalCount"":45,
        ""items"":[{""symbol"":""1052.HK"",""name"":""Bull"",""type"":""Call"",""secType"":""WAR"",
        ""market"":""HK"",""entitlementRatio"":10.0,""entitlementPrice"":0.5,""premium"":0.02,
        ""breakevenPoint"":29500.0,""callPrice"":1.2,""beforeCallLevel"":0.5,
        ""expireDate"":""2026-12-30"",""lastTradingDate"":""2026-12-29"",""state"":""Normal"",
        ""changeRate"":0.03,""change"":0.05,""latestPrice"":1.5,""volume"":100000,
        ""amount"":150000.0,""outstandingRatio"":0.0019,""lotSize"":1000,""strike"":""30000"",
        ""inOutPrice"":0.20744,""delta"":0.5,""leverageRatio"":5.0,""effectiveLeverage"":4.5,
        ""impliedVolatility"":0.3}],
        ""bounds"":{""issuerName"":[""GS""],""expireDate"":[""2026-12-30""],""lotSize"":[1000],
        ""entitlementRatio"":[10.0],""leverageRatio"":{""min"":1.0,""max"":10.0},
        ""strike"":{""min"":100.0,""max"":400.0},""premium"":{""min"":0.0,""max"":1.0},
        ""outstandingRatio"":{""min"":0.0,""max"":0.5},""impliedVolatility"":{""min"":0.1,""max"":0.9},
        ""effectiveLeverage"":{""min"":1.0,""max"":20.0},""callPrice"":{""min"":0.0,""max"":5.0}}}}";
      var resp = JsonConvert.DeserializeObject<WarrantFilterResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Page, Is.EqualTo(1));
      Assert.That(resp.Data.TotalPage, Is.EqualTo(3));
      Assert.That(resp.Data.TotalCount, Is.EqualTo(45));
      Assert.That(resp.Data.Items.Count, Is.EqualTo(1));
      var w = resp.Data.Items[0];
      Assert.That(w.Symbol, Is.EqualTo("1052.HK"));
      Assert.That(w.Type, Is.EqualTo(WarrantType.Call));
      Assert.That(w.State, Is.EqualTo(WarrantState.Normal));
      Assert.That(w.LotSize, Is.EqualTo(1000));
      Assert.That(w.Strike, Is.EqualTo("30000"));
      Assert.That(w.changeRate, Is.EqualTo(0.03));
      Assert.That(w.Change, Is.EqualTo(0.05));
      Assert.That(w.EffectiveLeverage, Is.EqualTo(4.5));
      var b = resp.Data.Bounds;
      Assert.That(b.IssuerName[0], Is.EqualTo("GS"));
      Assert.That(b.LeverageRatio.Min, Is.EqualTo(1.0));
      Assert.That(b.LeverageRatio.Max, Is.EqualTo(10.0));
      Assert.That(b.Strike.Min, Is.EqualTo(100.0));
      Assert.That(b.CallPrice.Max, Is.EqualTo(5.0));
    }

    // ---------------------------------------------------------------- Option

    [Test]
    public void OptionExpirationResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""count"":12,
        ""dates"":[""2026-01-16"",""2026-02-20""],""timestamps"":[1700000000,1700000001],
        ""periodTags"":[""monthly"",""weekly""]}]}";
      var resp = JsonConvert.DeserializeObject<OptionExpirationResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      var e = resp.Data[0];
      Assert.That(e.Symbol, Is.EqualTo("AAPL"));
      Assert.That(e.Count, Is.EqualTo(12));
      Assert.That(e.Dates.Count, Is.EqualTo(2));
      Assert.That(e.Dates[0], Is.EqualTo("2026-01-16"));
      Assert.That(e.Timestamps[1], Is.EqualTo(1700000001));
      Assert.That(e.PeriodTags[1], Is.EqualTo("weekly"));
    }

    [Test]
    public void OptionBriefResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":[{""identifier"":""AAPL20260116C00150000"",
        ""symbol"":""AAPL"",""strike"":""150"",""right"":""CALL"",""expiry"":1700000000,
        ""askPrice"":2.5,""askSize"":100,""bidPrice"":2.4,""bidSize"":200,
        ""latestPrice"":2.45,""preClose"":2.3,""volume"":1000,""high"":2.6,
        ""low"":2.2,""open"":2.35,""openInterest"":500,""change"":0.15,
        ""multiplier"":100,""volatility"":""0.25"",""ratesBonds"":0.05,
        ""timestamp"":1700000000}]}";
      var resp = JsonConvert.DeserializeObject<OptionBriefResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var b = resp.Data[0];
      Assert.That(b.Identifier, Is.EqualTo("AAPL20260116C00150000"));
      Assert.That(b.Symbol, Is.EqualTo("AAPL"));
      Assert.That(b.Strike, Is.EqualTo("150"));
      Assert.That(b.Right, Is.EqualTo("CALL"));
      Assert.That(b.Expiry, Is.EqualTo(1700000000));
      Assert.That(b.AskPrice, Is.EqualTo(2.5));
      Assert.That(b.AskSize, Is.EqualTo(100));
      Assert.That(b.BidPrice, Is.EqualTo(2.4));
      Assert.That(b.BidSize, Is.EqualTo(200));
      Assert.That(b.LatestPrice, Is.EqualTo(2.45));
      Assert.That(b.PreClose, Is.EqualTo(2.3));
      Assert.That(b.Volume, Is.EqualTo(1000));
      Assert.That(b.High, Is.EqualTo(2.6));
      Assert.That(b.Low, Is.EqualTo(2.2));
      Assert.That(b.Open, Is.EqualTo(2.35));
      Assert.That(b.OpenInterest, Is.EqualTo(500));
      Assert.That(b.Change, Is.EqualTo(0.15));
      Assert.That(b.Multiplier, Is.EqualTo(100));
      Assert.That(b.Volatility, Is.EqualTo("0.25"));
      Assert.That(b.RatesBonds, Is.EqualTo(0.05));
      Assert.That(b.Timestamp, Is.EqualTo(1700000000));
    }

    [Test]
    public void OptionChainResponse_Deserialize_WithPutCall()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""expiry"":1700000000,
        ""items"":[{""put"":{""identifier"":""P1"",""strike"":""150"",""right"":""PUT"",
        ""askPrice"":1.5,""askSize"":10,""bidPrice"":1.4,""bidSize"":20,
        ""latestPrice"":1.45,""preClose"":1.3,""volume"":100,""openInterest"":50,
        ""multiplier"":100,""lastTimestamp"":1700000000,""impliedVol"":0.2,
        ""delta"":-0.5,""gamma"":0.01,""theta"":-0.05,""vega"":0.1,""rho"":0.02,
        ""markPrice"":1.46,""preMarkPrice"":1.31,""markTimestamp"":1700000100,
        ""midPrice"":1.455,""preMidPrice"":1.305,""midTimestamp"":1700000100},
        ""call"":{""identifier"":""C1"",""strike"":""150"",""right"":""CALL"",
        ""askPrice"":2.5,""askSize"":30,""bidPrice"":2.4,""bidSize"":40,
        ""latestPrice"":2.45,""preClose"":2.3,""volume"":200,""openInterest"":60,
        ""multiplier"":100,""lastTimestamp"":1700000001,""impliedVol"":0.25,
        ""delta"":0.5,""gamma"":0.02,""theta"":-0.06,""vega"":0.12,""rho"":0.03,
        ""markPrice"":2.46,""preMarkPrice"":2.31,""markTimestamp"":1700000101,
        ""midPrice"":2.455,""preMidPrice"":2.305,""midTimestamp"":1700000101}}]}]}";
      var resp = JsonConvert.DeserializeObject<OptionChainResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var c = resp.Data[0];
      Assert.That(c.Symbol, Is.EqualTo("AAPL"));
      Assert.That(c.Expiry, Is.EqualTo(1700000000));
      Assert.That(c.Items.Count, Is.EqualTo(1));
      var put = c.Items[0].Put;
      var call = c.Items[0].Call;
      Assert.That(put.Identifier, Is.EqualTo("P1"));
      Assert.That(put.Right, Is.EqualTo("PUT"));
      Assert.That(put.Delta, Is.EqualTo(-0.5));
      Assert.That(put.Gamma, Is.EqualTo(0.01));
      Assert.That(put.Theta, Is.EqualTo(-0.05));
      Assert.That(put.Vega, Is.EqualTo(0.1));
      Assert.That(put.Rho, Is.EqualTo(0.02));
      Assert.That(put.ImpliedVol, Is.EqualTo(0.2));
      Assert.That(put.MarkPrice, Is.EqualTo(1.46));
      Assert.That(put.PreMarkPrice, Is.EqualTo(1.31));
      Assert.That(put.MarkTimestamp, Is.EqualTo(1700000100));
      Assert.That(put.MidPrice, Is.EqualTo(1.455));
      Assert.That(put.PreMidPrice, Is.EqualTo(1.305));
      Assert.That(put.MidTimestamp, Is.EqualTo(1700000100));
      Assert.That(call.Identifier, Is.EqualTo("C1"));
      Assert.That(call.Delta, Is.EqualTo(0.5));
      Assert.That(call.MarkPrice, Is.EqualTo(2.46));
      Assert.That(call.MidPrice, Is.EqualTo(2.455));
    }

    [Test]
    public void OptionDepthResponse_Deserialize_WithAskBidBooks()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""expiry"":1700000000,
        ""strike"":""150"",""right"":""CALL"",""timestamp"":1700000000,
        ""ask"":[{""price"":2.5,""code"":""A"",""timestamp"":1700000000,""volume"":100,""count"":5}],
        ""bid"":[{""price"":2.4,""code"":""B"",""timestamp"":1700000001,""volume"":200,""count"":3}]}]}";
      var resp = JsonConvert.DeserializeObject<OptionDepthResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var d = resp.Data[0];
      Assert.That(d.Symbol, Is.EqualTo("AAPL"));
      Assert.That(d.Expiry, Is.EqualTo(1700000000));
      Assert.That(d.Strike, Is.EqualTo("150"));
      Assert.That(d.Right, Is.EqualTo("CALL"));
      Assert.That(d.Timestamp, Is.EqualTo(1700000000));
      Assert.That(d.Ask.Count, Is.EqualTo(1));
      Assert.That(d.Ask[0].Price, Is.EqualTo(2.5));
      Assert.That(d.Ask[0].Code, Is.EqualTo("A"));
      Assert.That(d.Ask[0].Volume, Is.EqualTo(100));
      Assert.That(d.Ask[0].Count, Is.EqualTo(5));
      Assert.That(d.Bid[0].Price, Is.EqualTo(2.4));
      Assert.That(d.Bid[0].Volume, Is.EqualTo(200));
    }

    [Test]
    public void OptionSymbolResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""name"":""Apple Inc"",
        ""underlyingSymbol"":""AAPL""}]}";
      var resp = JsonConvert.DeserializeObject<OptionSymbolResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var s = resp.Data[0];
      Assert.That(s.Symbol, Is.EqualTo("AAPL"));
      Assert.That(s.Name, Is.EqualTo("Apple Inc"));
      Assert.That(s.UnderlyingSymbol, Is.EqualTo("AAPL"));
    }

    [Test]
    public void OptionTradeTickResponse_Deserialize_WithTickPoints()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""expiry"":1700000000,
        ""strike"":""150"",""right"":""CALL"",""items"":[{""price"":2.5,""time"":1700000000,
        ""volume"":100}]}]}";
      var resp = JsonConvert.DeserializeObject<OptionTradeTickResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var t = resp.Data[0];
      Assert.That(t.Symbol, Is.EqualTo("AAPL"));
      Assert.That(t.Expiry, Is.EqualTo(1700000000));
      Assert.That(t.Strike, Is.EqualTo("150"));
      Assert.That(t.Right, Is.EqualTo("CALL"));
      Assert.That(t.Items.Count, Is.EqualTo(1));
      Assert.That(t.Items[0].Price, Is.EqualTo(2.5));
      Assert.That(t.Items[0].Time, Is.EqualTo(1700000000));
      Assert.That(t.Items[0].Volume, Is.EqualTo(100));
    }

    [Test]
    public void OptionKlineResponse_Deserialize_WithKlinePoints()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""strike"":""150"",
        ""right"":""CALL"",""expiry"":1700000000,""period"":""day"",
        ""items"":[{""open"":2.0,""high"":2.5,""low"":1.9,""close"":2.3,
        ""volume"":1000,""amount"":2300.0,""time"":1700000000,""openInterest"":500}]}]}";
      var resp = JsonConvert.DeserializeObject<OptionKlineResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var k = resp.Data[0];
      Assert.That(k.Symbol, Is.EqualTo("AAPL"));
      Assert.That(k.Strike, Is.EqualTo("150"));
      Assert.That(k.Right, Is.EqualTo("CALL"));
      Assert.That(k.Expiry, Is.EqualTo(1700000000));
      Assert.That(k.Period, Is.EqualTo("day"));
      Assert.That(k.Items.Count, Is.EqualTo(1));
      var p = k.Items[0];
      Assert.That(p.Open, Is.EqualTo(2.0));
      Assert.That(p.High, Is.EqualTo(2.5));
      Assert.That(p.Low, Is.EqualTo(1.9));
      Assert.That(p.Close, Is.EqualTo(2.3));
      Assert.That(p.Volume, Is.EqualTo(1000));
      Assert.That(p.Amount, Is.EqualTo(2300.0));
      Assert.That(p.Time, Is.EqualTo(1700000000));
      Assert.That(p.OpenInterest, Is.EqualTo(500));
    }

    [Test]
    public void OptionAnalysisResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""impliedVol30Days"":0.25,
        ""hisVolatility"":0.20,""ivHisVRatio"":1.25,""callPutRatio"":1.5,
        ""impliedVolMetric"":{""period"":""52week"",""percentile"":0.75,""rank"":0.80},
        ""volatilityList"":[]}]}";
      var resp = JsonConvert.DeserializeObject<OptionAnalysisResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var a = resp.Data[0];
      Assert.That(a.Symbol, Is.EqualTo("AAPL"));
      Assert.That(a.ImpliedVol30Days, Is.EqualTo(0.25));
      Assert.That(a.HisVolatility, Is.EqualTo(0.20));
      Assert.That(a.IvHisVRatio, Is.EqualTo(1.25));
      Assert.That(a.CallPutRatio, Is.EqualTo(1.5));
      Assert.That(a.ImpliedVolMetric, Is.Not.Null);
      Assert.That(a.ImpliedVolMetric.Period, Is.EqualTo("52week"));
      Assert.That(a.ImpliedVolMetric.Percentile, Is.EqualTo(0.75));
      Assert.That(a.VolatilityList.Count, Is.EqualTo(0));
    }

    // ---------------------------------------------------------------- Future

    [Test]
    public void FutureRealTimeQuoteResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":[{""contractCode"":""CL2026JAN"",
        ""latestPrice"":75.5,""latestSize"":10,""latestTime"":1700000000,
        ""bidPrice"":75.4,""bidSize"":5,""askPrice"":75.6,""askSize"":8,
        ""openInterest"":1000,""volume"":5000,""open"":75.0,""high"":76.0,
        ""low"":74.5,""settlement"":75.2,""limitUp"":80.0,""limitDown"":70.0}]}";
      var resp = JsonConvert.DeserializeObject<FutureRealTimeQuoteResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var f = resp.Data[0];
      Assert.That(f.ContractCode, Is.EqualTo("CL2026JAN"));
      Assert.That(f.LatestPrice, Is.EqualTo(75.5m));
      Assert.That(f.LatestSize, Is.EqualTo(10));
      Assert.That(f.LatestTime, Is.EqualTo(1700000000));
      Assert.That(f.BidPrice, Is.EqualTo(75.4m));
      Assert.That(f.BidSize, Is.EqualTo(5));
      Assert.That(f.AskPrice, Is.EqualTo(75.6m));
      Assert.That(f.AskSize, Is.EqualTo(8));
      Assert.That(f.OpenInterest, Is.EqualTo(1000));
      Assert.That(f.Volume, Is.EqualTo(5000));
      Assert.That(f.Open, Is.EqualTo(75.0m));
      Assert.That(f.High, Is.EqualTo(76.0m));
      Assert.That(f.Low, Is.EqualTo(74.5m));
      Assert.That(f.Settlement, Is.EqualTo(75.2m));
      Assert.That(f.LimitUp, Is.EqualTo(80.0m));
      Assert.That(f.LimitDown, Is.EqualTo(70.0m));
    }

    [Test]
    public void FutureKlineResponse_Deserialize_WithBatchAndPoints()
    {
      string json = @"{""code"":0,""data"":[{""contractCode"":""CL2026JAN"",
        ""nextPageToken"":""tok123"",
        ""items"":[{""time"":1700000000,""lastTime"":1700000001,""open"":75.0,
        ""close"":75.5,""high"":76.0,""low"":74.5,""volume"":1000,
        ""openInterest"":500,""settlement"":75.2}]}]}";
      var resp = JsonConvert.DeserializeObject<FutureKlineResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var batch = resp.Data[0];
      Assert.That(batch.ContractCode, Is.EqualTo("CL2026JAN"));
      Assert.That(batch.NextPageToken, Is.EqualTo("tok123"));
      Assert.That(batch.Items.Count, Is.EqualTo(1));
      var k = batch.Items[0];
      Assert.That(k.Time, Is.EqualTo(1700000000));
      Assert.That(k.LastTime, Is.EqualTo(1700000001));
      Assert.That(k.Open, Is.EqualTo(75.0m));
      Assert.That(k.Close, Is.EqualTo(75.5m));
      Assert.That(k.High, Is.EqualTo(76.0m));
      Assert.That(k.Low, Is.EqualTo(74.5m));
      Assert.That(k.Volume, Is.EqualTo(1000));
      Assert.That(k.OpenInterest, Is.EqualTo(500));
      Assert.That(k.Settlement, Is.EqualTo(75.2m));
    }

    [Test]
    public void FutureTickResponse_Deserialize_WithBatchAndTicks()
    {
      string json = @"{""code"":0,""data"":{""contractCode"":""CL2026JAN"",
        ""items"":[{""index"":1,""price"":75.5,""volume"":10,""time"":1700000000}]}}";
      var resp = JsonConvert.DeserializeObject<FutureTickResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.ContractCode, Is.EqualTo("CL2026JAN"));
      Assert.That(resp.Data.Items.Count, Is.EqualTo(1));
      var t = resp.Data.Items[0];
      Assert.That(t.Index, Is.EqualTo(1));
      Assert.That(t.Price, Is.EqualTo(75.5m));
      Assert.That(t.Volume, Is.EqualTo(10));
      Assert.That(t.Time, Is.EqualTo(1700000000));
    }

    [Test]
    public void FutureDepthResponse_Deserialize_WithAskBidItems()
    {
      string json = @"{""code"":0,""data"":[{""contractId"":""CL001"",""contractCode"":""CL2026JAN"",
        ""ask"":[{""price"":75.6,""volume"":10}],""bid"":[{""price"":75.4,""volume"":8}]}]}";
      var resp = JsonConvert.DeserializeObject<FutureDepthResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var d = resp.Data[0];
      Assert.That(d.ContractId, Is.EqualTo("CL001"));
      Assert.That(d.ContractCode, Is.EqualTo("CL2026JAN"));
      Assert.That(d.Ask.Count, Is.EqualTo(1));
      Assert.That(d.Ask[0].Price, Is.EqualTo(75.6m));
      Assert.That(d.Ask[0].Volume, Is.EqualTo(10));
      Assert.That(d.Bid[0].Price, Is.EqualTo(75.4m));
      Assert.That(d.Bid[0].Volume, Is.EqualTo(8));
    }

    [Test]
    public void FutureExchangeResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""code"":""CME"",""name"":""Chicago Mercantile Exchange"",
        ""zoneId"":""America/Chicago""}]}";
      var resp = JsonConvert.DeserializeObject<FutureExchangeResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var e = resp.Data[0];
      Assert.That(e.Code, Is.EqualTo("CME"));
      Assert.That(e.Name, Is.EqualTo("Chicago Mercantile Exchange"));
      Assert.That(e.ZoneId, Is.EqualTo("America/Chicago"));
    }

    [Test]
    public void FutureContractResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":{""type"":""FUT"",""name"":""Crude Oil"",
        ""ibCode"":""CL"",""contractCode"":""CL2026JAN"",""contractMonth"":""202601"",
        ""exchangeCode"":""NYMEX"",""exchange"":""NYMEX"",""multiplier"":1000.0,
        ""minTick"":0.01,""lastTradingDate"":""2026-01-20"",""firstNoticeDate"":""2026-01-15"",
        ""lastBiddingCloseTime"":1700000000,""currency"":""USD"",""continuous"":true,
        ""trade"":true}}";
      var resp = JsonConvert.DeserializeObject<FutureContractResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var c = resp.Data;
      Assert.That(c.Type, Is.EqualTo("FUT"));
      Assert.That(c.Name, Is.EqualTo("Crude Oil"));
      Assert.That(c.IbCode, Is.EqualTo("CL"));
      Assert.That(c.ContractCode, Is.EqualTo("CL2026JAN"));
      Assert.That(c.ContractMonth, Is.EqualTo("202601"));
      Assert.That(c.ExchangeCode, Is.EqualTo("NYMEX"));
      Assert.That(c.Exchange, Is.EqualTo("NYMEX"));
      Assert.That(c.Multiplier, Is.EqualTo(1000.0m));
      Assert.That(c.MinTick, Is.EqualTo(0.01m));
      Assert.That(c.LastTradingDate, Is.EqualTo("2026-01-20"));
      Assert.That(c.FirstNoticeDate, Is.EqualTo("2026-01-15"));
      Assert.That(c.LastBiddingCloseTime, Is.EqualTo(1700000000));
      Assert.That(c.Currency, Is.EqualTo("USD"));
      Assert.That(c.Continuous, Is.True);
      Assert.That(c.Trade, Is.True);
    }

    [Test]
    public void FutureContractsResponse_Deserialize_List()
    {
      string json = @"{""code"":0,""data"":[{""type"":""FUT"",""name"":""Crude Oil"",""ibCode"":""CL"",
        ""contractCode"":""CL2026JAN"",""contractMonth"":""202601"",""exchangeCode"":""NYMEX"",
        ""exchange"":""NYMEX"",""multiplier"":1000.0,""minTick"":0.01,""lastTradingDate"":""2026-01-20"",
        ""firstNoticeDate"":""2026-01-15"",""lastBiddingCloseTime"":1700000000,
        ""currency"":""USD"",""continuous"":true,""trade"":true}]}";
      var resp = JsonConvert.DeserializeObject<FutureContractsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      Assert.That(resp.Data[0].ContractCode, Is.EqualTo("CL2026JAN"));
    }

    [Test]
    public void FutureTradingDateResponse_Deserialize_WithTimeSections()
    {
      string json = @"{""code"":0,""data"":{""biddingTimes"":[{""start"":1700000000,""end"":1700003600}],
        ""tradingTimes"":[{""start"":1700000000,""end"":1700036000}],""timeSection"":""RTH""}}";
      var resp = JsonConvert.DeserializeObject<FutureTradingDateResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.TimeSection, Is.EqualTo("RTH"));
    }

    // ---------------------------------------------------------------- Misc Quote

    [Test]
    public void MarketStateResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""market"":""US"",""marketStatus"":""OPEN"",
        ""status"":""NORMAL"",""openTime"":""09:30""}]}";
      var resp = JsonConvert.DeserializeObject<MarketStateResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var m = resp.Data[0];
      Assert.That(m.Market, Is.EqualTo("US"));
      Assert.That(m.MarketStatus, Is.EqualTo("OPEN"));
      Assert.That(m.Status, Is.EqualTo("NORMAL"));
      Assert.That(m.OpenTime, Is.EqualTo("09:30"));
    }

    [Test]
    public void QuoteStockFundamentalResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":{""items"":[{""symbol"":""AAPL"",""roe"":0.15,
        ""roa"":0.10,""pbRate"":3.5,""psRate"":2.0,""divideRate"":0.01,
        ""week52High"":200.0,""week52Low"":150.0,""ttmEps"":5.0,""lyrEps"":4.5,
        ""volumeRatio"":1.2,""turnoverRate"":0.05,""ttmPeRate"":25.0,
        ""lyrPeRate"":24.0,""marketCap"":3000000.0,""floatMarketCap"":2500000.0}]}}";
      var resp = JsonConvert.DeserializeObject<QuoteStockFundamentalResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Items.Count, Is.EqualTo(1));
      var f = resp.Data.Items[0];
      Assert.That(f.Symbol, Is.EqualTo("AAPL"));
      Assert.That(f.Roe, Is.EqualTo(0.15));
      Assert.That(f.Roa, Is.EqualTo(0.10));
      Assert.That(f.PbRate, Is.EqualTo(3.5));
      Assert.That(f.PsRate, Is.EqualTo(2.0));
      Assert.That(f.DivideRate, Is.EqualTo(0.01));
      Assert.That(f.Week52High, Is.EqualTo(200.0));
      Assert.That(f.Week52Low, Is.EqualTo(150.0));
      Assert.That(f.TtmEps, Is.EqualTo(5.0));
      Assert.That(f.LyrEps, Is.EqualTo(4.5));
      Assert.That(f.VolumeRatio, Is.EqualTo(1.2));
      Assert.That(f.TurnoverRate, Is.EqualTo(0.05));
      Assert.That(f.TtmPeRate, Is.EqualTo(25.0));
      Assert.That(f.LyrPeRate, Is.EqualTo(24.0));
      Assert.That(f.MarketCap, Is.EqualTo(3000000.0));
      Assert.That(f.FloatMarketCap, Is.EqualTo(2500000.0));
    }

    [Test]
    public void TradeCalendarResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""date"":""2026-01-02"",""type"":""NORMAL""},
        {""date"":""2026-01-15"",""type"":""EARLY_CLOSE""}]}";
      var resp = JsonConvert.DeserializeObject<TradeCalendarResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(2));
      Assert.That(resp.Data[0].Date, Is.EqualTo("2026-01-02"));
      Assert.That(resp.Data[0].Type, Is.EqualTo("NORMAL"));
      Assert.That(resp.Data[1].Type, Is.EqualTo("EARLY_CLOSE"));
    }

    [Test]
    public void SymbolNameResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""name"":""Apple Inc""}]}";
      var resp = JsonConvert.DeserializeObject<SymbolNameResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data[0].Symbol, Is.EqualTo("AAPL"));
      Assert.That(resp.Data[0].Name, Is.EqualTo("Apple Inc"));
    }

    // ---------------------------------------------------------------- SymbolsResponse

    /// <summary>
    /// all_symbols and fund_all_symbols share the same wire shape:
    /// data is a plain string array. Regular tickers, index symbols
    /// (leading dot) and fund symbols (ISIN.CCY) must all deserialize.
    /// </summary>
    [Test]
    public void SymbolsResponse_Deserialize_MixedSymbolFormats()
    {
      string json = @"{""code"":0,""data"":[""AAPL"","".DJI"",""IE00B11XZ988.USD""]}";
      var resp = JsonConvert.DeserializeObject<SymbolsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(3));
      Assert.That(resp.Data[0], Is.EqualTo("AAPL"));
      Assert.That(resp.Data[1], Is.EqualTo(".DJI"), "index symbols must deserialize");
      Assert.That(resp.Data[2], Is.EqualTo("IE00B11XZ988.USD"), "fund symbols must deserialize");
    }

    // ---------------------------------------------------------------- StockDetailResponse

    /// <summary>
    /// stock_detail returns data as an object ({"items":[...]}) rather
    /// than a top-level array like quote_real_time. Verifies the wrapper
    /// deserializes and that snapshot + fundamental + sub-object fields
    /// all map correctly. Also confirms that all optional fields tolerate
    /// missing wire keys without crashing.
    /// </summary>
    [Test]
    public void StockDetailResponse_Deserialize_AllFieldGroupsMapped()
    {
      string json = @"{""code"":0,""data"":{""items"":[{
        ""symbol"":""AAPL"",""market"":""US"",""secType"":""STK"",""exchange"":""NASDAQ"",
        ""name"":""Apple Inc"",""shortable"":true,""askPrice"":200.5,""askSize"":100,
        ""bidPrice"":200.4,""bidSize"":200,""preClose"":199.9,""latestPrice"":200.45,
        ""latestTime"":""08-14 16:00:00 EDT"",""volume"":1000000,""open"":199.0,""high"":201.0,
        ""low"":198.5,""change"":0.55,""amount"":200000000.0,""amplitude"":0.0125,
        ""marketStatus"":""Trading"",""tradingStatus"":2,""floatShares"":15000000000,
        ""shares"":15500000000,""eps"":6.5,""adrRate"":1.0,""etf"":0,
        ""listingDate"":312825600000,""halted"":0.0,""delay"":0,
        ""hourTrading"":{""tag"":""Pre-Mkt"",""latestPrice"":200.6,""preClose"":199.9,""volume"":1000},
        ""nextMarketStatus"":{""tag"":""TRADING"",""beginTime"":1700000000000}
      }]}}";
      var resp = JsonConvert.DeserializeObject<StockDetailResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data, Is.Not.Null);
      Assert.That(resp.Data.Items.Count, Is.EqualTo(1));
      var it = resp.Data.Items[0];
      Assert.That(it.Symbol, Is.EqualTo("AAPL"));
      Assert.That(it.SecType, Is.EqualTo("STK"));
      Assert.That(it.LatestPrice, Is.EqualTo(200.45));
      Assert.That(it.LatestTime, Is.EqualTo("08-14 16:00:00 EDT"),
        "stock_detail returns latestTime as a formatted string, not epoch-ms");
      Assert.That(it.Shares, Is.EqualTo(15500000000L));
      Assert.That(it.Etf, Is.EqualTo(0));
      Assert.That(it.HourTrading, Is.Not.Null);
      Assert.That(it.HourTrading.LatestPrice, Is.EqualTo(200.6));
      Assert.That(it.NextMarketStatus, Is.Not.Null);
      // Sub-objects not present in the payload stay null.
      Assert.That(it.StockSplit, Is.Null);
      Assert.That(it.StockNotice, Is.Null);
    }

    /// <summary>
    /// Minimal payload — only the required identity fields. All optional
    /// fundamentals and sub-objects must deserialize as null / default.
    /// </summary>
    [Test]
    public void StockDetailResponse_Deserialize_MinimalPayload()
    {
      string json = @"{""code"":0,""data"":{""items"":[{""symbol"":""AAPL"",""secType"":""STK"",""name"":""Apple""}]}}";
      var resp = JsonConvert.DeserializeObject<StockDetailResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Items[0].Symbol, Is.EqualTo("AAPL"));
      Assert.That(resp.Data.Items[0].LatestPrice, Is.Null);
      Assert.That(resp.Data.Items[0].HourTrading, Is.Null);
    }

    // ---------------------------------------------------------------- HourTradingTimelineResponse

    /// <summary>
    /// hour_trading_timeline wraps a preClose scalar, an optional detail
    /// object (extended-hours snapshot), and an items array of tick points.
    /// </summary>
    [Test]
    public void HourTradingTimelineResponse_Deserialize_DetailAndItems()
    {
      string json = @"{""code"":0,""data"":{
        ""preClose"":224.05,
        ""detail"":{""tag"":""Pre-Mkt"",""latestPrice"":224.2,""preClose"":224.05,""volume"":5000,""timestamp"":1700000000000},
        ""items"":[
          {""time"":1700000000000,""price"":224.1,""avgPrice"":224.05,""volume"":100},
          {""time"":1700000060000,""price"":224.2,""avgPrice"":224.1,""volume"":150}
        ]
      }}";
      var resp = JsonConvert.DeserializeObject<HourTradingTimelineResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.PreClose, Is.EqualTo(224.05));
      Assert.That(resp.Data.Detail, Is.Not.Null);
      Assert.That(resp.Data.Detail.LatestPrice, Is.EqualTo(224.2));
      Assert.That(resp.Data.Items.Count, Is.EqualTo(2));
      Assert.That(resp.Data.Items[0].Price, Is.EqualTo(224.1));
      Assert.That(resp.Data.Items[1].AvgPrice, Is.EqualTo(224.1));
    }

    // ---------------------------------------------------------------- FinancialDailyResponse

    /// <summary>
    /// financial_daily returns data as an array of {symbol, date, field, value}
    /// rows. date is epoch millis; value is numeric.
    /// </summary>
    [Test]
    public void FinancialDailyResponse_Deserialize_TimeSeriesRows()
    {
      string json = @"{""code"":0,""data"":[
        {""symbol"":""AAPL"",""date"":1704067200000,""field"":""open_price"",""value"":185.5},
        {""symbol"":""AAPL"",""date"":1704153600000,""field"":""open_price"",""value"":186.2}
      ]}";
      var resp = JsonConvert.DeserializeObject<FinancialDailyResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(2));
      Assert.That(resp.Data[0].Symbol, Is.EqualTo("AAPL"));
      Assert.That(resp.Data[0].Field, Is.EqualTo("open_price"));
      Assert.That(resp.Data[0].Value, Is.EqualTo(185.5));
      Assert.That(resp.Data[0].Date, Is.EqualTo(1704067200000L));
    }

    // ---------------------------------------------------------------- FinancialReportResponse

    /// <summary>
    /// financial_report rows carry filing metadata (currency, filingDate,
    /// periodEndDate) alongside a string-valued field/value pair. Value is
    /// a wire string because currencies and other markers can be non-numeric.
    /// </summary>
    [Test]
    public void FinancialReportResponse_Deserialize_FilingRows()
    {
      string json = @"{""code"":0,""data"":[{
        ""symbol"":""AAPL"",""currency"":""USD"",""field"":""total_revenue"",
        ""value"":""94836000000"",""filingDate"":""2024-11-01"",""periodEndDate"":""2024-09-30""
      }]}";
      var resp = JsonConvert.DeserializeObject<FinancialReportResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      var r = resp.Data[0];
      Assert.That(r.Symbol, Is.EqualTo("AAPL"));
      Assert.That(r.Currency, Is.EqualTo("USD"));
      Assert.That(r.Field, Is.EqualTo("total_revenue"));
      Assert.That(r.Value, Is.EqualTo("94836000000"));
      Assert.That(r.FilingDate, Is.EqualTo("2024-11-01"));
      Assert.That(r.PeriodEndDate, Is.EqualTo("2024-09-30"));
    }

    // ---------------------------------------------------------------- Trade: Position Transfer

    [Test]
    public void PositionTransferResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":{""id"":100,""accountId"":""U123"",
        ""counterpartyAccountId"":""U456"",""method"":""ACATS"",""direction"":""OUTGOING"",
        ""status"":""PENDING"",""memo"":""transfer"",""userId"":789,""userName"":""user"",
        ""finishedAt"":1700000000,""updatedAt"":1700000001,""createdAt"":1700000002}}";
      var resp = JsonConvert.DeserializeObject<PositionTransferResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var t = resp.Data;
      Assert.That(t.Id, Is.EqualTo(100));
      Assert.That(t.AccountId, Is.EqualTo("U123"));
      Assert.That(t.CounterpartyAccountId, Is.EqualTo("U456"));
      Assert.That(t.Method, Is.EqualTo("ACATS"));
      Assert.That(t.Direction, Is.EqualTo("OUTGOING"));
      Assert.That(t.Status, Is.EqualTo("PENDING"));
      Assert.That(t.Memo, Is.EqualTo("transfer"));
      Assert.That(t.UserId, Is.EqualTo(789));
      Assert.That(t.UserName, Is.EqualTo("user"));
      Assert.That(t.FinishedAt, Is.EqualTo(1700000000));
      Assert.That(t.UpdatedAt, Is.EqualTo(1700000001));
      Assert.That(t.CreatedAt, Is.EqualTo(1700000002));
    }

    [Test]
    public void PositionTransferRecordsResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""id"":1,""accountId"":""U123"",
        ""counterpartyAccountId"":""U456"",""method"":""ACATS"",""direction"":""OUTGOING"",
        ""status"":""DONE"",""memo"":""m"",""userId"":1,""userName"":""u"",
        ""finishedAt"":1700000000,""updatedAt"":1700000001,""createdAt"":1700000002}]}";
      var resp = JsonConvert.DeserializeObject<PositionTransferRecordsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      Assert.That(resp.Data[0].Id, Is.EqualTo(1));
      Assert.That(resp.Data[0].Method, Is.EqualTo("ACATS"));
    }

    [Test]
    public void PositionTransferExternalRecordsResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":[{""id"":200,""status"":""PENDING"",
        ""allFinished"":false,""counterpartyContacted"":true,""accountId"":""U123"",
        ""transferMethod"":""ACATS"",""institutionName"":""Firm"",""institutionType"":""BROKER"",
        ""remoteClearingBroker"":""DTC"",""dtcNumber"":""001"",""remoteUserName"":""ruser"",
        ""remoteAccount"":""R001"",""contactName"":""John"",""contactEmail"":""j@x.com"",
        ""contactPhone"":""555-0100"",""cancelable"":true,""side"":""OUT"",""market"":""US"",
        ""userName"":""user"",""transferHin"":""HIN001"",""fullPortfolio"":false,
        ""createdAt"":1700000000,""updatedAt"":1700000001,""transferPropertyInfos"":[]}]}";
      var resp = JsonConvert.DeserializeObject<PositionTransferExternalRecordsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      var e = resp.Data[0];
      Assert.That(e.Id, Is.EqualTo(200));
      Assert.That(e.Status, Is.EqualTo("PENDING"));
      Assert.That(e.AllFinished, Is.False);
      Assert.That(e.CounterpartyContacted, Is.True);
      Assert.That(e.AccountId, Is.EqualTo("U123"));
      Assert.That(e.TransferMethod, Is.EqualTo("ACATS"));
      Assert.That(e.InstitutionName, Is.EqualTo("Firm"));
      Assert.That(e.InstitutionType, Is.EqualTo("BROKER"));
      Assert.That(e.RemoteClearingBroker, Is.EqualTo("DTC"));
      Assert.That(e.DtcNumber, Is.EqualTo("001"));
      Assert.That(e.RemoteUserName, Is.EqualTo("ruser"));
      Assert.That(e.RemoteAccount, Is.EqualTo("R001"));
      Assert.That(e.ContactName, Is.EqualTo("John"));
      Assert.That(e.ContactEmail, Is.EqualTo("j@x.com"));
      Assert.That(e.ContactPhone, Is.EqualTo("555-0100"));
      Assert.That(e.Cancelable, Is.True);
      Assert.That(e.Side, Is.EqualTo("OUT"));
      Assert.That(e.Market, Is.EqualTo("US"));
      Assert.That(e.UserName, Is.EqualTo("user"));
      Assert.That(e.TransferHin, Is.EqualTo("HIN001"));
      Assert.That(e.FullPortfolio, Is.False);
      Assert.That(e.CreatedAt, Is.EqualTo(1700000000));
      Assert.That(e.UpdatedAt, Is.EqualTo(1700000001));
      Assert.That(e.TransferPropertyInfos.Count, Is.EqualTo(0));
    }

    [Test]
    public void PositionTransferDetailResponse_Deserialize_WithStockDetail()
    {
      string json = @"{""code"":0,""data"":{""id"":300,""accountId"":""U123"",
        ""counterpartyAccountId"":""U456"",""method"":""ACATS"",""direction"":""OUT"",
        ""status"":""DONE"",""memo"":""m"",""userId"":1,""userName"":""u"",
        ""finishedAt"":1700000000,""updatedAt"":1700000001,""createdAt"":1700000002,
        ""detail"":[{""transferId"":300,""direction"":""OUT"",""symbol"":""AAPL"",
        ""formattedSymbol"":""AAPL"",""market"":""US"",""quantity"":100,
        ""status"":""DONE"",""message"":""ok"",""updatedAt"":1700000003,
        ""createdAt"":1700000004}]}}";
      var resp = JsonConvert.DeserializeObject<PositionTransferDetailResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var d = resp.Data;
      Assert.That(d.Id, Is.EqualTo(300));
      Assert.That(d.Detail.Count, Is.EqualTo(1));
      var s = d.Detail[0];
      Assert.That(s.TransferId, Is.EqualTo(300));
      Assert.That(s.Direction, Is.EqualTo("OUT"));
      Assert.That(s.Symbol, Is.EqualTo("AAPL"));
      Assert.That(s.FormattedSymbol, Is.EqualTo("AAPL"));
      Assert.That(s.Market, Is.EqualTo("US"));
      Assert.That(s.Quantity, Is.EqualTo(100));
      Assert.That(s.Status, Is.EqualTo("DONE"));
      Assert.That(s.Message, Is.EqualTo("ok"));
      Assert.That(s.UpdatedAt, Is.EqualTo(1700000003));
      Assert.That(s.CreatedAt, Is.EqualTo(1700000004));
    }

    // ---------------------------------------------------------------- Trade: Assets

    [Test]
    public void AggregateAssetResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":{""currency"":""USD"",""cashBalance"":10000.0,
        ""cashBalanceWithInTransit"":10500.0,""equityWithLoan"":20000.0,
        ""netLiquidation"":25000.0,""initMargin"":5000.0,""maintainMargin"":4000.0,
        ""tradeCurrencyMargin"":3000.0,""intradayRiskRatio"":0.2,
        ""grossPositionValue"":15000.0,""optionMarketValue"":1000.0,
        ""stockMarketValue"":12000.0,""cashAvailableForTrade"":8000.0,
        ""availableCash"":9000.0,""lockedFunds"":500.0,""lockedCash"":300.0,
        ""creditLimit"":50000.0,""excessEquity"":10000.0,""excessLiquidity"":12000.0}}";
      var resp = JsonConvert.DeserializeObject<AggregateAssetResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var a = resp.Data;
      Assert.That(a.Currency, Is.EqualTo("USD"));
      Assert.That(a.CashBalance, Is.EqualTo(10000.0));
      Assert.That(a.CashBalanceWithInTransit, Is.EqualTo(10500.0));
      Assert.That(a.EquityWithLoan, Is.EqualTo(20000.0));
      Assert.That(a.NetLiquidation, Is.EqualTo(25000.0));
      Assert.That(a.InitMargin, Is.EqualTo(5000.0));
      Assert.That(a.MaintainMargin, Is.EqualTo(4000.0));
      Assert.That(a.TradeCurrencyMargin, Is.EqualTo(3000.0));
      Assert.That(a.IntradayRiskRatio, Is.EqualTo(0.2));
      Assert.That(a.GrossPositionValue, Is.EqualTo(15000.0));
      Assert.That(a.OptionMarketValue, Is.EqualTo(1000.0));
      Assert.That(a.StockMarketValue, Is.EqualTo(12000.0));
      Assert.That(a.CashAvailableForTrade, Is.EqualTo(8000.0));
      Assert.That(a.AvailableCash, Is.EqualTo(9000.0));
      Assert.That(a.LockedFunds, Is.EqualTo(500.0));
      Assert.That(a.LockedCash, Is.EqualTo(300.0));
      Assert.That(a.CreditLimit, Is.EqualTo(50000.0));
      Assert.That(a.ExcessEquity, Is.EqualTo(10000.0));
      Assert.That(a.ExcessLiquidity, Is.EqualTo(12000.0));
    }

    [Test]
    public void PrimeAssetResponse_Deserialize_WithSegments()
    {
      string json = @"{""code"":0,""data"":{""accountId"":""U123"",
        ""updateTimestamp"":1700000000,""segments"":[{""capability"":""STK"",
        ""category"":""SEC"",""currency"":""USD"",""cashBalance"":10000.0,
        ""cashAvailableForTrade"":8000.0,""cashAvailableForWithdrawal"":9000.0,
        ""grossPositionValue"":15000.0,""equityWithLoan"":20000.0,
        ""netLiquidation"":25000.0,""initMargin"":5000.0,""maintainMargin"":4000.0,
        ""overnightMargin"":4500.0,""unrealizedPL"":500.0,""realizedPL"":100.0,
        ""excessLiquidation"":12000.0,""overnightLiquidation"":11000.0,
        ""buyingPower"":50000.0,""leverage"":2.0,
        ""unrealizedPLByCostOfCarry"":450.0,""totalTodayPL"":600.0,
        ""lockedFunds"":200.0,""uncollected"":100.0,""currencyAssets"":[],
        ""consolidatedSegTypes"":[""SEC"",""FUT""]}]}}";
      var resp = JsonConvert.DeserializeObject<PrimeAssetResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var a = resp.Data;
      Assert.That(a.Account, Is.EqualTo("U123"));
      Assert.That(a.UpdateTimestamp, Is.EqualTo(1700000000));
      Assert.That(a.Segments.Count, Is.EqualTo(1));
      var s = a.Segments[0];
      Assert.That(s.Capability, Is.EqualTo("STK"));
      Assert.That(s.Category, Is.EqualTo("SEC"));
      Assert.That(s.Currency, Is.EqualTo("USD"));
      Assert.That(s.CashBalance, Is.EqualTo(10000.0));
      Assert.That(s.CashAvailableForTrade, Is.EqualTo(8000.0));
      Assert.That(s.CashAvailableForWithdrawal, Is.EqualTo(9000.0));
      Assert.That(s.GrossPositionValue, Is.EqualTo(15000.0));
      Assert.That(s.EquityWithLoan, Is.EqualTo(20000.0));
      Assert.That(s.NetLiquidation, Is.EqualTo(25000.0));
      Assert.That(s.InitMargin, Is.EqualTo(5000.0));
      Assert.That(s.MaintainMargin, Is.EqualTo(4000.0));
      Assert.That(s.OvernightMargin, Is.EqualTo(4500.0));
      Assert.That(s.UnrealizedPL, Is.EqualTo(500.0));
      Assert.That(s.RealizedPL, Is.EqualTo(100.0));
      Assert.That(s.ExcessLiquidation, Is.EqualTo(12000.0));
      Assert.That(s.OvernightLiquidation, Is.EqualTo(11000.0));
      Assert.That(s.BuyingPower, Is.EqualTo(50000.0));
      Assert.That(s.Leverage, Is.EqualTo(2.0));
      Assert.That(s.UnrealizedPLByCostOfCarry, Is.EqualTo(450.0));
      Assert.That(s.TotalTodayPL, Is.EqualTo(600.0));
      Assert.That(s.LockedFunds, Is.EqualTo(200.0));
      Assert.That(s.Uncollected, Is.EqualTo(100.0));
      Assert.That(s.ConsolidatedSegTypes.Count, Is.EqualTo(2));
      Assert.That(s.ConsolidatedSegTypes[1], Is.EqualTo("FUT"));
    }

    [Test]
    public void PrimeAnalyticsAssetResponse_Deserialize_WithSummaryAndHistory()
    {
      string json = @"{""code"":0,""data"":{""summary"":{""pnl"":500.0,
        ""pnlPercentage"":0.02,""annualizedReturn"":0.15,
        ""overUserPercentage"":0.5},""history"":[{""date"":1700000000,
        ""asset"":25000.0,""pnl"":500.0,""pnlPercentage"":0.02,
        ""cashBalance"":10000.0,""grossPositionValue"":15000.0,
        ""deposit"":1000.0,""withdrawal"":500.0}]}}";
      var resp = JsonConvert.DeserializeObject<PrimeAnalyticsAssetResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var a = resp.Data;
      Assert.That(a.Summary.Pnl, Is.EqualTo(500.0));
      Assert.That(a.Summary.PnlPercentage, Is.EqualTo(0.02));
      Assert.That(a.Summary.AnnualizedReturn, Is.EqualTo(0.15));
      Assert.That(a.Summary.OverUserPercentage, Is.EqualTo(0.5));
      Assert.That(a.History.Count, Is.EqualTo(1));
      var h = a.History[0];
      Assert.That(h.Date, Is.EqualTo(1700000000));
      Assert.That(h.Asset, Is.EqualTo(25000.0));
      Assert.That(h.Pnl, Is.EqualTo(500.0));
      Assert.That(h.PnlPercentage, Is.EqualTo(0.02));
      Assert.That(h.CashBalance, Is.EqualTo(10000.0));
      Assert.That(h.GrossPositionValue, Is.EqualTo(15000.0));
      Assert.That(h.Deposit, Is.EqualTo(1000.0));
      Assert.That(h.Withdrawal, Is.EqualTo(500.0));
    }

    // ---------------------------------------------------------------- Trade: Deposit/Withdraw & Fund

    [Test]
    public void DepositWithdrawResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":[{""id"":""DW001"",""refId"":""REF001"",
        ""type"":""DEPOSIT"",""typeDesc"":""Cash Deposit"",""currency"":""USD"",
        ""amount"":5000.0,""businessDate"":""2026-01-02"",""completedStatus"":""DONE"",
        ""updatedAt"":1700000000,""createdAt"":1700000001}]}";
      var resp = JsonConvert.DeserializeObject<DepositWithdrawResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var d = resp.Data[0];
      Assert.That(d.Id, Is.EqualTo("DW001"));
      Assert.That(d.RefId, Is.EqualTo("REF001"));
      Assert.That(d.Type, Is.EqualTo("DEPOSIT"));
      Assert.That(d.TypeDesc, Is.EqualTo("Cash Deposit"));
      Assert.That(d.Currency, Is.EqualTo("USD"));
      Assert.That(d.Amount, Is.EqualTo(5000.0));
      Assert.That(d.BusinessDate, Is.EqualTo("2026-01-02"));
      Assert.That(d.CompletedStatus, Is.EqualTo("DONE"));
      Assert.That(d.UpdatedAt, Is.EqualTo(1700000000));
      Assert.That(d.CreatedAt, Is.EqualTo(1700000001));
    }

    [Test]
    public void EstimateTradableQuantityResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":{""tradableQuantity"":100.0,
        ""financingQuantity"":200.0,""positionQuantity"":50.0,
        ""tradablePositionQuantity"":50.0}}";
      var resp = JsonConvert.DeserializeObject<EstimateTradableQuantityResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var d = resp.Data;
      Assert.That(d.TradableQuantity, Is.EqualTo(100.0));
      Assert.That(d.FinancingQuantity, Is.EqualTo(200.0));
      Assert.That(d.PositionQuantity, Is.EqualTo(50.0));
      Assert.That(d.TradablePositionQuantity, Is.EqualTo(50.0));
    }

    [Test]
    public void FundDetailsResponse_Deserialize_WithPagedItems()
    {
      string json = @"{""code"":0,""data"":{""page"":1,""limit"":20,""itemCount"":1,
        ""pageCount"":1,""timestamp"":1700000000,""items"":[{""id"":1,
        ""currency"":""USD"",""type"":""DEPOSIT"",""desc"":""deposit"",
        ""contractName"":""C1"",""segType"":""SEC"",""amount"":5000.0,
        ""businessDate"":""2026-01-02"",""updatedAt"":1700000001}]}}";
      var resp = JsonConvert.DeserializeObject<FundDetailsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var p = resp.Data;
      Assert.That(p.Page, Is.EqualTo(1));
      Assert.That(p.Limit, Is.EqualTo(20));
      Assert.That(p.ItemCount, Is.EqualTo(1));
      Assert.That(p.PageCount, Is.EqualTo(1));
      Assert.That(p.Timestamp, Is.EqualTo(1700000000));
      Assert.That(p.Items.Count, Is.EqualTo(1));
      var f = p.Items[0];
      Assert.That(f.Id, Is.EqualTo(1));
      Assert.That(f.Currency, Is.EqualTo("USD"));
      Assert.That(f.Type, Is.EqualTo("DEPOSIT"));
      Assert.That(f.Desc, Is.EqualTo("deposit"));
      Assert.That(f.ContractName, Is.EqualTo("C1"));
      Assert.That(f.SegType, Is.EqualTo("SEC"));
      Assert.That(f.Amount, Is.EqualTo(5000.0));
      Assert.That(f.BusinessDate, Is.EqualTo("2026-01-02"));
      Assert.That(f.UpdatedAt, Is.EqualTo(1700000001));
    }

    [Test]
    public void ForexTradeOrderResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":{""id"":99,""symbol"":""EURUSD"",
        ""orderType"":""LMT"",""limitPrice"":1.05,""status"":""Submitted""}}";
      var resp = JsonConvert.DeserializeObject<ForexTradeOrderResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Id, Is.EqualTo(99));
      Assert.That(resp.Data.Symbol, Is.EqualTo("EURUSD"));
      Assert.That(resp.Data.OrderType, Is.EqualTo("LMT"));
      Assert.That(resp.Data.LimitPrice, Is.EqualTo(1.05));
    }

    // ---------------------------------------------------------------- Trade: Segment Fund

    [Test]
    public void SegmentFundResponse_Deserialize_AllFieldsMapped()
    {
      string json = @"{""code"":0,""data"":{""id"":100,""fromSegment"":""SEC"",
        ""toSegment"":""FUT"",""currency"":""USD"",""amount"":5000.0,
        ""status"":""SUCC"",""statusDesc"":""Success"",""message"":""ok"",
        ""settledAt"":1700000000,""updatedAt"":1700000001,""createdAt"":1700000002}}";
      var resp = JsonConvert.DeserializeObject<SegmentFundResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      var f = resp.Data;
      Assert.That(f.Id, Is.EqualTo(100));
      Assert.That(f.FromSegment, Is.EqualTo("SEC"));
      Assert.That(f.ToSegment, Is.EqualTo("FUT"));
      Assert.That(f.Currency, Is.EqualTo("USD"));
      Assert.That(f.Amount, Is.EqualTo(5000.0));
      Assert.That(f.Status, Is.EqualTo(SegmentFundStatus.SUCC));
      Assert.That(f.StatusDesc, Is.EqualTo("Success"));
      Assert.That(f.Message, Is.EqualTo("ok"));
      Assert.That(f.SettledTime, Is.EqualTo(1700000000));
      Assert.That(f.UpdatedTime, Is.EqualTo(1700000001));
      Assert.That(f.CreatedTime, Is.EqualTo(1700000002));
    }

    [Test]
    public void SegmentFundsResponse_Deserialize_List()
    {
      string json = @"{""code"":0,""data"":[{""id"":1,""fromSegment"":""SEC"",""toSegment"":""FUT"",
        ""currency"":""USD"",""amount"":1000.0,""status"":""NEW"",""statusDesc"":""New"",
        ""message"":"""",""settledAt"":0,""updatedAt"":1700000000,""createdAt"":1700000001}]}";
      var resp = JsonConvert.DeserializeObject<SegmentFundsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      Assert.That(resp.Data[0].Status, Is.EqualTo(SegmentFundStatus.NEW));
    }

    [Test]
    public void SegmentFundAvailableResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""fromSegment"":""SEC"",""currency"":""USD"",
        ""amount"":5000.0}]}";
      var resp = JsonConvert.DeserializeObject<SegmentFundAvailableResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      Assert.That(resp.Data[0].FromSegment, Is.EqualTo("SEC"));
      Assert.That(resp.Data[0].Currency, Is.EqualTo("USD"));
      Assert.That(resp.Data[0].Amount, Is.EqualTo(5000.0));
    }

    // ---------------------------------------------------------------- Trade: Support types

    [Test]
    public void CurrencyAssets_Deserialize_AllFieldsMapped()
    {
      string json = @"{""currency"":""USD"",""cashBalance"":10000.0,
        ""cashAvailableForTrade"":8000.0,""grossPositionValue"":15000.0,
        ""stockMarketValue"":12000.0,""optionMarketValue"":1000.0,
        ""futuresMarketValue"":500.0,""unrealizedPL"":500.0,""realizedPL"":100.0}";
      var c = JsonConvert.DeserializeObject<CurrencyAssets>(json, TigerClient.JsonSet);
      Assert.That(c.Currency, Is.EqualTo("USD"));
      Assert.That(c.CashBalance, Is.EqualTo(10000.0));
      Assert.That(c.CashAvailableForTrade, Is.EqualTo(8000.0));
      Assert.That(c.GrossPositionValue, Is.EqualTo(15000.0));
      Assert.That(c.StockMarketValue, Is.EqualTo(12000.0));
      Assert.That(c.OptionMarketValue, Is.EqualTo(1000.0));
      Assert.That(c.FuturesMarketValue, Is.EqualTo(500.0));
      Assert.That(c.UnrealizedPL, Is.EqualTo(500.0));
      Assert.That(c.RealizedPL, Is.EqualTo(100.0));
    }

    [Test]
    public void Charge_WithChargeDetails_Deserialize()
    {
      string json = @"{""category"":""COMMISSION"",""categoryDesc"":""Commission"",
        ""total"":10.0,""details"":[{""type"":""BROKER"",""typeDesc"":""Broker Fee"",
        ""originalAmount"":10.0,""afterDiscountAmount"":8.0}]}";
      var c = JsonConvert.DeserializeObject<Charge>(json, TigerClient.JsonSet);
      Assert.That(c.Category, Is.EqualTo("COMMISSION"));
      Assert.That(c.CategoryDesc, Is.EqualTo("Commission"));
      Assert.That(c.Total, Is.EqualTo(10.0));
      Assert.That(c.Details.Count, Is.EqualTo(1));
      var d = c.Details[0];
      Assert.That(d.Type, Is.EqualTo("BROKER"));
      Assert.That(d.TypeDesc, Is.EqualTo("Broker Fee"));
      Assert.That(d.OriginalAmount, Is.EqualTo(10.0));
      Assert.That(d.AfterDiscountAmount, Is.EqualTo(8.0));
    }

    [Test]
    public void TickSizeItem_Deserialize()
    {
      string json = @"{""begin"":""0"",""end"":""5"",""type"":""OPEN_CLOSED"",""tickSize"":0.01}";
      var t = JsonConvert.DeserializeObject<TickSizeItem>(json, TigerClient.JsonSet);
      Assert.That(t.Begin, Is.EqualTo("0"));
      Assert.That(t.End, Is.EqualTo("5"));
      Assert.That(t.Type, Is.EqualTo(TickSizeType.OPEN_CLOSED));
      Assert.That(t.TickSize, Is.EqualTo(0.01));
    }

    // ---------------------------------------------------------------- Error path

    [Test]
    public void WarrantQuoteResponse_ErrorResponse()
    {
      string json = "{\"code\":2100,\"message\":\"quote error\"}";
      var resp = JsonConvert.DeserializeObject<WarrantQuoteResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.False);
      Assert.That(resp.Code, Is.EqualTo(2100));
      Assert.That(resp.Message, Is.EqualTo("quote error"));
    }

    [Test]
    public void OptionChainResponse_EmptyData_Deserialize()
    {
      string json = @"{""code"":0,""data"":[]}";
      var resp = JsonConvert.DeserializeObject<OptionChainResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(0));
    }

    [Test]
    public void FutureContractResponse_NullData_Deserialize()
    {
      string json = @"{""code"":0}";
      var resp = JsonConvert.DeserializeObject<FutureContractResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data, Is.Null);
    }

    [Test]
    public void PositionTransferResponse_MissingOptionalFields_Deserialize()
    {
      string json = @"{""code"":0,""data"":{""id"":1,""accountId"":""U123"",
        ""status"":""PENDING""}}";
      var resp = JsonConvert.DeserializeObject<PositionTransferResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Id, Is.EqualTo(1));
      Assert.That(resp.Data.AccountId, Is.EqualTo("U123"));
      Assert.That(resp.Data.Status, Is.EqualTo("PENDING"));
      Assert.That(resp.Data.Memo, Is.Null);
      Assert.That(resp.Data.Method, Is.Null);
      Assert.That(resp.Data.UserId, Is.EqualTo(0));
      Assert.That(resp.Data.FinishedAt, Is.EqualTo(0));
    }
  }
}
