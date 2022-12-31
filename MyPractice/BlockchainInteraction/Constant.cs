namespace BlockchainInteraction
{
  public static class Constant
  {
    public const string JsonRpcVersion2 = "2.0";

    public static readonly string[] RETRIABLE_ERRORS = new string[] { "Gateway timeout", "ETIMEDOUT", "failed to parse response body", "Failed to fetch" };
  }

  public static class BlockTag
  {
    public const string Earliest = "earliest";
    public const string Finalized = "finalized";
    public const string Safe = "safe";
    public const string Latest = "latest";
    public const string Pending = "pending";
  }

  public static class EthRequest
  {
    public const string getBlockByHash = "eth_getBlockByHash";
    public const string getBlockByNumber = "eth_getBlockByNumber";
    public const string getBlockTransactionCountByHash = "eth_getBlockTransactionCountByHash";
    public const string getBlockTransactionCountByNumber = "eth_getBlockTransactionCountByNumber";
    public const string getUncleCountBlockHash = "eth_getUncleCountBlockHash";
    public const string getUncleCountBlockNumber = "eth_getUncleCountBlockNumber";
    public const string chainId = "eth_chainId";
    public const string syncing = "eth_syncing";
    public const string coinbase = "eth_coinbase";
    public const string accounts = "eth_accounts";
    public const string blockNumber = "eth_blockNumber";
    public const string call = "eth_call";
    public const string estimateGas = "eth_estimateGas";
    public const string createAccessList = "eth_createAccessList";
    public const string gasPrice = "eth_gasPrice";
    public const string maxPriorityFeePerGas = "eth_maxPriorityFeePerGas";
    public const string feeHistory = "eth_feeHistory";
    public const string newFilter = "eth_newFilter";
    public const string newBlockFilter = "eth_newBlockFilter";
    public const string newPendingTransactionFilter = "eth_newPendingTransactionFilter";
    public const string uninstallFilter = "eth_uninstallFilter";
    public const string getFilterChanges = "eth_getFilterChanges";
    public const string getFilterLogs = "eth_getFilterLogs";
    public const string getLogs = "eth_getLogs";
    public const string mining = "eth_mining";
    public const string hashrate = "eth_hashrate";
    public const string getWork = "eth_getWork";
    public const string submitWork = "eth_submitWork";
    public const string submitHashrate = "eth_submitHashrate";
    public const string sign = "eth_sign";
    public const string signTransaction = "eth_signTransaction";
    public const string getBalance = "eth_getBalance";
    public const string getStorageAt = "eth_getStorageAt";
    public const string getTransactionCount = "eth_getTransactionCount";
    public const string getCode = "eth_getCode";
    public const string getProof = "eth_getProof";
    public const string sendTransaction = "eth_sendTransaction";
    public const string sendRawTransaction = "eth_sendRawTransaction";
    public const string getTransactionByHash = "eth_getTransactionByHash";
    public const string getTransactionByBlockHashAndIndex = "eth_getTransactionByBlockHashAndIndex";
    public const string getTransactionByBlockNumberAndIndex = "eth_getTransactionByBlockNumberAndIndex";
    public const string getTransactionReceipt = "eth_getTransactionReceipt";
  }
}
