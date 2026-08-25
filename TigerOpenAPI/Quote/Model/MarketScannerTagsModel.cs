using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for the <c>market_scanner_tags</c> API.
  ///
  /// Wire shape (per Python <c>MarketScannerParams.to_openapi_dict</c> and
  /// Java <c>MarketScannerTagsModel</c>):
  /// <code>
  /// {
  ///   "market": "US",
  ///   "multi_tag_field_list": ["MultiTagField_Industry","MultiTagField_Concept"]
  /// }
  /// </code>
  ///
  /// Elements serialize as the C# field name of the
  /// <see cref="MultiTagField"/> instance (e.g. <c>MultiTagField_Industry</c>),
  /// which matches Python <c>field_request_name</c> = <c>field_type + '_' +
  /// name</c>. <see cref="EnumNameConverter"/> handles this via
  /// <c>ItemConverterType</c>.
  ///
  /// Previously typed as <c>List&lt;string&gt;</c> with no converter, which
  /// produced <c>biz_content</c> parse errors on the server.
  /// </summary>
  public class MarketScannerTagsModel : ApiModel
  {
    [JsonProperty(PropertyName = "market"),
     Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    /// <summary>
    /// List of <see cref="MultiTagField"/> instances. Serialized as the C#
    /// field name (e.g. <c>MultiTagField_Industry</c>) via
    /// <see cref="EnumNameConverter"/> applied per item.
    /// </summary>
    [JsonProperty(PropertyName = "multi_tag_field_list",
        ItemConverterType = typeof(EnumNameConverter))]
    public List<MultiTagField> MultiTagFieldList { get; set; }

    public MarketScannerTagsModel() : base()
    {
    }
  }
}
