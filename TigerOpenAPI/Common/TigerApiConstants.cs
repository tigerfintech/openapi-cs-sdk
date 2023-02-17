
namespace TigerOpenAPI.Common
{
  public class TigerApiConstants
  {
    public const string SEPARATOR = ",";

    public const string DEFAULT_VERSION = "3.0";

    public const string SIGN_TYPE = "sign_type";

    public const string SIGN_TYPE_RSA = "RSA";

    public const string SIGN_ALGORITHMS = "SHA1WithRSA";

    public const string TIGER_ID = "tiger_id";

    public const string ACCESS_TOKEN = "access_token";

    public const string TRADE_TOKEN = "trade_token";

    public const string ACCOUNT_TYPE = "account_type";

    public const string METHOD = "method";

    public const string TIMESTAMP = "timestamp";

    public const string VERSION = "version";

    public const string SDK_VERSION = "sdk-version";

    public const string HEART_BEAT = "heart-beat";

    public const string SIGN = "sign";

    public const string CHARSET = "charset";

    public const string NOTIFY_URL = "notify_url";

    public const string BIZ_CONTENT = "biz_content";

    public const string CHARSET_UTF8 = "UTF-8";

    public const string CONTENT_TYPE_JSON = "application/json";

    public const string CODE = "code";

    public const string MESSAGE = "message";

    public const string DATA = "data";

    public const string ACCOUNT = "account";

    public const string DEVICE_ID = "device_id";

    public const string SSL_HANDLER_NAME = "sslHandler";

    public const string AUTHORIZATION = "Authorization";
    public const string CONFIG_FILENAME = "tiger_openapi_config.properties";
    public const string TOKEN_FILENAME = "tiger_openapi_token.properties";

    public const string API_ONLINE_DOMAIN_URL = "openapi.tigerfintech.com";
    public const string API_SANDBOX_DOMAIN_URL = "openapi-sandbox.tigerfintech.com";
    public const string API_TEST_DOMAIN_URL = "openapi-test.tigerfintech.com";

    public const string DEFAULT_PROD_DOMAIN_URL = API_ONLINE_DOMAIN_URL;
    public const string DEFAULT_SANDBOX_DOMAIN_URL = API_SANDBOX_DOMAIN_URL;
    public const string DEFAULT_TEST_DOMAIN_URL = API_TEST_DOMAIN_URL;
    public const string DOMAIN_GARDEN_ADDRESS = "https://cg.play-analytics.com/";

    public const string DEFAULT_PROD_SOCKET_PORT = "9887";
    public const string DEFAULT_PROD_SOCKET_SSL_PORT = "9883";
    public const string DEFAULT_SANDBOX_SOCKET_PORT = "9889";
    public const string DEFAULT_SANDBOX_SOCKET_SSL_PORT = "9885";

    public const int DefaultRetryCount = 2;
    public const int MaxRetryCount = 5;
  }
}
