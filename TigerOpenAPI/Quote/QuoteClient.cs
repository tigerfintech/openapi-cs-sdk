using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote.Model;

namespace TigerOpenAPI.Quote
{
  public class QuoteClient : TigerClient
  {

    public string ServerUrl { get; set; }

    public QuoteClient(TigerConfig config) : base(config)
    {
      ApiLogger.Debug($"QuoteClient env:{config.Environment}, license:{config.License}");
      RefreshServerUri(config);

      TokenManager.GetInstance().Init(config, this);
      if (config.AutoGrabPermission)
      {
        TigerRequest<TigerListResponse> request = new TigerRequest<TigerListResponse>()
        {
          ApiMethodName = QuoteApiService.GRAB_QUOTE_PERMISSION
        };
        Execute(request);
      }
    }

    protected override void RefreshServerUri(TigerConfig config)
    {
      Dictionary<UriType, string> uriDict = NetworkUtil.GetServerAddress(Protocol.HTTP, config.License, config.Environment);
      ServerUrl = (uriDict.ContainsKey(UriType.QUOTE) && !string.IsNullOrWhiteSpace(uriDict[UriType.QUOTE]))
        ? uriDict[UriType.QUOTE] : uriDict[UriType.COMMON];
    }

    public override string GetServerUri<T>(TigerRequest<T> request)
    {
      return ServerUrl;
    }

    public override bool Validate<T>(TigerRequest<T> request, out string errorMsg)
    {
      errorMsg = string.Empty;
      if (!QuoteApiService.IsQuoteApi(request.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_COMMON_PARAM_ERROR.Message, $"'ApiMethodName'({request?.ApiMethodName}) is not quote api");
        return false;
      }
      // other param check
      return true;
    }
  }
}

