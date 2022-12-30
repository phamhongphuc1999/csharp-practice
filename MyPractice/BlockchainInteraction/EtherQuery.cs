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

    public async Task<string?> chainId()
    {
      object? _result = await this.Send<object, object>(this.CreatePayload("eth_chainId", new object[] { }));
      if (_result != null) return (_result as string);
      return null;
    }
  }
}


