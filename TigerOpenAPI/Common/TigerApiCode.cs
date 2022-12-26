using System;
namespace TigerOpenAPI.Common
{
  public class TigerApiCode
  {

    public static readonly TigerApiCode SUCCESS = new TigerApiCode(0, "success");
    public static readonly TigerApiCode SERVER_ERROR = new TigerApiCode(1, "server error");
    public static readonly TigerApiCode READ_TIME_OUT = new TigerApiCode(2, "network read timeout");
    public static readonly TigerApiCode CLIENT_API_ERROR = new TigerApiCode(3, "sdk send request exception");
    public static readonly TigerApiCode ACCESS_FORBIDDEN = new TigerApiCode(4, "access forbidden");
    public static readonly TigerApiCode RATE_LIMIT_ERROR = new TigerApiCode(5, "rate limit error");
    public static readonly TigerApiCode EMPTY_DATA_ERROR = new TigerApiCode(6, "the data returned from the server is empty");

    public static readonly TigerApiCode COMMON_PARAM_ERROR = new TigerApiCode(1000, "common param error");
    public static readonly TigerApiCode BIZ_PARAM_ERROR = new TigerApiCode(1010, "biz param error");

    public static readonly TigerApiCode TRADE_RESPONSE_ERROR = new TigerApiCode(1100, "global account response error");
    public static readonly TigerApiCode STANDARD_RESPONSE_ERROR = new TigerApiCode(1200, "standard account response error");
    public static readonly TigerApiCode PAPER_RESPONSE_ERROR = new TigerApiCode(1300, "paper account response error");

    public static readonly TigerApiCode FINANCIAL_RESPONSE_ERROR = new TigerApiCode(2000, "financial response error");
    public static readonly TigerApiCode STOCK_RESPONSE_ERROR = new TigerApiCode(2100, "stock response error");
    public static readonly TigerApiCode OPTION_RESPONSE_ERROR = new TigerApiCode(2200, "option response error");
    public static readonly TigerApiCode FUTURES_RESPONSE_ERROR = new TigerApiCode(2300, "futures response error");
    public static readonly TigerApiCode USER_RESPONSE_ERROR = new TigerApiCode(2400, "user token error");

    public static readonly TigerApiCode SUBSCRIBE_ERROR = new TigerApiCode(3000, "subscribe error");
    public static readonly TigerApiCode SUBSCRIBE_SYMBOL_ERROR = new TigerApiCode(3001, "subscribe symbol error");
    public static readonly TigerApiCode SUBSCRIBE_SYMBOL_LIMIT_ERROR = new TigerApiCode(3002, "subscribe symbol limit error");
    public static readonly TigerApiCode SUBSCRIBE_SUBJECT_ERROR = new TigerApiCode(3003, "subscribe subject error");
    public static readonly TigerApiCode SUBSCRIBE_ID_ERROR = new TigerApiCode(3004, "subscribe tigerId error");
    public static readonly TigerApiCode SUBSCRIBE_OTHER_SUBJECT_ERROR = new TigerApiCode(3005, "register subject (except quote) error");
    public static readonly TigerApiCode UNSUBSCRIBE_ERROR = new TigerApiCode(3006, "unsubscribe error");
    public static readonly TigerApiCode UNKNOWN_STOMP_COMMAND = new TigerApiCode(3007, "unknown stomp command");
    public static readonly TigerApiCode SUBSCRIBE_CHANNEL_ERROR = new TigerApiCode(3008, "subscribe channel error");

    public static readonly TigerApiCode CONNECTION_KICK_OUT_ERROR = new TigerApiCode(4001, "kick out by a new connection");

    public static readonly TigerApiCode SIGN_CHECK_FAILED = new TigerApiCode(40013, "check sign and data fail");

    public static readonly TigerApiCode HTTP_COMMON_PARAM_ERROR = new TigerApiCode(10000, "client common param error({0})");
    public static readonly TigerApiCode HTTP_COMMON_PARAM_EMPTY_ERROR = new TigerApiCode(10001, "client common param error({0} is requried)");
    public static readonly TigerApiCode HTTP_BIZ_PARAM_ERROR = new TigerApiCode(10100, "client biz param error({0})");
    public static readonly TigerApiCode HTTP_BIZ_PARAM_EMPTY_ERROR = new TigerApiCode(10101, "client biz param error ({0} is requried)");
    public static readonly TigerApiCode HTTP_BIZ_PARAM_VALUE_ERROR = new TigerApiCode(10102, "client biz param error ({0} is incorrect)");
    public static readonly TigerApiCode HTTP_BIZ_PARAM_RANGE_ERROR = new TigerApiCode(10103, "client biz param error ('{0}' cannot be greater than '{1}')");
    public static readonly TigerApiCode HTTP_BIZ_PARAM_CONCTRACT_SECTYPE_ERROR =
        new TigerApiCode(10104, "client biz param error('sec_type':'{0}' is not supported, all supported sec_type include:['OPT','WAR','IOPT'])");

    public int Code { get; private set; }
    public string Message { get; set; }

    TigerApiCode(int code, string message)
    {
      Code = code;
      Message = message;
    }

  }
}
