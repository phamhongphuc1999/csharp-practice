namespace BlockchainInteraction
{
  public class EtherQuery
  {
    public string rpcUrl;
    private JsonRpcEngine engine;
    private ulong idCounter;
    private ulong max;

    public EtherQuery(string rpcUrl)
    {
      this.rpcUrl = rpcUrl;
      this.engine = new JsonRpcEngine(rpcUrl);
      this.max = UInt64.MaxValue;
      Random rn = new Random();
      this.idCounter = (ulong)rn.NextInt64(0, Int64.MaxValue);
    }

    public EtherQuery(string rpcUrl, ulong idCounter, ulong max)
    {
      this.rpcUrl = rpcUrl;
      this.engine = new JsonRpcEngine(rpcUrl);
      this.idCounter = idCounter;
      this.max = max;
    }

    public void AddRequestMiddleware<Params>(RequestRpcMiddleware<Params> middleware)
    {
      this.engine.AddRequestMiddleware(middleware);
    }

    public void AddResponseMiddleware<Params, Result>(ResponseRpcMiddleware<Params, Result> middleware)
    {
      this.engine.AddResponseMiddleware(middleware);
    }

    private JsonRpcRequest<object> CreatePayload(string method, object data)
    {
      ulong _counter = this.idCounter % this.max;
      this.idCounter = _counter + 1;
      return new JsonRpcRequest<object>(this.idCounter.ToString(), method, data);
    }

    private async Task<object?> Send<Params, Result>(JsonRpcRequest<Params> request)
    {
      return await this.engine.Handle<Params, Result>(request, null);
    }

    public async Task<EthBlock?> getBlockByHash(string blockHash, bool hydratedTransaction)
    {
      JsonRpcRequest<object> _data = this.CreatePayload(EthRequest.getBlockByHash, new object[] { blockHash, hydratedTransaction });
      object? _result = await this.Send<object, object>(_data);
      if (_result != null) return (_result as EthBlock);
      return null;
    }

    public async Task<EthBlock?> getBlockByNumber(string blockNumber = BlockTag.Latest, bool hydratedTransaction = false)
    {
      JsonRpcRequest<object> _data = this.CreatePayload(EthRequest.getBlockByNumber, new object[] { blockNumber, hydratedTransaction });
      object? _result = await this.Send<object, object>(_data);
      if (_result != null) return (_result as EthBlock);
      return null;
    }

    public async Task<string?> getBlockTransactionCountByHash(string blockHash)
    {
      JsonRpcRequest<object> _data = this.CreatePayload(EthRequest.getBlockTransactionCountByHash, new object[] { blockHash });
      object? _result = await this.Send<object, object>(_data);
      if (_result != null) return (_result as string);
      return null;
    }

    public async Task<string?> getBlockTransactionCountByNumber(string blockNumber = BlockTag.Latest)
    {
      JsonRpcRequest<object> _data = this.CreatePayload(EthRequest.getBlockTransactionCountByNumber, new object[] { blockNumber });
      object? _result = await this.Send<object, object>(_data);
      if (_result != null) return (_result as string);
      return null;
    }

    public async Task<string?> chainId()
    {
      object? _result = await this.Send<object, object>(this.CreatePayload(EthRequest.chainId, new object[] { }));
      if (_result != null) return (_result as string);
      return null;
    }
  }
}


