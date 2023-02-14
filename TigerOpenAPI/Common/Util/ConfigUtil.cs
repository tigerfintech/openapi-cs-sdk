using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Common.Util
{
  public class ConfigUtil
  {
    private const string PPRVATE_KEY_PREFIX = "KEY-----";
    private const string PRIVATE_KEY_SUFFIX = "-----END";
    private const string COMMENT_PREFIX = "#";
    private const char EQUAL_CHAR = '=';

    private const string CONFIG_FILE_PRIVATE_KEY = "private_key_pk8";
    private const string CONFIG_FILE_TIGER_ID = "tiger_id";
    private const string CONFIG_FILE_ACCOUNT = "account";
    private const string CONFIG_FILE_LICENSE = "license";
    private const string CONFIG_FILE_ENV = "env";
    private const string TOKEN_FILE_TOKEN = "token";

    private static ISet<string> configFileKeys = new HashSet<string>()
    {
      CONFIG_FILE_PRIVATE_KEY,
      CONFIG_FILE_TIGER_ID,
      CONFIG_FILE_ACCOUNT,
      CONFIG_FILE_LICENSE,
      CONFIG_FILE_ENV,
    };

    private ConfigUtil() { }

    private static bool CheckFile(string dir, string fileName)
    {
      if (string.IsNullOrWhiteSpace(dir))
      {
        return false;
      }
      dir = dir.Trim();
      if (!Directory.Exists(dir))
      {
        ApiLogger.Info($"config file directory[{dir}] is missing, ingore");
        return false;
      }

      string configFilePath = Path.Combine(dir, fileName);
      if (!File.Exists(configFilePath))
      {
        ApiLogger.Info($"config file[{configFilePath}] is missing, ingore");
        return false;
      }

      return true;
    }

    public static void LoadConfigFile(TigerConfig tigerConfig)
    {
      if (!CheckFile(tigerConfig.ConfigFilePath, TigerApiConstants.CONFIG_FILENAME))
      {
        return;
      }

      string configFile = Path.Combine(tigerConfig.ConfigFilePath.Trim(), TigerApiConstants.CONFIG_FILENAME);
      Dictionary<string, string> dataDict = ReadPropertiesFile(configFile, configFileKeys);

      foreach (var item in dataDict)
      {
        switch (item.Key)
        {
          case CONFIG_FILE_PRIVATE_KEY:
            tigerConfig.PrivateKey = item.Value;
            break;
          case CONFIG_FILE_TIGER_ID:
            tigerConfig.TigerId = item.Value;
            break;
          case CONFIG_FILE_ACCOUNT:
            tigerConfig.DefaultAccount = item.Value;
            break;
          case CONFIG_FILE_LICENSE:
            License license = (License) System.Enum.Parse(typeof(License), item.Value);
            if (License.NONE != license)
            {
              tigerConfig.License = license;
            }
            break;
          case CONFIG_FILE_ENV:
            Env env = (Env)System.Enum.Parse(typeof(Env), item.Value);
            tigerConfig.Environment = env;
            break;
          default:
            break;
        }
      }
    }

    public static bool LoadTokenFile(TigerConfig tigerConfig)
    {
      if (!CheckFile(tigerConfig.ConfigFilePath, TigerApiConstants.TOKEN_FILENAME))
      {
        return false;
      }

      string tokenFile = Path.Combine(tigerConfig.ConfigFilePath.Trim(), TigerApiConstants.TOKEN_FILENAME);
      Dictionary<string, string> dataDict = ReadPropertiesFile(tokenFile);
      string token = dataDict[TOKEN_FILE_TOKEN];

      if (string.IsNullOrWhiteSpace(token))
      {
        return false;
      }
      tigerConfig.Token = token;
      return true;
    }

    public static bool UpdateTokenFile(TigerConfig tigerConfig, string token)
    {
      if (string.IsNullOrWhiteSpace(token))
      {
        return false;
      }
      if (!CheckFile(tigerConfig.ConfigFilePath, TigerApiConstants.TOKEN_FILENAME))
      {
        return false;
      }

      string tokenFilePath = Path.Combine(tigerConfig.ConfigFilePath.Trim(), TigerApiConstants.TOKEN_FILENAME);
      try {
        System.IO.File.WriteAllText(tokenFilePath,
          (new StringBuilder(TOKEN_FILE_TOKEN)).Append(EQUAL_CHAR).Append(token).ToString(),
          new UTF8Encoding(false));
        return true;
      } catch (IOException e)
      {
        ApiLogger.Error($"write file fail:{tokenFilePath}", e);
      }
      return false;
    }

    public static Dictionary<string, string> ReadPropertiesFile(string configFile,
      ISet<string>? includeKeys = null)
    {
      Dictionary<string, string> dataDict = new Dictionary<string, string>();
      try
      {
        using (StreamReader reader = new StreamReader(configFile))
        {
          string? line = null;
          while ((line = reader.ReadLine()) != null)
          {
            line = line.Trim();
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith(COMMENT_PREFIX))
            {
              continue;
            }
            int idx = line.IndexOf(EQUAL_CHAR);
            if (idx <= 0 || idx == line.Length - 1)
            {
              continue;
            }
            string fieldname = line.Substring(0, idx).Trim();
            if (includeKeys == null || includeKeys.Count == 0 || includeKeys.Contains(fieldname))
            {
              dataDict.Add(fieldname, line.Substring(idx + 1).Trim());
            }
          }
        }
      }
      catch (IOException e)
      {
        ApiLogger.Error($"read file fail:{configFile}", e);
      }
      return dataDict;
    }

    /**
     * read private key from file(Remove first and last lines and line breaks)
     *
     * @param privateKeyFile absolute path
     * @return privateKey String
     */
    public static string ReadPrivateKey(string privateKeyFile)
    {
      try
      {
        string content = System.IO.File.ReadAllText(privateKeyFile);
        return ProcessPrivateKey(content);
      }
      catch (IOException e)
      {
        ApiLogger.Error($"read file fail:{privateKeyFile}", e);
      }
      return string.Empty;
    }

    public static string ProcessPrivateKey(string content)
    {
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
      return Trim(content, start, end);
    }

    private static string Trim(string content, int start, int end)
    {
      StringBuilder builder = new StringBuilder();
      foreach (char ch in content.Substring(start, end - start))
      {
        if (ch == 10 || ch == 13 || ch == 32)
        {
          continue;
        }
        builder.Append(ch);
      }
      return builder.ToString();
    }

    public static long GetCreateTime(string token)
    {
      string text = Encoding.UTF8.GetString(Convert.FromBase64String(token));
      int idx = text.IndexOf(TigerApiConstants.SEPARATOR);
      if (idx > 0 && Int64.TryParse(text.Substring(0, idx), out long createTime))
      {
        return createTime;
      }
      return 0;
    }

    public static long GetExpiredTime(string token)
    {
      string text = Encoding.UTF8.GetString(Convert.FromBase64String(token));
      int idx = text.IndexOf(TigerApiConstants.SEPARATOR);
      if (idx > 0 && Int64.TryParse(text.Substring(idx + 1, 13), out long expiredTime))
      {
        return expiredTime;
      }
      return 0;
    }

  }
}
