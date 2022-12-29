using System;
namespace TigerOpenAPI.Config
{
  public class Protocol
  {
    public static readonly Protocol HTTP = new Protocol("https://{0}/gateway", "");
    public static readonly Protocol WEB_SOCKET = new Protocol("wss://{0}:{1}/stomp", "port");
    public static readonly Protocol SECURE_SOCKET = new Protocol("wss://{0}:{1}", "socket_port");

    readonly string urlFormat;
    readonly string portFieldName;

    private Protocol(string urlFormat, string portFieldName)
    {
      this.urlFormat = urlFormat;
      this.portFieldName = portFieldName;
    }

    public string UrlFormat { get => urlFormat; }

    public string PortFieldName { get => portFieldName; }

    public void Deconstruct(out string urlFormat, out string portFieldName)
    {
      urlFormat = UrlFormat;
      portFieldName = PortFieldName;
    }

    public static bool IsWebSocketUrl(string url)
    {
      if (string.IsNullOrWhiteSpace(url))
      {
        return false;
      }
      string webSocketUrlFormat = WEB_SOCKET.UrlFormat;
      string suffix = webSocketUrlFormat.Substring(webSocketUrlFormat.LastIndexOf('/'));
      return url.EndsWith(suffix);
    }
  }
}
