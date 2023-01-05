namespace BlockchainInteraction.ContractHandler
{
  public class ParamType
  {
    public string name;
    public string type;
    public string? baseType;
    public bool? indexed;
    public ParamType? arrayChildren;
    public uint? arrayLength;
    public ParamType[]? components;

    public ParamType(string name, string type)
    {
      this.name = name;
      this.type = type;
    }
  }

  public class BaseFragment
  {
    public string name;
    public static string type = "base";
    public ParamType[] inputs;

    public BaseFragment(string name, ParamType[] inputs)
    {
      this.name = name;
      this.inputs = inputs;
    }
  }

  public class ConstructorFragment : BaseFragment
  {
    public static new string type = "constructor";
    public bool? payable;
    public string stateMutability;

    public ConstructorFragment(string name, ParamType[] inputs, string stateMutability) : base(name, inputs)
    {
      this.stateMutability = stateMutability;
    }
  }

  public class FunctionFragment : BaseFragment
  {
    public static new string type = "function";
    public bool? constant;
    public string stateMutability;
    public ParamType[] outputs;

    public FunctionFragment(string name, ParamType[] inputs, ParamType[] outputs, string stateMutability) : base(name, inputs)
    {
      this.outputs = outputs;
      this.stateMutability = stateMutability;
    }
  }

  public class EventFragment : BaseFragment
  {
    public static new string type = "event";
    public bool anonymous;

    public EventFragment(string name, ParamType[] inputs, bool anonymous) : base(name, inputs)
    {
      this.anonymous = anonymous;
    }
  }

  public class ErrorFragment : BaseFragment
  {
    public static new string type = "error";

    public ErrorFragment(string name, ParamType[] inputs) : base(name, inputs) { }
  }

  public class JsonContractAbi
  {
    public ConstructorFragment? constructor;
    public FunctionFragment[] functions;
    public EventFragment[] events;
    public ErrorFragment[] errors;

    public JsonContractAbi(FunctionFragment[] functions, EventFragment[] events, ErrorFragment[] errors)
    {
      this.functions = functions;
      this.events = events;
      this.errors = errors;
    }
  }
}