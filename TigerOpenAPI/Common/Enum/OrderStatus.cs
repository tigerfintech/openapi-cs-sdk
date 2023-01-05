using System;
namespace TigerOpenAPI.Common.Enum
{
  public enum OrderStatus
  {
    NONE = 0,
    Invalid = -2,
    Initial = -1,
    PendingCancel = 3,
    Cancelled = 4,
    Submitted = 5,
    Filled = 6,
    Inactive = 7,
    PendingSubmit = 8
  }
}

