using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade
{
  public class TradeClient : TigerClient
  {
    private string ServerUrl { get; set; }
    private string ServerUrlForPaper { get; set; }


    public TradeClient(TigerConfig config) : base(config)
    {
      ServerUrl = "https://openapi-sandbox.tigerfintech.com/gateway";
      ServerUrlForPaper = "https://openapi-sandbox.tigerfintech.com/gateway";
    }

    public override string GetServerUri<T>(TigerRequest<T> request)
    {
      ApiLogger.Debug($"TradeClient env:{Environment}, license:{License}, account:{request.ModelValue.Account}");
      // 区分账号类型, 环境和牌照
      return ServerUrl;
    }

    public override bool Validate<T>(TigerRequest<T> request, out string errorMsg)
    {
      // TODO
      errorMsg = string.Empty;
      return true;
    }
  }
}

