using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Common
{
  public class DefaultRefreshTokenCallback : IRefreshTokenCallback
  {
    public DefaultRefreshTokenCallback()
    {
    }

    public void TokenChange(TigerConfig config, string oldToken, UserToken userToken)
    {
      try
      {
        ApiLogger.Info("tokenChange oldToken:{}, newTokenInfo:{}",
            oldToken, JsonConvert.SerializeObject(userToken));
        if (userToken == null)
        {
          ApiLogger.Warn("tokenChange called with null userToken — skipping token update");
          return;
        }
        config.Token = userToken.Token;
        ConfigFileUtil.UpdateTokenFile(config, userToken.Token);
      }
      catch (Exception e)
      {
        ApiLogger.Error("tokenChange process fail", e);
      }
    }
  }
}

