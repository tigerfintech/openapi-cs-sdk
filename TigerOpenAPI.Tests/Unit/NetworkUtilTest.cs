using System;
using System.Reflection;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for NetworkUtil. GetDeviceId is exercised directly (no network
  /// required). ConvertUriType and GetDefaultUrlInfo are private static methods
  /// so they are invoked via reflection to test the real mapping logic without
  /// hitting the network-based GetServerAddress path.
  /// </summary>
  [TestFixture]
  public class NetworkUtilTest
  {
    // ---------- GetDeviceId ----------

    [Test]
    public void GetDeviceId_ReturnsNonEmptyString()
    {
      string id = NetworkUtil.GetDeviceId();
      Assert.That(id, Is.Not.Empty);
    }

    [Test]
    public void GetDeviceId_MultipleCalls_ReturnsSameValue()
    {
      string first = NetworkUtil.GetDeviceId();
      string second = NetworkUtil.GetDeviceId();
      Assert.That(second, Is.EqualTo(first));
    }

    // ---------- ConvertUriType (private -> reflection) ----------

    private static UriType InvokeConvertUriType(License license, string key)
    {
      var method = typeof(NetworkUtil).GetMethod("ConvertUriType",
        BindingFlags.NonPublic | BindingFlags.Static);
      Assert.That(method, Is.Not.Null, "ConvertUriType method not found via reflection");
      return (UriType)method!.Invoke(null, new object[] { license, key })!;
    }

    [Test]
    public void ConvertUriType_CommonKey_ReturnsCommon()
    {
      Assert.That(InvokeConvertUriType(License.TBNZ, "COMMON"), Is.EqualTo(UriType.COMMON));
    }

    [Test]
    public void ConvertUriType_LicenseName_ReturnsTrade()
    {
      Assert.That(InvokeConvertUriType(License.TBNZ, "TBNZ"), Is.EqualTo(UriType.TRADE));
    }

    [Test]
    public void ConvertUriType_LicenseNameQuote_ReturnsQuote()
    {
      Assert.That(InvokeConvertUriType(License.TBSG, "TBSG-QUOTE"), Is.EqualTo(UriType.QUOTE));
    }

    [Test]
    public void ConvertUriType_LicenseNamePaper_ReturnsPaper()
    {
      Assert.That(InvokeConvertUriType(License.TBAU, "TBAU-PAPER"), Is.EqualTo(UriType.PAPER));
    }

    [Test]
    public void ConvertUriType_UnknownKey_ReturnsNone()
    {
      Assert.That(InvokeConvertUriType(License.TBNZ, "unknown"), Is.EqualTo(UriType.NONE));
    }

    [Test]
    public void ConvertUriType_MismatchedLicenseName_ReturnsNone()
    {
      // Key matches a different license than the one passed -> NONE
      Assert.That(InvokeConvertUriType(License.TBNZ, "TBSG"), Is.EqualTo(UriType.NONE));
    }

    // ---------- GetDefaultUrlInfo (private -> reflection) ----------

    private static (string Domain, string KeyField, string SocketPort) InvokeGetDefaultUrlInfo(
      Env env, Protocol protocol)
    {
      var method = typeof(NetworkUtil).GetMethod("GetDefaultUrlInfo",
        BindingFlags.NonPublic | BindingFlags.Static);
      Assert.That(method, Is.Not.Null, "GetDefaultUrlInfo method not found via reflection");
      var result = method!.Invoke(null, new object[] { env, protocol });
      var tuple = (ValueTuple<string, string, string>)result!;
      return (tuple.Item1, tuple.Item2, tuple.Item3);
    }

    [Test]
    public void GetDefaultUrlInfo_ProdHttp_ReturnsProdDomain()
    {
      var info = InvokeGetDefaultUrlInfo(Env.PROD, Protocol.HTTP);
      Assert.That(info.Domain, Is.EqualTo(TigerApiConstants.DEFAULT_PROD_DOMAIN_URL));
      Assert.That(info.KeyField, Is.EqualTo("openapi"));
      Assert.That(info.SocketPort, Is.EqualTo(TigerApiConstants.DEFAULT_PROD_SOCKET_SSL_PORT));
    }

    [Test]
    public void GetDefaultUrlInfo_ProdWebSocket_ReturnsProdSocketPort()
    {
      var info = InvokeGetDefaultUrlInfo(Env.PROD, Protocol.WEB_SOCKET);
      Assert.That(info.Domain, Is.EqualTo(TigerApiConstants.DEFAULT_PROD_DOMAIN_URL));
      Assert.That(info.SocketPort, Is.EqualTo(TigerApiConstants.DEFAULT_PROD_SOCKET_PORT));
    }

    [Test]
    public void GetDefaultUrlInfo_SandboxHttp_ReturnsSandboxDomain()
    {
      var info = InvokeGetDefaultUrlInfo(Env.SANDBOX, Protocol.HTTP);
      Assert.That(info.Domain, Is.EqualTo(TigerApiConstants.DEFAULT_SANDBOX_DOMAIN_URL));
      Assert.That(info.KeyField, Is.EqualTo("openapi-sandbox"));
      Assert.That(info.SocketPort, Is.EqualTo(TigerApiConstants.DEFAULT_SANDBOX_SOCKET_SSL_PORT));
    }

    [Test]
    public void GetDefaultUrlInfo_SandboxWebSocket_ReturnsSandboxSocketPort()
    {
      var info = InvokeGetDefaultUrlInfo(Env.SANDBOX, Protocol.WEB_SOCKET);
      Assert.That(info.SocketPort, Is.EqualTo(TigerApiConstants.DEFAULT_SANDBOX_SOCKET_PORT));
    }

    [Test]
    public void GetDefaultUrlInfo_TestHttp_ReturnsTestDomain()
    {
      var info = InvokeGetDefaultUrlInfo(Env.TEST, Protocol.HTTP);
      Assert.That(info.Domain, Is.EqualTo(TigerApiConstants.DEFAULT_TEST_DOMAIN_URL));
      Assert.That(info.KeyField, Is.EqualTo("openapi-test"));
    }

    [Test]
    public void GetDefaultUrlInfo_TestSocket_ReturnsTestSocketDomain()
    {
      var info = InvokeGetDefaultUrlInfo(Env.TEST, Protocol.SECURE_SOCKET);
      Assert.That(info.Domain, Is.EqualTo(TigerApiConstants.DEFAULT_TEST_DOMAIN_URL));
    }
  }
}
