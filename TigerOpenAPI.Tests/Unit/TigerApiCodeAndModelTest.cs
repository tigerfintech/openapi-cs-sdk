using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for TigerApiCode, TigerApiConstants, TigerApiException, TigerRequest, and response model classes.
  /// Zero network — pure construction and serialization assertions.
  /// </summary>
  [TestFixture]
  public class TigerApiCodeAndModelTest
  {
    [Test]
    public void TigerApiCode_Success_HasCode0()
    {
      Assert.That(TigerApiCode.SUCCESS.Code, Is.EqualTo(0));
      Assert.That(TigerApiCode.SUCCESS.Message, Is.EqualTo("success"));
    }

    [Test]
    public void TigerApiCode_AllCodes_HaveNonZeroCodeExceptSuccess()
    {
      Assert.That(TigerApiCode.SERVER_ERROR.Code, Is.EqualTo(1));
      Assert.That(TigerApiCode.READ_TIME_OUT.Code, Is.EqualTo(2));
      Assert.That(TigerApiCode.CLIENT_API_ERROR.Code, Is.EqualTo(3));
      Assert.That(TigerApiCode.ACCESS_FORBIDDEN.Code, Is.EqualTo(4));
      Assert.That(TigerApiCode.RATE_LIMIT_ERROR.Code, Is.EqualTo(5));
      Assert.That(TigerApiCode.EMPTY_DATA_ERROR.Code, Is.EqualTo(6));
    }

    [Test]
    public void TigerApiCode_ParamErrorCodes_HaveExpectedValues()
    {
      Assert.That(TigerApiCode.COMMON_PARAM_ERROR.Code, Is.EqualTo(1000));
      Assert.That(TigerApiCode.SIGN_CHECK_FAILED.Code, Is.EqualTo(40013));
    }

    [Test]
    public void TigerApiCode_HttpParamErrors_HaveFormatPlaceholders()
    {
      Assert.That(TigerApiCode.HTTP_COMMON_PARAM_ERROR.Message, Does.Contain("{0}"));
      Assert.That(TigerApiCode.HTTP_COMMON_PARAM_EMPTY_ERROR.Message, Does.Contain("{0}"));
      Assert.That(TigerApiCode.HTTP_BIZ_PARAM_ERROR.Message, Does.Contain("{0}"));
    }

    [Test]
    public void TigerApiCode_MessageIsSettable()
    {
      var code = TigerApiCode.SUCCESS;
      string original = code.Message;
      code.Message = "modified";
      Assert.That(code.Message, Is.EqualTo("modified"));
      // Restore original to avoid polluting other tests that check SUCCESS.Message.
      code.Message = original;
    }

    [Test]
    public void TigerApiConstants_HaveExpectedValues()
    {
      Assert.That(TigerApiConstants.SEPARATOR, Is.EqualTo(","));
      Assert.That(TigerApiConstants.API_VERSION_1, Is.EqualTo("1.0"));
      Assert.That(TigerApiConstants.DEFAULT_VERSION, Is.EqualTo("3.0"));
      Assert.That(TigerApiConstants.SIGN_TYPE_RSA, Is.EqualTo("RSA"));
      Assert.That(TigerApiConstants.SIGN_ALGORITHMS, Is.EqualTo("SHA1WithRSA"));
      Assert.That(TigerApiConstants.CHARSET_UTF8, Is.EqualTo("UTF-8"));
      Assert.That(TigerApiConstants.CONTENT_TYPE_JSON, Is.EqualTo("application/json"));
      Assert.That(TigerApiConstants.CONFIG_FILENAME, Is.EqualTo("tiger_openapi_config.properties"));
      Assert.That(TigerApiConstants.TOKEN_FILENAME, Is.EqualTo("tiger_openapi_token.properties"));
    }

    [Test]
    public void TigerApiConstants_DomainUrls_AreNotEmpty()
    {
      Assert.That(TigerApiConstants.API_ONLINE_DOMAIN_URL, Is.Not.Empty);
      Assert.That(TigerApiConstants.API_SANDBOX_DOMAIN_URL, Is.Not.Empty);
      Assert.That(TigerApiConstants.DEFAULT_TEST_DOMAIN_URL, Is.EqualTo(TigerApiConstants.API_SANDBOX_DOMAIN_URL));
      Assert.That(TigerApiConstants.DEFAULT_PROD_DOMAIN_URL, Is.EqualTo(TigerApiConstants.API_ONLINE_DOMAIN_URL));
    }

    [Test]
    public void TigerApiConstants_SocketPorts_AreNotEmpty()
    {
      Assert.That(TigerApiConstants.DEFAULT_PROD_SOCKET_PORT, Is.EqualTo("9887"));
      Assert.That(TigerApiConstants.DEFAULT_PROD_SOCKET_SSL_PORT, Is.EqualTo("9883"));
      Assert.That(TigerApiConstants.DEFAULT_SANDBOX_SOCKET_PORT, Is.EqualTo("9889"));
    }

    [Test]
    public void TigerApiConstants_RetryCounts_HaveExpectedValues()
    {
      Assert.That(TigerApiConstants.DefaultRetryCount, Is.EqualTo(2));
      Assert.That(TigerApiConstants.MaxRetryCount, Is.EqualTo(5));
    }

    // --- TigerRequest ---

    [Test]
    public void TigerRequest_DefaultProperties_HaveExpectedValues()
    {
      var req = new TigerRequest<TigerResponse>();
      Assert.That(req.ApiVersion, Is.EqualTo(TigerApiConstants.DEFAULT_VERSION));
      Assert.That(req.Charset, Is.EqualTo(TigerApiConstants.CHARSET_UTF8));
      Assert.That(req.SignType, Is.EqualTo(TigerApiConstants.SIGN_TYPE_RSA));
      Assert.That(req.SdkVersion, Does.StartWith("openapi-cs-sdk-"));
    }

    [Test]
    public void TigerRequest_GetResponseType_ReturnsGenericType()
    {
      var req = new TigerRequest<TigerResponse>();
      Assert.That(req.GetResponseType(), Is.EqualTo(typeof(TigerResponse)));
    }

    [Test]
    public void TigerRequest_Serialization_ProducesCorrectWireNames()
    {
      var req = new TigerRequest<TigerResponse>
      {
        TigerId = "12345",
        ApiMethodName = "test_method",
        Timestamp = "2024-01-01 00:00:00",
        Sign = "abc123",
        BizContent = "{}"
      };
      string json = JsonConvert.SerializeObject(req, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"tiger_id\":\"12345\""));
      Assert.That(json, Does.Contain("\"method\":\"test_method\""));
      Assert.That(json, Does.Contain("\"timestamp\":\"2024-01-01 00:00:00\""));
      Assert.That(json, Does.Contain("\"sign\":\"abc123\""));
      Assert.That(json, Does.Contain("\"biz_content\":\"{}\""));
      Assert.That(json, Does.Contain("\"version\":\"3.0\""));
      Assert.That(json, Does.Contain("\"charset\":\"UTF-8\""));
      Assert.That(json, Does.Contain("\"sign_type\":\"RSA\""));
    }

    // --- TigerResponse subclasses ---

    [Test]
    public void TigerDictResponse_Deserialize_DataDict()
    {
      string json = "{\"code\":0,\"message\":\"ok\",\"data\":{\"key\":\"value\"}}";
      var resp = JsonConvert.DeserializeObject<TigerDictResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data["key"], Is.EqualTo("value"));
    }

    [Test]
    public void TigerListResponse_Deserialize_DataListOfDict()
    {
      string json = "{\"code\":0,\"data\":[{\"k\":\"v1\"},{\"k\":\"v2\"}]}";
      var resp = JsonConvert.DeserializeObject<TigerListResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(2));
      Assert.That(resp.Data[0]["k"], Is.EqualTo("v1"));
    }

    [Test]
    public void TigerListStringResponse_Deserialize_DataListOfStrings()
    {
      string json = "{\"code\":0,\"data\":[\"a\",\"b\",\"c\"]}";
      var resp = JsonConvert.DeserializeObject<TigerListStringResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(3));
      Assert.That(resp.Data[2], Is.EqualTo("c"));
    }

    [Test]
    public void TigerStringResponse_Deserialize_DataString()
    {
      string json = "{\"code\":0,\"data\":\"hello\"}";
      var resp = JsonConvert.DeserializeObject<TigerStringResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data, Is.EqualTo("hello"));
    }

    // --- ApiModel ---

    [Test]
    public void ApiModel_DefaultConstructor_LangIsNone()
    {
      var model = new ApiModel();
      Assert.That(model.Lang, Is.EqualTo(Language.None));
      Assert.That(model.Account, Is.Null);
    }

    [Test]
    public void ApiModel_Serialization_LangSerializedAsWireValue()
    {
      var model = new ApiModel { Lang = Language.en_US, Account = "U123456" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"lang\":\"en_US\""));
      Assert.That(json, Does.Contain("\"account\":\"U123456\""));
    }

    // --- BatchApiModel ---

    [Test]
    public void BatchApiModel_ContainsItems()
    {
      var batch = new BatchApiModel<ApiModel>();
      batch.Items = new List<ApiModel>
      {
        new ApiModel { Lang = Language.en_US, Account = "A1" },
        new ApiModel { Lang = Language.zh_CN, Account = "A2" }
      };
      string json = JsonConvert.SerializeObject(batch.Items, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("A1"));
      Assert.That(json, Does.Contain("A2"));
    }

    // --- OptionSymbol ---

    [Test]
    public void OptionSymbol_PropertiesSettable()
    {
      var os = new OptionSymbol
      {
        Symbol = "AAPL",
        Expiry = "20240119",
        Strike = "180",
        Right = "PUT"
      };
      Assert.That(os.Symbol, Is.EqualTo("AAPL"));
      Assert.That(os.Expiry, Is.EqualTo("20240119"));
      Assert.That(os.Strike, Is.EqualTo("180"));
      Assert.That(os.Right, Is.EqualTo("PUT"));
    }
  }
}
