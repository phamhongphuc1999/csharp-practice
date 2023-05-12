namespace BlockchainInteraction
{
  public class BaseJsonRpc
  {
    public string id;
    public const string jsonprc = Constant.JsonRpcVersion2;

    public BaseJsonRpc(string id)
    {
      this.id = id;
    }
  }

  public class JsonRpcRequest<Params> : BaseJsonRpc
  {
    public string method;
    public Params param;

    public JsonRpcRequest(string id, string method, Params param) : base(id)
    {
      this.method = method;
      this.param = param;
    }
  }

  public class JsonRpcError
  {
    public int code;
    public string message;
    public object? data;
    public string? stack;

    public JsonRpcError(int code, string message)
    {
      this.code = code;
      this.message = message;
    }

    public JsonRpcError(int code, string message, object data, string stack)
    {
      this.code = code;
      this.message = message;
      this.data = data;
      this.stack = stack;
    }
  }

  public class JsonRpcResponse<Result> : BaseJsonRpc
  {
    public Result? result;
    public JsonRpcError? error;

    public JsonRpcResponse(string id) : base(id) { }

    public void SetResult(Result result)
    {
      this.result = result;
    }

    public void SetError(JsonRpcError error)
    {
      this.error = error;
    }
  }
}
