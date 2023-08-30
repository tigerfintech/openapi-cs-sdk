using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Trade.Model;

namespace TigerOpenAPI.Trade
{
  public class TradeClient : TigerClient
  {
    private string ServerUrl { get; set; }
    private string ServerUrlForPaper { get; set; }

    public TradeClient(TigerConfig config) : base(config)
    {
      ApiLogger.Debug($"TradeClient env:{config.Environment}, license:{config.License}");
      RefreshServerUri(config);

      TokenManager.GetInstance().Init(config, this);
    }

    protected override void RefreshServerUri(TigerConfig config)
    {
      // get serverAddress by Env and license
      Dictionary<UriType, string> uriDict = NetworkUtil.GetServerAddress(Protocol.HTTP, config.License, config.Environment);
      ServerUrl = (uriDict.ContainsKey(UriType.TRADE) && !string.IsNullOrWhiteSpace(uriDict[UriType.TRADE]))
        ? uriDict[UriType.TRADE] : uriDict[UriType.COMMON];
      ServerUrlForPaper = (uriDict.ContainsKey(UriType.PAPER) && !string.IsNullOrWhiteSpace(uriDict[UriType.PAPER]))
        ? uriDict[UriType.PAPER] : uriDict[UriType.COMMON];
    }

    public override string GetServerUri<T>(TigerRequest<T> request)
    {
      return AccountUtil.IsVirtualAccount(request?.ModelValue?.Account) ? ServerUrlForPaper : ServerUrl;
    }

    public override bool Validate<T>(TigerRequest<T> request, out string errorMsg)
    {
      errorMsg = string.Empty;
      if (QuoteApiService.USER_TOKEN_REFRESH.Equals(request.ApiMethodName))
      {
        return true;
      }
      if (!TradeApiService.IsTradeApi(request.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_COMMON_PARAM_ERROR.Message, $"'ApiMethodName'({request?.ApiMethodName}) is not trade api");
        return false;
      }
      if (request.ModelValue != null && !string.Equals(TradeApiService.ACCOUNTS, request.ApiMethodName)
        && string.IsNullOrWhiteSpace(request.ModelValue.Account))
      {
        request.ModelValue.Account = Config.DefaultAccount;
      }
      if (request.ModelValue != null && request.ModelValue is TradeModel)
      {
        TradeModel tradeModel = (TradeModel)request.ModelValue;
        if (string.IsNullOrWhiteSpace(tradeModel.SecretKey)
          && !string.IsNullOrWhiteSpace(Config.SecretKey))
        {
          tradeModel.SecretKey = Config.SecretKey;
        }
      }

      if (string.IsNullOrWhiteSpace(request.ModelValue?.Account)
        && !string.Equals(TradeApiService.ACCOUNTS, request.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_BIZ_PARAM_EMPTY_ERROR.Message, "'Account'");
        return false;
      }
      // other param check
      return true;
    }
  }
}

