namespace BlockchainInteraction.ContractHandler
{
  public enum Filter
  {
    ALL, VIEW, WRITE
  }
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
    public ParamType[] inputs;
    public string type;

    public BaseFragment(string name, ParamType[] inputs, string type)
    {
      this.name = name;
      this.inputs = inputs;
      this.type = type;
    }
  }

  public class ConstructorFragment : BaseFragment
  {
    public bool? payable;
    public string stateMutability;

    public ConstructorFragment(string name, ParamType[] inputs, string stateMutability) : base(name, inputs, "constructor")
    {
      this.stateMutability = stateMutability;
    }
  }

  public class FunctionFragment : BaseFragment
  {
    public bool? constant;
    public string stateMutability;
    public ParamType[] outputs;

    public FunctionFragment(string name, ParamType[] inputs, ParamType[] outputs, string stateMutability) : base(name, inputs, "function")
    {
      this.outputs = outputs;
      this.stateMutability = stateMutability;
    }
  }

  public class EventFragment : BaseFragment
  {
    public bool anonymous;

    public EventFragment(string name, ParamType[] inputs, bool anonymous) : base(name, inputs, "event")
    {
      this.anonymous = anonymous;
    }
  }

  public class ErrorFragment : BaseFragment
  {

    public ErrorFragment(string name, ParamType[] inputs) : base(name, inputs, "error") { }
  }

  public class GlobalFragment : BaseFragment
  {
    public bool? payable;
    public string? stateMutability;
    public bool? constant;
    public ParamType[]? outputs;
    public bool? anonymous;

    public GlobalFragment(string name, ParamType[] inputs, string type, bool? payable, string? stateMutability, bool? constant, ParamType[]? outputs, bool? anonymous) : base(name, inputs, type)
    {
      this.payable = payable;
      this.stateMutability = stateMutability;
      this.constant = constant;
      this.outputs = outputs;
      this.anonymous = anonymous;
    }
  }

  public class JsonContractAbi
  {
    public ConstructorFragment? constructor;
    public List<FunctionFragment> functions;
    public List<EventFragment> events;
    public List<ErrorFragment> errors;

    public JsonContractAbi()
    {
      this.functions = new List<FunctionFragment>();
      this.events = new List<EventFragment>();
      this.errors = new List<ErrorFragment>();
    }

    public JsonContractAbi(List<FunctionFragment> functions, List<EventFragment> events, List<ErrorFragment> errors)
    {
      this.functions = functions;
      this.events = events;
      this.errors = errors;
    }
  }
}
