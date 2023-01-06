using System;
namespace TigerOpenAPI.Common.Enum
{
  public enum OrderSortBy
  {
    NONE,
    // Filter by created time and sort descending
    LATEST_CREATED,
    // Filter by status updated time and sort descending
    LATEST_STATUS_UPDATED
  }
}

