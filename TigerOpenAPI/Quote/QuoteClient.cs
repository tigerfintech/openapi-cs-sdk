using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote
{
  public class QuoteClient : TigerClient
  {

    public string ServerUrl { get; set; }

    public QuoteClient(TigerConfig config) : base(config)
    {
      ServerUrl = "https://openapi-sandbox.tigerfintech.com/gateway";
      //ServerUrl = "https://openapi-test.tigerfintech.com/gateway";
      //ServerUrl = "http://openapi-local.tigerfintech.com:8085/gateway";
    }

    public override string GetServerUri<T>(TigerRequest<T> request)
    {
      ApiLogger.Debug($"QuoteClient env:{Environment}, license:{License}");
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

