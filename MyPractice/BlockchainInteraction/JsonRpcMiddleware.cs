namespace BlockchainInteraction
{
  public class RequestRpcMiddlewareReturn<Params>
  {
    public JsonRpcRequest<Params> request;
    public JsonRpcError? error;

    public RequestRpcMiddlewareReturn(JsonRpcRequest<Params> request, JsonRpcError? error)
    {
      this.request = request;
      this.error = error;
    }
  }

  public class ResponseRpcMiddlewareReturn<Params, Result>
  {
    public JsonRpcRequest<Params> request;
    public JsonRpcResponse<Result> response;

    public ResponseRpcMiddlewareReturn(JsonRpcRequest<Params> request, JsonRpcResponse<Result> response)
    {
      this.request = request;
      this.response = response;
    }
  }

  public delegate RequestRpcMiddlewareReturn<Params> RequestRpcMiddleware<Params>(JsonRpcRequest<Params> request);
  public delegate ResponseRpcMiddlewareReturn<Params, Result> ResponseRpcMiddleware<Params, Result>(JsonRpcRequest<Params> request, JsonRpcResponse<Result> response);

  public static class JsonRpcMiddleware
  {
    public static RequestRpcMiddlewareReturn<Params> RunRequestMiddleware<Params>(JsonRpcRequest<Params> request, RequestRpcMiddleware<Params> middleware)
    {
      return middleware(request);
    }

    public static RequestRpcMiddlewareReturn<Params> RunRequestMiddlewareList<Params>(JsonRpcRequest<Params> request, RequestRpcMiddleware<Params>[] middlewares)
    {
      JsonRpcRequest<Params> finalRequest = request;
      JsonRpcError? finalError = null;
      foreach (RequestRpcMiddleware<Params> middleware in middlewares)
      {
        RequestRpcMiddlewareReturn<Params> _result = middleware(finalRequest);
        finalRequest = _result.request;
        if (_result.error != null)
        {
          finalError = _result.error;
          break;
        }
      }
      return new RequestRpcMiddlewareReturn<Params>(finalRequest, finalError);
    }

    public static ResponseRpcMiddlewareReturn<Params, Result> RunResponseMiddleware<Params, Result>(JsonRpcRequest<Params> request, JsonRpcResponse<Result> response, ResponseRpcMiddleware<Params, Result> middleware)
    {
      return middleware(request, response);
    }

    public static ResponseRpcMiddlewareReturn<Params, Result> RunResponseMiddlewareList<Params, Result>(JsonRpcRequest<Params> request, JsonRpcResponse<Result> response, ResponseRpcMiddleware<Params, Result>[] middlewares)
    {
      JsonRpcResponse<Result> finalResponse = response;
      foreach (ResponseRpcMiddleware<Params, Result> middleware in middlewares)
      {
        ResponseRpcMiddlewareReturn<Params, Result> _return = middleware(request, response);
        finalResponse = _return.response;
        if (finalResponse.error != null) break;
      }
      return new ResponseRpcMiddlewareReturn<Params, Result>(request, finalResponse);
    }

    public static RequestRpcMiddleware<Params> MergeRequestMiddleware<Params>(RequestRpcMiddleware<Params>[] middlewares)
    {
      return (JsonRpcRequest<Params> request) =>
      {
        return RunRequestMiddlewareList<Params>(request, middlewares);
      };
    }

    public static ResponseRpcMiddleware<Params, Result> MergeResponseMiddleware<Params, Result>(ResponseRpcMiddleware<Params, Result>[] middlewares)
    {
      return (JsonRpcRequest<Params> request, JsonRpcResponse<Result> response) =>
      {
        return RunResponseMiddlewareList<Params, Result>(request, response, middlewares);
      };
    }
  }
}
