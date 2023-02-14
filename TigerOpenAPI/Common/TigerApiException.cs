using System;
namespace TigerOpenAPI.Common
{
  public class TigerApiException : Exception
  {

    public int ErrCode { get; private set; }
    public string ErrMsg { get; private set; }
    public TigerApiCode TigerApiCode { get; private set; }

    public TigerApiException() : base ()
    {
    }

    public TigerApiException(string message) : base (message)
    {
      ErrMsg = message;
    }

    public TigerApiException(int errCode, string errMsg) : base (errCode + ":" + errMsg)
    {
      ErrCode = errCode;
      ErrMsg = errMsg;
    }

    public TigerApiException(TigerApiCode error) : this (error.Code, error.Message)
    {
      TigerApiCode = error;
    }

    public TigerApiException(TigerApiCode error, params string[] args) : this(error.Code, String.Format(error.Message, args))
    {
      TigerApiCode = error;
    }
  }
}

