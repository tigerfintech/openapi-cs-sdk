using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for futures depth (level 2) quotes
  /// </summary>
  public class FutureDepthModel : FutureContractCodesModel
  {
    public FutureDepthModel() : base()
    {
    }

    public FutureDepthModel(List<string> contractCodes) : base()
    {
      ContractCodes = contractCodes;
    }
  }
}
