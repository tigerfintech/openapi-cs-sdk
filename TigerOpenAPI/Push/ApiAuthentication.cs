using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Push
{
  public class ApiAuthentication
  {
    public const string PROTOBUF_VERSION_3 = "3";
    public const string DEFAULT_VERSION = PROTOBUF_VERSION_3;

    public string TigerId { get; set; }
    public string Sign { get; set; }
    public string Version { get; set; }

    ApiAuthentication(string tigerId)
    {
      TigerId = tigerId;
    }

    public static ApiAuthentication? build(string tigerId, string privateKey, string version = DEFAULT_VERSION)
    {
      ApiAuthentication authentication = new ApiAuthentication(tigerId);
      try
      {
        authentication.Sign = SignatureUtil.Sign(tigerId, privateKey, TigerApiConstants.CHARSET_UTF8);
        authentication.Version = version;
      }
      catch (Exception e)
      {
        ApiLogger.Error("authentication build fail:{}", e.Message, e);
        return null;
      }
      return authentication;
    }
  }
}

