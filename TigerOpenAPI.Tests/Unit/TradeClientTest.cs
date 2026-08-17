using System;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Tests.TestSupport;
using TigerOpenAPI.Trade;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for TradeClient — server URI routing, Validate() field checks,
  /// and custom server URL behavior. Uses TestClientFactory.CreateOfflineConfig().
  /// </summary>
  /// <remarks>
  /// TradeClient construction calls RefreshServerUri, which reaches out to the
  /// domain garden service. When that call fails (offline), NetworkUtil falls
  /// back to the default prod domain: https://openapi.tigerfintech.com/gateway.
  /// Construction happens once in OneTimeSetUp to amortize any network attempt.
  /// </remarks>
  [TestFixture]
  public class TradeClientTest
  {
    private TigerConfig _config = null!;
    private TradeClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      _config = TestClientFactory.CreateOfflineConfig();
      _config.DefaultAccount = null; // ensure no default account leaks into validation tests
      _client = new TradeClient(_config);
    }

    [Test]
    public void Constructor_WithOfflineConfig_DoesNotThrow()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      Assert.DoesNotThrow(() => new TradeClient(config));
    }

    [Test]
    public void GetServerUri_ReturnsHttpsGatewayUrl_ForRealAccount()
    {
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = new OptionExerciseSubmitModel
        {
          Account = "1234567890", // 10 digits, not a paper account
          ContractId = 1L,
          Type = "Exercise",
          Quantity = 1.0
        }
      };
      string uri = _client.GetServerUri(req);
      Assert.That(uri, Is.Not.Null.And.Not.Empty);
      Assert.That(uri, Does.StartWith("https://"),
          "trade server URI must be HTTPS");
      Assert.That(uri, Does.Contain("/gateway"),
          "trade server URI must target the gateway path");
    }

    [Test]
    public void GetServerUri_ReturnsHttpsGatewayUrl_ForVirtualAccount()
    {
      // 17-digit all-digit account is a paper/virtual account
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = new OptionExerciseSubmitModel
        {
          Account = "12345678901234567",
          ContractId = 1L,
          Type = "Exercise",
          Quantity = 1.0
        }
      };
      string uri = _client.GetServerUri(req);
      Assert.That(uri, Is.Not.Null.And.Not.Empty);
      Assert.That(uri, Does.StartWith("https://"),
          "paper server URI must be HTTPS");
      Assert.That(uri, Does.Contain("/gateway"),
          "paper server URI must target the gateway path");
    }

    [Test]
    public void GetServerUri_RoutesRealVsPaperAccount_ToConfiguredServerUrls()
    {
      // Real (non-paper) account routes to ServerUrl; paper account routes to
      // ServerUrlForPaper. Both must be valid HTTPS gateway URLs. The exact
      // host depends on the domain-garden config, so we assert structure only.
      var realReq = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = new OptionExerciseSubmitModel { Account = "12345", ContractId = 1L }
      };
      var paperReq = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = new OptionExerciseSubmitModel { Account = "12345678901234567", ContractId = 1L }
      };
      string realUri = _client.GetServerUri(realReq);
      string paperUri = _client.GetServerUri(paperReq);
      Assert.That(realUri, Does.StartWith("https://").And.Contains("/gateway"));
      Assert.That(paperUri, Does.StartWith("https://").And.Contains("/gateway"));
    }

    [Test]
    public void Validate_UserTokenRefresh_ReturnsTrueWithoutAccount()
    {
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = QuoteApiService.USER_TOKEN_REFRESH,
        ModelValue = null
      };
      bool valid = _client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.True);
      Assert.That(errorMsg, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Validate_NonTradeApi_ReturnsFalseWithError()
    {
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = "quote_real_time", // a quote api method, not trade
        ModelValue = new OptionExerciseSubmitModel { Account = "12345", ContractId = 1L }
      };
      bool valid = _client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.False);
      Assert.That(errorMsg, Does.Contain("not trade api"));
    }

    [Test]
    public void Validate_AccountsMethod_DoesNotRequireAccount()
    {
      var req = new TigerRequest<AccountsResponse>
      {
        ApiMethodName = TradeApiService.ACCOUNTS,
        ModelValue = new OptionExerciseSubmitModel { Account = null }
      };
      bool valid = _client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.True, "accounts method must not require an account");
      Assert.That(errorMsg, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Validate_TradeMethodWithEmptyAccountAndNoDefault_ReturnsFalse()
    {
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = new OptionExerciseSubmitModel { Account = null, ContractId = 1L }
      };
      bool valid = _client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.False);
      Assert.That(errorMsg, Does.Contain("Account"));
    }

    [Test]
    public void Validate_TradeMethod_FillsAccountFromConfigDefault()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.DefaultAccount = "9876543210";
      var client = new TradeClient(config);
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = new OptionExerciseSubmitModel { Account = null, ContractId = 1L }
      };
      bool valid = client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.True);
      Assert.That(errorMsg, Is.EqualTo(string.Empty));
      Assert.That(req.ModelValue!.Account, Is.EqualTo("9876543210"),
          "Validate must backfill Account from config.DefaultAccount");
    }

    [Test]
    public void Validate_TradeMethodWithAccountSet_ReturnsTrue()
    {
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = new OptionExerciseSubmitModel { Account = "12345", ContractId = 1L }
      };
      bool valid = _client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.True);
      Assert.That(errorMsg, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Validate_TradeModel_InjectsSecretKeyFromConfig()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.SecretKey = "INST_SECRET_KEY";
      var client = new TradeClient(config);
      var model = new OptionExerciseSubmitModel
      {
        Account = "12345",
        ContractId = 1L,
        SecretKey = null
      };
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = model
      };
      bool valid = client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.True);
      Assert.That(model.SecretKey, Is.EqualTo("INST_SECRET_KEY"),
          "Validate must inject config.SecretKey into TradeModel when missing");
    }

    [Test]
    public void Validate_TradeModel_PreservesExistingSecretKey()
    {
      var config = TestClientFactory.CreateOfflineConfig();
      config.SecretKey = "CONFIG_SECRET";
      var client = new TradeClient(config);
      var model = new OptionExerciseSubmitModel
      {
        Account = "12345",
        ContractId = 1L,
        SecretKey = "EXISTING_SECRET"
      };
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = model
      };
      bool valid = client.Validate(req, out string errorMsg);
      Assert.That(valid, Is.True);
      Assert.That(model.SecretKey, Is.EqualTo("EXISTING_SECRET"),
          "Validate must not overwrite an explicitly-set SecretKey");
    }

    [Test]
    public void UseCustomServerUrl_IsNoOpForTradeClient_BaseInherited()
    {
      // TradeClient does NOT override UseCustomServerUrl; the base implementation
      // is a no-op. Custom URL is not supported for trade requests — verify the
      // documented behavior: GetServerUri is unaffected by the call.
      var req = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = new OptionExerciseSubmitModel { Account = "12345", ContractId = 1L }
      };
      string uriBefore = _client.GetServerUri(req);

      Assert.DoesNotThrow(() => _client.UseCustomServerUrl("https://custom.example.com/api"));

      string uriAfter = _client.GetServerUri(req);
      Assert.That(uriAfter, Is.EqualTo(uriBefore),
          "TradeClient inherits the no-op base UseCustomServerUrl; URI must be unchanged");
      Assert.That(uriAfter, Does.StartWith("https://").And.Contains("/gateway"));
    }
  }
}
