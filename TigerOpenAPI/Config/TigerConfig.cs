using System;
using System.Text;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Config
{
  public class TigerConfig
  {
    public const string PPRVATE_KEY_PREFIX = "KEY-----";
    public const string PRIVATE_KEY_SUFFIX = "-----END";
    public const Env DEFAULT_ENV = Env.PROD;

    /**
     * log file output directory
     */
    public static string LogDir { get; set; }

    public Env Environment { get; set; } = DEFAULT_ENV;

    /**
     * (must)tigerId : 2015xxxx,  address：https://developer.itigerup.com/profile
     */
    public string TigerId { get; set; }

    /**
     * (must)tigerId : TBNZ/TBSG/TBAU/TBHK,  address：https://developer.itigerup.com/profile
     */
    public License License { get; set; }

    /**
     * (must)private key, use readPrivateKey() method, read private key from file
     */
    public string PrivateKey { get; set; }

    /**
     * default account
     */
    public string DefaultAccount { get; set; }

    /**
     * whether to automatically grab quote permission when the initialization instance is completed
     */
    public bool AutoGrabPermission { get; set; } = true;

    /**
     * default time zone
     */
    public TimeZoneInfo TimeZone { get; set; } = DateUtil.HK_ZONE;

    /**
     * default language
     */
    public Language Language { get; set; } = Language.en_US;

    /**
     * institutional trader private key
     */
    public string SecretKey { get; set; }

    private Protocol subscribeProtocol = Protocol.SECURE_SOCKET;
    public Protocol SubscribeProtocol
    {
      get => subscribeProtocol;
      set
      {
        if (value == null || value == Protocol.HTTP)
        {
          return;
        }
        subscribeProtocol = value;
      }
    }

    /**
     * read private key from file(Remove first and last lines and line breaks)
     *
     * @param privateKeyFile absolute path
     * @return privateKey String
     */
    public static string ReadPrivateKey(string privateKeyFile)
    {
      try {
        string content = System.IO.File.ReadAllText(privateKeyFile);
        int start = 0;
        int startIdx = content.IndexOf(PPRVATE_KEY_PREFIX);
        if (startIdx >= 0)
        {
          start = startIdx + PPRVATE_KEY_PREFIX.Length;
        }
        int end = content.Length;
        int endIndex = content.IndexOf(PRIVATE_KEY_SUFFIX);
        if (endIndex > 0)
        {
          end = endIndex;
        }
        StringBuilder builder = new StringBuilder();
        foreach (char ch in content.Substring(start, end - start))
        {
          if (ch == 10 || ch == 13)
          {
            continue;
          }
          builder.Append(ch);
        }
        return builder.ToString();
      } catch (IOException e)
      {
        ApiLogger.Error($"read file fail:{privateKeyFile}", e);
      }
      return string.Empty;
    }
  }
}
