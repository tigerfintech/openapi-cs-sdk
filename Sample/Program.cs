using Newtonsoft.Json;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;

class Program
{
  static async Task Main(string[] args)
  {
    Console.WriteLine("Hello, World!");
    TigerConfig.LogDir = "/data0/logs/tiger-openapi-cs";
    ApiLogger.Info("start");

    // tiger config
    TigerConfig config = new TigerConfig()
    {
      License = License.TBNZ,// must
      TigerId = "20150899", // must
      DefaultAccount = "572386",// (optional) paper account: 20200821144442583
      PrivateKey = TigerConfig.ReadPrivateKey("/Users/tiger/dev/liutp_account/rsa_private_key_pkcs8_prod.pem"),// must
      FailRetryCounts = 2, // (optional) range:[1, 5],  default is 2
      AutoGrabPermission = true,   // (optional) default is true
      Language = Language.en_US,   // (optional) default is en_US
      TimeZone = DateUtil.HK_ZONE  // (optional) default is HK_ZONE
    };
    QuoteClient quoteClient = new QuoteClient(config);

    // QuoteApiService.USER_LICENSE
    TigerResponse? response = await testQuoteUserLicenseAsync(quoteClient);

    ApiLogger.Info("response:" + JsonConvert.SerializeObject(response));
    Thread.Sleep(2000);
    ApiLogger.Info("end");
  }

  static async Task<TigerDictResponse?> testQuoteUserLicenseAsync(QuoteClient quoteClient)
  {
    TigerRequest<TigerDictResponse> request = new TigerRequest<TigerDictResponse>()
    {
      ApiMethodName = QuoteApiService.USER_LICENSE
    };
    return await quoteClient.ExecuteAsync(request);
  }
}