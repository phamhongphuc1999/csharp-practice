namespace BlockchainInteraction
{
  public class AccessList
  {
    public string address;
    public string[] storageKeys;

    public AccessList(string address, string[] storageKeys)
    {
      this.address = address;
      this.storageKeys = storageKeys;
    }
  }

  public class EthTransaction
  {
    public string hash;
    public string type;
    public string nonce;
    public string? from;
    public string? to;
    public string gas;
    public string value;
    public string? maxPriorityFeePerGas;
    public string? maxFeePerGas;
    public string? gasPrice;
    public AccessList[]? accessList;
    public string chainId;
    public string yParity;
    public string? v;
    public string r;
    public string s;

    public EthTransaction(string hash, string type, string nonce, string gas, string value, string chainId, string yParity, string r, string s)
    {
      this.hash = hash;
      this.type = type;
      this.nonce = nonce;
      this.gas = gas;
      this.value = value;
      this.chainId = chainId;
      this.yParity = yParity;
      this.r = r;
      this.s = s;
    }
  }

  public class BlockDataTransaction
  {
    public string[]? sData;
    public EthTransaction[]? eData;

    public BlockDataTransaction(string[] sData)
    {
      this.sData = sData;
    }

    public BlockDataTransaction(EthTransaction[] eData)
    {
      this.eData = eData;
    }
  }
}