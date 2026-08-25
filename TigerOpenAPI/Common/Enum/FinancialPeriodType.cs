using System;

namespace TigerOpenAPI.Common.Enum
{
  /// <summary>
  /// Period type for the <c>financial_report</c> API. Wire values match Java
  /// <c>FinancialPeriodType</c>: <c>Annual</c>, <c>Quarterly</c>, <c>LTM</c>.
  /// Serialized as the enum member name via <c>StringEnumConverter</c>.
  ///
  /// <c>NONE</c> is the default sentinel so the SDK's
  /// <c>DefaultValueHandling.Ignore</c> serializer setting drops the field
  /// when it's unset — mirroring how <see cref="Market"/> / <see cref="SecType"/>
  /// use <c>NONE</c> to distinguish "unset" from a real value. Users must set
  /// this to <c>Annual</c>, <c>Quarterly</c>, or <c>LTM</c> before calling
  /// <c>financial_report</c>; the server rejects requests without period_type.
  /// </summary>
  public enum FinancialPeriodType
  {
    NONE,
    Annual,
    Quarterly,
    LTM
  }
}
