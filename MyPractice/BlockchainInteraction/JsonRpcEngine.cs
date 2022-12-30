using Newtonsoft.Json;

namespace BlockchainInteraction
{
  public class JsonRpcEngine
  {
    public string rpcUrl;
    private HttpClient client;
    private RequestRpcMiddleware<object>[] requestMiddleware;
    private ResponseRpcMiddleware<object, object>[] responseMiddleware;

    public JsonRpcEngine(string rpcUrl)
    {
      this.rpcUrl = rpcUrl;
      this.client = new HttpClient();
      this.requestMiddleware = new RequestRpcMiddleware<object>[] { };
      this.responseMiddleware = new ResponseRpcMiddleware<object, object>[] { };
    }

    public void AddRequestMiddleware<Params>(RequestRpcMiddleware<Params> middleware)
    {
      RequestRpcMiddleware<object>? _temp = middleware as RequestRpcMiddleware<object>;
      if (_temp != null) this.requestMiddleware.Append(_temp);
    }

    public void AddResponseMiddleware<Params, Result>(ResponseRpcMiddleware<Params, Result> middleware)
    {
      ResponseRpcMiddleware<object, object>? _temp = middleware as ResponseRpcMiddleware<object, object>;
      if (_temp != null) this.responseMiddleware.Append(_temp);
    }

    private async Task<JsonRpcResponse<object>> fetch<Params>(JsonRpcRequest<Params> request)
    {
      string json = JsonConvert.SerializeObject(request);
      StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
      var res = await this.client.PostAsync(this.rpcUrl, httpContent);
      string _result = "" + res.Content + " : " + res.StatusCode;
      var data = new JsonRpcResponse<object>(request.id);
      data.SetResult(_result);
      return data;
    }

    public async Task<object?> Handle<Params, Result>(JsonRpcRequest<Params> request, Action<JsonRpcResponse<object>>? callback)
    {
      JsonRpcRequest<object>? objectRequest = (request as JsonRpcRequest<object>);
      if (objectRequest != null)
      {
        RequestRpcMiddlewareReturn<object> requestResult = JsonRpcMiddleware.RunRequestMiddlewareList<object>(objectRequest, this.requestMiddleware);
        if (requestResult.error != null) throw new Exception("RPC engine fail");
        JsonRpcResponse<object> _result = await this.fetch<object>(requestResult.request);
        ResponseRpcMiddlewareReturn<object, object> responseResult = JsonRpcMiddleware.RunResponseMiddlewareList<object, object>(objectRequest, _result, this.responseMiddleware);
        if (callback != null) callback(responseResult.response);
        return responseResult.response.result;
      }
      return null;
    }
  }
}

