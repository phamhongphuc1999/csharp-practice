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

  public class EthBlock
  {
    public string parentHash;
    public string sha3Uncles;
    public string miner;
    public string stateRoot;
    public string transactionsRoot;
    public string receiptRoot;
    public string logsBloom;
    public string? difficulty;
    public string number;
    public string gasLimit;
    public string gasUsed;
    public string timestamp;
    public string extraData;
    public string mixHash;
    public string nonce;
    public string? totalDifficulty;
    public string? baseFeePerGas;
    public string size;
    public BlockDataTransaction transactions;
    public string[]? uncle;
    public string hash;

    public EthBlock(string parentHash, string sha3Uncles, string miner, string stateRoot, string transactionsRoot, string receiptRoot, string logsBloom, string number,
    string gasLimit, string gasUsed, string timestamp, string extraData, string mixHash, string nonce, string size, BlockDataTransaction transactions, string hash)
    {
      this.parentHash = parentHash;
      this.sha3Uncles = sha3Uncles;
      this.miner = miner;
      this.stateRoot = stateRoot;
      this.transactionsRoot = transactionsRoot;
      this.receiptRoot = receiptRoot;
      this.logsBloom = logsBloom;
      this.number = number;
      this.gasLimit = gasLimit;
      this.gasUsed = gasUsed;
      this.timestamp = timestamp;
      this.extraData = extraData;
      this.mixHash = mixHash;
      this.nonce = nonce;
      this.size = size;
      this.transactions = transactions;
      this.hash = hash;
    }
  }
}