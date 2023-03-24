using System;
namespace TigerOpenAPI.Common.Enum
{
  public enum SegmentFundStatus
  {
    NONE = 0,
    NEW = 1,   // submitted
    PROC = 2,  // processing
    SUCC = 3,  // success
    FAIL = 4,  // fail
    CANC = 5   // cancelled
  }
}

