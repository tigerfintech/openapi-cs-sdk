using System;
using System.Diagnostics;
using System.Reflection;

namespace TigerOpenAPI.Common.Util
{
  public class SdkVersionUtil
  {
    private static string Prefix = "openapi-cs-sdk-";
    private static string UnknownVersion = "unknown";
    private static string SdkVersion;

    private SdkVersionUtil()
    {
    }

    public static string GetSdkVersion()
    {
      if (string.IsNullOrWhiteSpace(SdkVersion))
      {
        Assembly assembly = Assembly.GetExecutingAssembly();
        // AssemblyInfo.cs [assembly: AssemblyFileVersion("1.0.0")]
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        string? versionValue = fileVersionInfo.FileVersion;
        if (string.IsNullOrWhiteSpace(versionValue))
        {
          // [assembly: AssemblyVersion("1.0.0.0")]
          Version? version = assembly.GetName().Version;
          versionValue = version?.ToString();
        }
        SdkVersion = Prefix + versionValue ?? UnknownVersion;
      }

      return SdkVersion;
    }
  }
}

