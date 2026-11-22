using System;
namespace TigerOpenAPI.Common.Enum
{
  public enum TradingSessionType
  {
    PRE_RTH_POST,
    OVERNIGHT,
    RTH,
    FULL,
    PRE_RTH, // Pre-market and regular trading hours
    RTH_POST // Regular and post-market trading hours
  }
}

