using System;
namespace TigerOpenAPI.Common.Struct
{
  public class OptionMetrics
  {
    public double Delta { get; set; }
    public double Gamma { get; set; }
    public double Theta { get; set; }
    public double Vega { get; set; }
    public double Rho { get; set; }

    public OptionMetrics()
    {
    }

    public OptionMetrics(double delta, double gamma, double theta, double vega, double rho)
    {
      Delta = delta;
      Gamma = gamma;
      Theta = theta;
      Vega = vega;
      Rho = rho;
    }
  }
}

