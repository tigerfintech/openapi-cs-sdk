using System;
namespace TigerOpenAPI.Common.Enum
{
  public enum TradeSession
  {
    Regular, PreMarket, AfterHours,
    OverNight, // place overnight order in the US market
    All
  }
}

