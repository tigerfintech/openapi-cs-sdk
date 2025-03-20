using System;
using System.Text;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Config
{
  public class TigerConfig
  {
    public const Env DEFAULT_ENV = Env.PROD;

    /**
     * (optional)log file output directory
     */
    public static string LogDir { get; set; }

    /**
     * tiger_openapi_config.properties/tiger_openapi_token.properties config file directory
     */
    public string ConfigFilePath { get; set; } = string.Empty;

    public Env Environment { get; set; } = DEFAULT_ENV;

    /**
     * (must)tigerId : 2015xxxx,  address：https://developer.itigerup.com/profile
     */
    public string TigerId { get; set; }

    /**
     * (must)license : TBNZ/TBSG/TBAU/TBHK,  address：https://developer.itigerup.com/profile
     */
    public License License { get; set; }

    /**
     * (must)private key, use readPrivateKey() method, read private key from file
     */
    public string PrivateKey { get; set; }

    /**
     * (optional)default account
     */
    public string DefaultAccount { get; set; }

    /**
     * api token(for TBHK license)
     */
    public string Token { get; set; }

    /**
     * refresh token frequency
     */
    public int RefreshTokenIntervalDays { get; set; }

    /**
     * refresh token time, formate: HH:mm:ss
     */
    public string RefreshTokenTime { get; set; }

    /**
     * (optional)automatically refresh token
     */
    public bool AutoRefreshToken { get; set; } = false;

    /**
     * (optional)whether to automatically grab quote permission when the initialization instance is completed
     */
    public bool AutoGrabPermission { get; set; } = true;

    /**
     * (optional)request fail retry counts, range:[0, 5], if less than 1 will no retry; if bigger than 5 will set default value
     */
    public int FailRetryCounts { get; set; } = TigerApiConstants.DefaultRetryCount;

    /**
     * (optional)default time zone
     */
    public TimeZoneInfo TimeZone { get; set; } = CustomTimeZone.HK_ZONE;

    /**
     * (optional)default language
     */
    public Language Language { get; set; } = Language.en_US;

    /**
     * institutional trader private key
     */
    public string SecretKey { get; set; }

    /**
     * socket protocal use SSL
     */
    public bool IsSslSocket { get; set; } = true;

    /**
     * subscribed tradetick data, Whether to use the full version of the stock tick
     */
    public bool UseFullTick { get; set; } = false;
  }
}
