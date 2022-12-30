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

	public class JsonRpcRequest<Params>: BaseJsonRpc
	{
		public string method;
		public Params param;

		public JsonRpcRequest(string id, string method, Params param): base(id)
		{
			this.method = method;
			this.param = param;
		}
    }

	public class JsonRpcResponse: BaseJsonRpc
	{
		public JsonRpcResponse(string id) : base(id) { }
	}

	public class JsonRpcResponse<Result>: JsonRpcResponse
	{
        public JsonRpcResponse(string id) : base(id) { }
    }


    public class JsonRpcSuccess<Result>: JsonRpcResponse<Result>
    {
		public Result result;

		public JsonRpcSuccess(string id, Result result): base(id)
		{
			this.result = result;
		}
    }

	public class JsonRpcError
	{
		public int code;
		public string message;
		public object data;
		public string stack;

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

	public class JsonRpcFailure: JsonRpcResponse
    {
		public JsonRpcError error;

		public JsonRpcFailure(string id, JsonRpcError error): base(id)
		{
			this.error = error;
		}
    }
}

