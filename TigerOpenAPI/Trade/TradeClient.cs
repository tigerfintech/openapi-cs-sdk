using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;

namespace TigerOpenAPI.Trade
{
  public class TradeClient : TigerClient
  {
    private string ServerUrl { get; set; }
    private string ServerUrlForPaper { get; set; }


    public TradeClient(TigerConfig config) : base(config)
    {
      // use cgplay by Env and license
      Dictionary<UriType, string> uriDict = NetworkUtil.GetServerAddress(Protocol.HTTP, config.License, config.Environment);
      ServerUrl = string.IsNullOrWhiteSpace(uriDict[UriType.TRADE]) ? uriDict[UriType.COMMON] : uriDict[UriType.TRADE];
      ServerUrlForPaper = string.IsNullOrWhiteSpace(uriDict[UriType.PAPER]) ? uriDict[UriType.COMMON] : uriDict[UriType.PAPER];

      //if (Env.PROD == config.Environment)
      //{
      //  ServerUrl = "https://openapi.tigerfintech.com/hkg/gateway";
      //  ServerUrlForPaper = "https://openapi-sandbox.tigerfintech.com/hkg/gateway";
      //}
      //else if (Env.SANDBOX == config.Environment)
      //{
      //  ServerUrl = "https://openapi-sandbox.tigerfintech.com/gateway";
      //  ServerUrlForPaper = "https://openapi-sandbox.tigerfintech.com/gateway";
      //}
      //else
      //{
      //  ServerUrl = "https://openapi-test.tigerfintech.com/gateway";
      //  ServerUrlForPaper = "https://openapi-test.tigerfintech.com/gateway";
      //}
    }

    public override string GetServerUri<T>(TigerRequest<T> request)
    {
      //ApiLogger.Debug($"TradeClient env:{Environment}, license:{License}, account:{request.ModelValue.Account}");
      // 区分账号类型, 环境和牌照
      return AccountUtil.isVirtualAccount(request?.ModelValue?.Account) ? ServerUrlForPaper : ServerUrl;
    }

    public override bool Validate<T>(TigerRequest<T> request, out string errorMsg)
    {
      errorMsg = string.Empty;
      if (!TradeApiService.IsTradeApi(request?.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_COMMON_PARAM_ERROR.Message, $"'ApiMethodName'({request?.ApiMethodName}) is not trade api");
        return false;
      }
      if (!string.IsNullOrWhiteSpace(request?.ModelValue?.Account)
        && !string.Equals(TradeApiService.ACCOUNTS, request?.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_BIZ_PARAM_EMPTY_ERROR.Message, "'Account'");
        return false;
      }
      // other param check
      return true;
    }
  }
}

