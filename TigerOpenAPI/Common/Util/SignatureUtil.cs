using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;
namespace TigerOpenAPI.Common.Util
{
  public class SignatureUtil
  {
    SignatureUtil() { }

    public static string Sign(string data, string privateKeyPkcs8, string charset)
    {
      AsymmetricKeyParameter privateKeyParam = PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKeyPkcs8));
      Encoding encoding = string.IsNullOrWhiteSpace(charset) ? Encoding.UTF8 : Encoding.GetEncoding(charset);
      byte[] dataBytes = encoding.GetBytes(data);

      ISigner signer = SignerUtilities.GetSigner(TigerApiConstants.SIGN_ALGORITHMS);
      signer.Init(true, privateKeyParam);
      signer.BlockUpdate(dataBytes, 0, dataBytes.Length);
      return Convert.ToBase64String(signer.GenerateSignature());
    }

    public static bool Verify(string data, string sign, string publicKey, string charset)
    {
      AsymmetricKeyParameter publicKeyParam = PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
      Encoding encoding = string.IsNullOrWhiteSpace(charset) ? Encoding.UTF8 : Encoding.GetEncoding(charset);
      byte[] dataBytes = encoding.GetBytes(data);

      ISigner signer = SignerUtilities.GetSigner(TigerApiConstants.SIGN_ALGORITHMS);
      signer.Init(false, publicKeyParam);
      signer.BlockUpdate(dataBytes, 0, dataBytes.Length);

      byte[] signBytes = Convert.FromBase64String(sign);
      return signer.VerifySignature(signBytes);
    }

    /**
     * get signature content
     */
    public static string GetSignContent(Dictionary<string, object> request)
    {
      StringBuilder content = new StringBuilder();
      List<string> keys = new List<string>(request.Keys);
      keys.Sort();
      int index = 0;
      foreach (string key in keys)
      {
        Object value = request[key];
        string? strValue = value is string s ? s : value?.ToString();
        if (!string.IsNullOrWhiteSpace(strValue))
        {
          content.Append((index == 0 ? string.Empty : "&")).Append(key).Append("=").Append(value);
          index++;
        }
      }
      return content.ToString();
    }
  }
}
