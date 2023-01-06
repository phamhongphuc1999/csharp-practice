using Org.BouncyCastle.Crypto.Digests;

namespace SimpleEncrypt
{
  public static class EtherEncrypt
  {
    public static string GetKeccak256(string rawTransaction)
    {
      var offset = rawTransaction.StartsWith("0x") ? 2 : 0;
      var txByte = Enumerable.Range(offset, rawTransaction.Length - offset).Where(x => x % 2 == 0).Select(x => Convert.ToByte(rawTransaction.Substring(x, 2), 16)).ToArray();
      var digest = new KeccakDigest(256);
      digest.BlockUpdate(txByte, 0, txByte.Length);
      var calculatedHash = new byte[digest.GetByteLength()];
      digest.DoFinal(calculatedHash, 0);
      var _hash = BitConverter.ToString(calculatedHash, 0, 32).Replace("-", "").ToLower();
      return _hash;
    }
  }
}
