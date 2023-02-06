using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class WarrantQuoteModel : ApiModel
  {

    [JsonProperty(PropertyName = "symbols")]
    public List<string> Symbols { get; set; }

    public WarrantQuoteModel() : base()
    {
    }
  }
}

