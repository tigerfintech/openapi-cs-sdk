using System;
using Newtonsoft.Json;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionsItem
  {

    [JsonProperty(PropertyName = "items")]
    public List<PositionDetail> Items { get; set; }

    public PositionsItem()
    {
    }
  }
}

