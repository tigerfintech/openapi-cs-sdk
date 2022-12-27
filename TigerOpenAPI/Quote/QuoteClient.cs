﻿using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote
{
  public class QuoteClient : TigerClient
  {

    public string ServerUrl { get; set; }

    public QuoteClient(TigerConfig config) : base(config)
    {
      // TODO use cgplay by Env and license
      if (Env.PROD == config.Environment)
        ServerUrl = "https://openapi.tigerfintech.com/hkg-quote/gateway";
      else if (Env.SANDBOX == config.Environment)
        ServerUrl = "https://openapi-sandbox.tigerfintech.com/gateway";
      else
        ServerUrl = "https://openapi-test.tigerfintech.com/gateway";
    }

    public override string GetServerUri<T>(TigerRequest<T> request)
    {
      //ApiLogger.Debug($"QuoteClient env:{Environment}, license:{License}");
      return ServerUrl;
    }

    public override bool Validate<T>(TigerRequest<T> request, out string errorMsg)
    {
      errorMsg = string.Empty;
      if (!QuoteApiService.IsQuoteApi(request?.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_COMMON_PARAM_ERROR.Message, $"'ApiMethodName'({request?.ApiMethodName}) is not quote api");
        return false;
      }
      // other param check
      return true;
    }
  }
}

