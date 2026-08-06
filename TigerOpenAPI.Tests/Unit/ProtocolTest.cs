using NUnit.Framework;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for Protocol static instances (HTTP / WEB_SOCKET / SECURE_SOCKET).
  /// Pure immutable data — zero network.
  /// </summary>
  [TestFixture]
  public class ProtocolTest
  {
    [Test]
    public void HttpProtocol_HasGatewayUrlFormat_AndEmptyPortFieldName()
    {
      Assert.That(Protocol.HTTP.UrlFormat, Is.EqualTo("https://{0}/gateway"));
      Assert.That(Protocol.HTTP.PortFieldName, Is.EqualTo(string.Empty));
    }

    [Test]
    public void WebSocketProtocol_HasStompUrlFormat_AndPortFieldName()
    {
      Assert.That(Protocol.WEB_SOCKET.UrlFormat, Is.EqualTo("wss://{0}:{1}/stomp"));
      Assert.That(Protocol.WEB_SOCKET.PortFieldName, Is.EqualTo("port"));
    }

    [Test]
    public void SecureSocketProtocol_HasBareWssUrlFormat_AndSocketPortFieldName()
    {
      Assert.That(Protocol.SECURE_SOCKET.UrlFormat, Is.EqualTo("wss://{0}:{1}"));
      Assert.That(Protocol.SECURE_SOCKET.PortFieldName, Is.EqualTo("socket_port"));
    }

    [Test]
    public void HttpUrlFormat_FormatsWithSingleDomainArg()
    {
      string formatted = string.Format(Protocol.HTTP.UrlFormat, "openapi.tigerfintech.com");
      Assert.That(formatted, Is.EqualTo("https://openapi.tigerfintech.com/gateway"));
    }

    [Test]
    public void WebSocketUrlFormat_FormatsWithDomainAndPort()
    {
      string formatted = string.Format(Protocol.WEB_SOCKET.UrlFormat, "openapi.tigerfintech.com", "9887");
      Assert.That(formatted, Is.EqualTo("wss://openapi.tigerfintech.com:9887/stomp"));
    }

    [Test]
    public void SecureSocketUrlFormat_FormatsWithDomainAndPort()
    {
      string formatted = string.Format(Protocol.SECURE_SOCKET.UrlFormat, "openapi.tigerfintech.com", "9883");
      Assert.That(formatted, Is.EqualTo("wss://openapi.tigerfintech.com:9883"));
    }

    [Test]
    public void PortFieldName_DistinguishesWebSocketFromSecureSocket()
    {
      Assert.That(Protocol.WEB_SOCKET.PortFieldName, Is.Not.EqualTo(Protocol.SECURE_SOCKET.PortFieldName),
          "WEB_SOCKET and SECURE_SOCKET must use distinct port field names");
      Assert.That(Protocol.HTTP.PortFieldName, Is.Empty,
          "HTTP does not use a port field");
    }

    [Test]
    public void Deconstruct_ReturnsUrlFormatAndPortFieldName_ForHttp()
    {
      var (urlFormat, portFieldName) = Protocol.HTTP;
      Assert.That(urlFormat, Is.EqualTo("https://{0}/gateway"));
      Assert.That(portFieldName, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Deconstruct_ReturnsUrlFormatAndPortFieldName_ForWebSocket()
    {
      var (urlFormat, portFieldName) = Protocol.WEB_SOCKET;
      Assert.That(urlFormat, Is.EqualTo("wss://{0}:{1}/stomp"));
      Assert.That(portFieldName, Is.EqualTo("port"));
    }

    [Test]
    public void Deconstruct_ReturnsUrlFormatAndPortFieldName_ForSecureSocket()
    {
      var (urlFormat, portFieldName) = Protocol.SECURE_SOCKET;
      Assert.That(urlFormat, Is.EqualTo("wss://{0}:{1}"));
      Assert.That(portFieldName, Is.EqualTo("socket_port"));
    }

    [Test]
    public void Deconstruct_MatchesPropertyGetters()
    {
      foreach (var protocol in new[] { Protocol.HTTP, Protocol.WEB_SOCKET, Protocol.SECURE_SOCKET })
      {
        var (urlFormat, portFieldName) = protocol;
        Assert.That(urlFormat, Is.EqualTo(protocol.UrlFormat));
        Assert.That(portFieldName, Is.EqualTo(protocol.PortFieldName));
      }
    }

    [Test]
    public void ThreeProtocols_AreDistinctInstances()
    {
      Assert.That(Protocol.HTTP, Is.Not.SameAs(Protocol.WEB_SOCKET));
      Assert.That(Protocol.HTTP, Is.Not.SameAs(Protocol.SECURE_SOCKET));
      Assert.That(Protocol.WEB_SOCKET, Is.Not.SameAs(Protocol.SECURE_SOCKET));
    }
  }
}
