using System;
namespace TigerOpenAPI.Common.Enum
{
  public enum PackageName
  {
    none,

    /**
     * popular stock
     */
    package_popular,

    /**
     * Chinese stocks
     */
    package_china,

    /**
     * ETF
     */
    package_etf,

    /**
     * S&P 500
     */
    package_indices_INX,

    /**
     * Russell 2000
     */
    package_indices_RUT,

    /**
     * hang seng index
     */
    package_HSI,

    /**
     * China HSCEI
     */
    package_HSCEI,

    /**
     * Red Chip Index
     */
    package_HSCCI
  }
}
