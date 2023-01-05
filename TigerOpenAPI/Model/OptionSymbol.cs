using System;
namespace TigerOpenAPI.Model
{
  public class OptionSymbol
  {
    public string Symbol { get; set; }
    public string Expiry { get; set; }
    public string Strike { get; set; }
    public string Right { get; set; }

    public OptionSymbol()
    {
    }
  }
}

