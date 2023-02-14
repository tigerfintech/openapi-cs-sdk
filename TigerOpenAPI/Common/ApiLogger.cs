using NLog;
using NLog.Config;
using NLog.Targets;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Common
{
  public class ApiLogger
  {
    private static Logger log;
    private static string logDir = string.Empty;
    public static bool Enabled { get; set; } = true;
    public static bool DebugEnabled { get; set; } = false;
    public static bool InfoEnabled { get; set; } = true;
    public static bool WarnEnabled { get; set; } = true;
    public static bool ErrorEnabled { get; set; } = true;

    public static bool Ready { get; set; }

    private ApiLogger(){}

    static ApiLogger()
    {
      Ready = false;
      // from TigerConfig
      string logDir = TigerConfig.LogDir;
      if (string.IsNullOrWhiteSpace(logDir))
      {
        logDir = Path.Combine(System.Environment.CurrentDirectory, "log");
      }
      //const string simpleLayout = @"${longdate}|${level:uppercase=true}|${stacktrace}|${callsite}|${message} ${exception}";
      // const string simpleLayout = @"${longdate}|${level:uppercase=true}|${message:withexception=true}";
      const string simpleLayout = @"${longdate}|${level:padding=-5}|${message} ${exception}";
      FileTarget tigerFileTarget = new FileTarget
      {
        Name = "TigerOpenAPI",
        Layout = simpleLayout,
        AutoFlush = true,
        FileName = logDir + @"/tiger_openapi_${date:format=yyyy-MM-dd}.txt",
        CreateDirs = true,
      };
      var consoleTarget = new NLog.Targets.ConsoleTarget
      {
        Name = "LogConsole",
        Layout = simpleLayout
      };

      LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
      loggingConfiguration.AddTarget("tigerFileTarget", tigerFileTarget);

      LoggingRule logRule = new LoggingRule("*", tigerFileTarget);
      logRule.EnableLoggingForLevels(LogLevel.Debug, LogLevel.Fatal);

      loggingConfiguration.LoggingRules.Add(logRule);
      loggingConfiguration.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);

      LogManager.Configuration = loggingConfiguration;
      log = LogManager.GetLogger("TigerOpenAPI");

      Ready = true;
    }

    public static void Debug(string msg, params Object[] args)
    {
      Debug(null, msg, args);
    }

    public static void Debug(Exception? exp, string msg, params Object[] args)
    {
      if (Ready && Enabled && DebugEnabled)
      {
        if (exp == null)
        {
          log.Debug(msg, args);
        }
        else
        {
          log.Debug(exp, msg, args);
        }
      }
    }

    public static void Info(string msg, params Object[] args)
    {
      Info(null, msg, args);
    }

    public static void Info(Exception? exp, string msg, params Object[] args)
    {
      if (Ready && Enabled && InfoEnabled)
      {
        if (exp == null)
        {
          log.Info(msg, args);
        }
        else
        {
          log.Info(exp, msg, args);
        }
      }
    }

    public static void Warn(string msg, params Object[] args)
    {
      Warn(null, msg, args);
    }

    public static void Warn(string msg, Exception? exp)
    {
      Warn(exp, msg);
    }

    public static void Warn(Exception? exp, string msg, params Object[] args)
    {
      if (Ready && Enabled && WarnEnabled)
      {
        if (exp == null)
        {
          log.Warn(msg, args);
        }
        else
        {
          log.Warn(exp, msg, args);
        }
      }
    }

    public static void Error(string msg, params Object[] args)
    {
      Error(null, msg, args);
    }

    public static void Error(string msg, Exception? exp)
    {
      Error(exp, msg);
    }

    public static void Error(Exception? exp, string msg, params Object[] args)
    {
      if (Ready && Enabled && ErrorEnabled)
      {
        if (exp == null)
        {
          log.Error(msg, args);
        }
        else
        {
          log.Error(exp, msg, args);
        }
      }
    }
  }
}

