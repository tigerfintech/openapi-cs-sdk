using System;
using System.Diagnostics;
using System.Reflection;

namespace TigerOpenAPI.Common.Util
{
  public class SdkVersionUtil
  {
    private static string Prefix = "openapi-cs-sdk-";
    private static string PushPrefix = "csharp-";
    private static string UnknownVersion = "unknown";
    private static string SdkVersion;
    private static string PushSdkVersion;

    private SdkVersionUtil()
    {
    }

    private static string ResolveVersion()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
      string? versionValue = fileVersionInfo.FileVersion;
      if (string.IsNullOrWhiteSpace(versionValue))
      {
        Version? version = assembly.GetName().Version;
        versionValue = version?.ToString();
      }
      return versionValue ?? UnknownVersion;
    }

    public static string GetSdkVersion()
    {
      if (string.IsNullOrWhiteSpace(SdkVersion))
        SdkVersion = Prefix + ResolveVersion();
      return SdkVersion;
    }

    public static string GetPushSdkVersion()
    {
      if (string.IsNullOrWhiteSpace(PushSdkVersion))
        PushSdkVersion = PushPrefix + ResolveVersion();
      return PushSdkVersion;
    }
  }
}

