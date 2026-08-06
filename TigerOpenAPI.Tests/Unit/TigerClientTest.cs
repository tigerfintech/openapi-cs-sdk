using System;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Tests.TestSupport;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for TigerClient base class — constructor validation, config accessors,
  /// BuildParams signing, Validate base behavior, and AfterExecute deserialization.
  /// Uses TestClientFactory.CreateOfflineConfig() (in-memory RSA key, no file I/O,
  /// no network). A TestableTigerClient subclass exposes the protected AfterExecute.
  /// </summary>
  [TestFixture]
  public class TigerClientTest
  {
    private TigerConfig _config = null!;
    private TestableTigerClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      _config = TestClientFactory.CreateOfflineConfig();
      _client = new TestableTigerClient(_config);
    }

    [Test]
    public void Constructor_WithValidConfig_DoesNotThrow()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      Assert.DoesNotThrow(() => new TestableTigerClient(config));
    }

    [Test]
    public void Constructor_WithNullConfig_ThrowsArgumentNullException()
    {
      var ex = Assert.Throws<ArgumentNullException>(() => new TestableTigerClient(null!));
      Assert.That(ex!.Message, Does.Contain("TigerConfig is empty."));
    }

    [Test]
    public void Constructor_WithEmptyTigerId_ThrowsArgumentNullException()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.TigerId = string.Empty;
      var ex = Assert.Throws<ArgumentNullException>(() => new TestableTigerClient(config));
      Assert.That(ex!.Message, Does.Contain("TigerId is empty."));
    }

    [Test]
    public void Constructor_WithEmptyPrivateKey_ThrowsArgumentNullException()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.PrivateKey = string.Empty;
      var ex = Assert.Throws<ArgumentNullException>(() => new TestableTigerClient(config));
      Assert.That(ex!.Message, Does.Contain("PrivateKey is empty."));
    }

    [Test]
    public void Constructor_WithNoneLicense_ThrowsArgumentNullException()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.License = License.NONE;
      var ex = Assert.Throws<ArgumentNullException>(() => new TestableTigerClient(config));
      Assert.That(ex!.Message, Does.Contain("License is empty."));
    }

    [Test]
    public void Constructor_WithWhitespaceTigerId_ThrowsArgumentNullException()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.TigerId = "   ";
      Assert.Throws<ArgumentNullException>(() => new TestableTigerClient(config));
    }

    [Test]
    public void GetConfigTimeZone_ReturnsConfigTimeZone()
    {
      Assert.That(_client.GetConfigTimeZone, Is.EqualTo(_config.TimeZone));
      Assert.That(_client.GetConfigTimeZone.Id, Is.EqualTo("Asia/Hong_Kong"));
    }

    [Test]
    public void GetConfigTimeZone_ReflectsOverriddenTimeZone()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.TimeZone = CustomTimeZone.NY_ZONE;
      var client = new TestableTigerClient(config);
      Assert.That(client.GetConfigTimeZone, Is.EqualTo(CustomTimeZone.NY_ZONE));
      Assert.That(client.GetConfigTimeZone.Id, Is.EqualTo("America/New_York"));
    }

    [Test]
    public void GetDefaultAccount_ReturnsConfigDefaultAccount()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.DefaultAccount = "12345678901234567";
      var client = new TestableTigerClient(config);
      Assert.That(client.GetDefaultAccount, Is.EqualTo("12345678901234567"));
    }

    [Test]
    public void GetDefaultAccount_ReturnsNull_WhenNotSet()
    {
      Assert.That(_client.GetDefaultAccount, Is.Null);
    }

    [Test]
    public void GetServerUri_BaseImplementation_ReturnsEmpty()
    {
      var req = new TigerRequest<TigerResponse>
      {
        ApiMethodName = QuoteApiService.MARKET_STATE
      };
      Assert.That(_client.GetServerUri(req), Is.EqualTo(string.Empty));
    }

    [Test]
    public void Validate_BaseImplementation_ReturnsTrueWithEmptyError()
    {
      var req = new TigerRequest<TigerResponse>
      {
        ApiMethodName = QuoteApiService.MARKET_STATE
      };
      bool valid = _client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.True);
      Assert.That(errorMsg, Is.EqualTo(string.Empty));
    }

    [Test]
    public void BuildParams_SetsTimestamp()
    {
      var req = NewQuoteRequest();
      _client.BuildParams(req);
      Assert.That(req.Timestamp, Is.Not.Null.And.Not.Empty,
          "BuildParams must populate Timestamp");
    }

    [Test]
    public void BuildParams_SetsTigerIdFromConfig()
    {
      var req = NewQuoteRequest();
      _client.BuildParams(req);
      Assert.That(req.TigerId, Is.EqualTo(_config.TigerId));
    }

    [Test]
    public void BuildParams_SetsSign_NonEmptyBase64()
    {
      var req = NewQuoteRequest();
      _client.BuildParams(req);
      Assert.That(req.Sign, Is.Not.Null.And.Not.Empty);
      Assert.DoesNotThrow(() => Convert.FromBase64String(req.Sign),
          "Sign must be valid Base64 (RSA-SHA1)");
    }

    [Test]
    public void BuildParams_SetsBizContent_FromModel()
    {
      var req = new TigerRequest<TradeCalendarResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new QuoteMarketModel { Market = Market.US }
      };
      _client.BuildParams(req);
      Assert.That(req.BizContent, Is.Not.Null.And.Not.Empty);
      Assert.That(req.BizContent, Does.Contain("US"));
    }

    [Test]
    public void BuildParams_SetsDeviceId_NonEmpty()
    {
      var req = NewQuoteRequest();
      _client.BuildParams(req);
      Assert.That(req.DeviceId, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void BuildParams_DifferentMethods_ProduceDifferentSigns()
    {
      var req1 = new TigerRequest<TradeCalendarResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new QuoteMarketModel { Market = Market.US }
      };
      var req2 = new TigerRequest<MarketStateResponse>
      {
        ApiMethodName = QuoteApiService.MARKET_STATE,
        ModelValue = new QuoteMarketModel { Market = Market.US }
      };
      _client.BuildParams(req1);
      _client.BuildParams(req2);
      Assert.That(req1.Sign, Is.Not.EqualTo(req2.Sign),
          "different methods must yield different signatures");
    }

    [Test]
    public void BuildParams_PreservesExplicitBizContent_WhenAlreadySet()
    {
      var req = NewQuoteRequest();
      const string preset = @"{""market"":""HK""}";
      req.BizContent = preset;
      _client.BuildParams(req);
      Assert.That(req.BizContent, Is.EqualTo(preset),
          "BuildParams must not overwrite an explicitly-set BizContent");
    }

    [Test]
    public void AfterExecute_WithEmptyData_ThrowsEmptyDataError()
    {
      var req = NewQuoteRequest();
      var ex = Assert.Throws<TigerApiException>(() =>
          _client.CallAfterExecute(req, false, string.Empty));
      Assert.That(ex!.TigerApiCode, Is.EqualTo(TigerApiCode.EMPTY_DATA_ERROR));
      Assert.That(ex.ErrCode, Is.EqualTo(TigerApiCode.EMPTY_DATA_ERROR.Code));
    }

    [Test]
    public void AfterExecute_WithWhitespaceData_ThrowsEmptyDataError()
    {
      var req = NewQuoteRequest();
      var ex = Assert.Throws<TigerApiException>(() =>
          _client.CallAfterExecute(req, false, "   "));
      Assert.That(ex!.TigerApiCode, Is.EqualTo(TigerApiCode.EMPTY_DATA_ERROR));
    }

    [Test]
    public void AfterExecute_WithValidJson_DeserializesResponse()
    {
      var req = NewQuoteRequest();
      const string json = @"{""code"":0,""message"":""success"",""timestamp"":1700000000000}";
      var response = _client.CallAfterExecute<MarketStateResponse>(req, false, json);
      Assert.That(response, Is.Not.Null);
      Assert.That(response!.Code, Is.EqualTo(0));
      Assert.That(response.Message, Is.EqualTo("success"));
      Assert.That(response.Timestamp, Is.EqualTo(1700000000000L));
      Assert.That(response.IsSuccess(), Is.True);
    }

    [Test]
    public void AfterExecute_WithErrorCode_DeserializesFailure()
    {
      var req = NewQuoteRequest();
      const string json = @"{""code"":40001,""message"":""invalid tiger id""}";
      var response = _client.CallAfterExecute<MarketStateResponse>(req, false, json);
      Assert.That(response, Is.Not.Null);
      Assert.That(response!.Code, Is.EqualTo(40001));
      Assert.That(response.Message, Is.EqualTo("invalid tiger id"));
      Assert.That(response.IsSuccess(), Is.False);
    }

    [Test]
    public void AfterExecute_WithEmptySign_ReturnsResponseWithoutVerification()
    {
      // Response without sign field skips signature verification (TigerPublicKey non-empty).
      var req = NewQuoteRequest();
      const string json = @"{""code"":0,""message"":""ok""}";
      var response = _client.CallAfterExecute<MarketStateResponse>(req, false, json);
      Assert.That(response, Is.Not.Null);
      Assert.That(response!.IsSuccess(), Is.True);
    }

    private static TigerRequest<MarketStateResponse> NewQuoteRequest()
    {
      return new TigerRequest<MarketStateResponse>
      {
        ApiMethodName = QuoteApiService.MARKET_STATE,
        ModelValue = new QuoteMarketModel { Market = Market.US }
      };
    }

    /// <summary>
    /// TigerClient subclass that exposes the protected AfterExecute for testing.
    /// Constructor does not call RefreshServerUri or TokenManager.Init, so it is
    /// fully offline (no network, no token init).
    /// </summary>
    private class TestableTigerClient : TigerClient
    {
      public TestableTigerClient(TigerConfig config) : base(config) { }

      public T? CallAfterExecute<T>(TigerRequest<T> request, bool isAsync, string data)
          where T : TigerResponse
      {
        return AfterExecute(request, in isAsync, in data);
      }
    }
  }
}
