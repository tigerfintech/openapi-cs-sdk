using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;
using static System.Net.WebRequestMethods;

namespace TigerOpenAPI.Common.Util
{
  public class NetworkUtil
  {
    private const string GET_DEVICE_ERROR = "please check network connection";
    private const int MAC_ARRAY_LENGTH = 6;

    private NetworkUtil()
    {
    }

    public static string GetDeviceId()
    {
      List<string> macs = new List<string>();
      try
      {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface ni in interfaces)
        {
          if (OperationalStatus.Up != ni.OperationalStatus
            || ni.NetworkInterfaceType == NetworkInterfaceType.Unknown
            || ni.NetworkInterfaceType == NetworkInterfaceType.Loopback
            || ni.NetworkInterfaceType == NetworkInterfaceType.Ppp)
          {
            continue;
          }
          string macAddress = ni.GetPhysicalAddress().ToString();
          if (macAddress.Length == 12)
          {
            for (int i = 1; i < MAC_ARRAY_LENGTH; i++)
            {
              macAddress = macAddress.Insert(3 * i - 1, ":");
            }
            macs.Add(macAddress);
          }
        }
      }
      catch (Exception e)
      {
        ApiLogger.Error("GetPhysicalAddress failed. {}, {}", e.Message, GET_DEVICE_ERROR);
      }
      return macs.FirstOrDefault() ?? "unknown";
    }


    public static Dictionary<UriType, string> GetServerAddress(
      Protocol protocol, License license, Env environment)
    {
      string response = string.Empty;
      List<Dictionary<string, object>>? domainConfigList = null;
      try
      {
        response = HttpUtil.HttpGet(TigerApiConstants.DOMAIN_GARDEN_ADDRESS);
        Dictionary<string, object>? domainConfigDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
        if (domainConfigDict != null && domainConfigDict["items"] != null) {
          JArray jArray = (JArray)domainConfigDict["items"];
          domainConfigList = jArray.ToObject<List<Dictionary<string, object>>>();
        }
      }
      catch (Exception ex)
      {
        ApiLogger.Error(ex, "domain config response error, response:{}", response);
      }
      // if get domain config data failed and original address is not emtpy, return original address
      if (domainConfigList == null || domainConfigList.Count == 0)
      {
        return new Dictionary<UriType, string>(){
          { Protocol.HTTP == protocol ? UriType.COMMON : UriType.SOCKET,
            Env.PROD == environment ? TigerApiConstants.DEFAULT_PROD_DOMAIN_URL : Env.SANDBOX == environment ?
              TigerApiConstants.DEFAULT_SANDBOX_DOMAIN_URL : TigerApiConstants.DEFAULT_TEST_DOMAIN_URL }
        };
      }

      string? port = getDefaultPort(environment, protocol);
      Dictionary<UriType, string> domainUrlDict = new Dictionary<UriType, string>();
      string? commonUrl = string.Empty;
      foreach (Dictionary<string, object> configMap in domainConfigList)
      {
        object openapiConfig = configMap[Env.PROD == environment ? "openapi" : "openapi-sandbox"];
        if (openapiConfig != null) {
          Dictionary<string, object>? dataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(openapiConfig.ToString() ?? string.Empty);
          if (dataDict == null)
            continue;
          //Dictionary<string, object> dataDict = (Dictionary<string, object>)openapiConfig;
          foreach (KeyValuePair<string, object> item in dataDict)
          {
            if (Protocol.WEB_SOCKET.PortFieldName.Equals(item.Key)
                || Protocol.SECURE_SOCKET.PortFieldName.Equals(item.Key))
            {
              if (protocol.PortFieldName.Equals(item.Value.ToString()))
              {
                port = item.Value.ToString();
              }
              continue;
            }

            string? domainUrl = item.Value?.ToString()?.Replace("https://", "");
            UriType uriType = convertUriType(license, item.Key);
            if (uriType != UriType.NONE && !string.IsNullOrWhiteSpace(domainUrl))
            {
              // NONE, COMMON, TRADE, QUOTE, PAPER, SOCKET
              if (protocol == Protocol.HTTP && uriType != UriType.SOCKET
                || protocol != Protocol.HTTP && (uriType == UriType.SOCKET || uriType == UriType.COMMON))
              {
                domainUrlDict.TryAdd(uriType, string.Format(protocol.UrlFormat, domainUrl, port));
              }
            }
            commonUrl = (string.IsNullOrWhiteSpace(commonUrl) && uriType == UriType.COMMON) ? domainUrl : commonUrl;
          }
        }
      }
      if (string.IsNullOrWhiteSpace(commonUrl))
      {
        commonUrl = getDefaultUrl(environment);
        domainUrlDict.TryAdd(UriType.COMMON, string.Format(protocol.UrlFormat, commonUrl, port));
      }
      if (protocol != Protocol.HTTP)
      {
          domainUrlDict.TryAdd(UriType.SOCKET, string.Format(protocol.UrlFormat, commonUrl, port));
      }
      return domainUrlDict;
    }

    private static string getDefaultPort(Env environment, Protocol protocol)
    {
      string port = string.Empty;
      if (protocol != Protocol.HTTP)
      {
        if (Env.PROD == environment)
        {
          port = protocol == Protocol.WEB_SOCKET ? TigerApiConstants.DEFAULT_PROD_SOCKET_PORT
              : TigerApiConstants.DEFAULT_PROD_SOCKET_SSL_PORT;
        }
        else
        {
          port = protocol == Protocol.WEB_SOCKET ? TigerApiConstants.DEFAULT_SANDBOX_SOCKET_PORT
              : TigerApiConstants.DEFAULT_SANDBOX_SOCKET_SSL_PORT;
        }
      }
      return port;
    }

    private static string getDefaultUrl(Env environment)
    {
      switch (environment)
      {
        case Env.SANDBOX:
          return TigerApiConstants.DEFAULT_SANDBOX_DOMAIN_URL;
        case Env.TEST:
          return TigerApiConstants.DEFAULT_TEST_DOMAIN_URL;
        default:
          return TigerApiConstants.DEFAULT_PROD_DOMAIN_URL;
      }
    }

    private static UriType convertUriType(License license, string key)
    {
      if (string.Equals(key, nameof(UriType.COMMON)))
        return UriType.COMMON;
      else
      {
        string licenseName = license.ToString();
        if (key.Equals(licenseName))
        {
          return UriType.TRADE;
        }
        else if (key.Equals(licenseName + "-" + nameof(UriType.QUOTE)))
        {
          return UriType.QUOTE;
        }
        else if (key.Equals(licenseName + "-" + nameof(UriType.PAPER)))
        {
          return UriType.PAPER;
        }
      }
      return UriType.NONE;
    }
  }
}