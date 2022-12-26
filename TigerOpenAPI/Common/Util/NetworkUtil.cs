using System;
using System.Net.NetworkInformation;
namespace TigerOpenAPI.Common.Util
{
  public class NetworkUtil
  {
    private const string GET_DEVICE_ERROR = "please check network connection";
    private const int MAC_ARRAY_LENGTH = 6;

    private NetworkUtil()
    {
    }

    public static string GetDeviceId()
    {
      List<string> macs = new List<string>();
      try
      {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface ni in interfaces)
        {
          if (OperationalStatus.Up != ni.OperationalStatus
            || ni.NetworkInterfaceType == NetworkInterfaceType.Unknown
            || ni.NetworkInterfaceType == NetworkInterfaceType.Loopback
            || ni.NetworkInterfaceType == NetworkInterfaceType.Ppp)
          {
            continue;
          }
          string macAddress = ni.GetPhysicalAddress().ToString();
          if (macAddress.Length == 12)
          {
            for (int i = 1; i < MAC_ARRAY_LENGTH; i++)
            {
              macAddress = macAddress.Insert(3 * i - 1, ":");
            }
            macs.Add(macAddress);
          }
        }
      }
      catch (Exception e)
      {
        ApiLogger.Error("GetPhysicalAddress failed. {}, {}", e.Message, GET_DEVICE_ERROR);
      }
      return macs.FirstOrDefault() ?? "unknown";
    }
  }
}