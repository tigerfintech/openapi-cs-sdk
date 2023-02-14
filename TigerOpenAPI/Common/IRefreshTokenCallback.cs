using System;
using TigerOpenAPI.Config;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Common
{
  public interface IRefreshTokenCallback
  {
    void TokenChange(TigerConfig config, string oldToken, UserToken token);
  }
}

