using Newtonsoft.Json;
using SimpleEncrypt;

namespace BlockchainInteraction.ContractHandler
{
  public class ContractInterface
  {
    public GlobalFragment[]? rawAbi;
    public JsonContractAbi abi;

    public ContractInterface(string abiFile)
    {
      using (StreamReader r = new StreamReader(abiFile))
      {
        string json = r.ReadToEnd();
        GlobalFragment[]? rawAbi = JsonConvert.DeserializeObject<GlobalFragment[]>(json);
        this.rawAbi = rawAbi;
        this.abi = new JsonContractAbi();
        this.FromRawAbi();
      }
    }

    private void FromRawAbi()
    {
      if (this.abi == null) this.abi = new JsonContractAbi();
      if (this.rawAbi != null)
      {
        foreach (GlobalFragment item in this.rawAbi)
        {
          if (item.type == "constructor")
          {
            ParamType[] inputs = item.inputs != null ? item.inputs : new ParamType[] { };
            string stateMutability = item.stateMutability != null ? item.stateMutability : "";
            this.abi.constructor = new ConstructorFragment(item.name, inputs, stateMutability);
          }
          else if (item.type == "function")
          {
            ParamType[] inputs = item.inputs != null ? item.inputs : new ParamType[] { };
            ParamType[] outputs = item.outputs != null ? item.outputs : new ParamType[] { };
            string stateMutability = item.stateMutability != null ? item.stateMutability : "";
            this.abi.functions.Add(new FunctionFragment(item.name, inputs, outputs, stateMutability));
          }
          else if (item.type == "event")
          {
            ParamType[] inputs = item.inputs != null ? item.inputs : new ParamType[] { };
            bool anonymous = true;
            if (item.anonymous != null) anonymous = (bool)item.anonymous;
            this.abi.events.Add(new EventFragment(item.name, inputs, anonymous));
          }
          else
          {
            ParamType[] inputs = item.inputs != null ? item.inputs : new ParamType[] { };
            this.abi.errors.Add(new ErrorFragment(item.name, inputs));
          }
        }
      }
    }

    public List<FunctionFragment>? GetFunctions(Filter filter)
    {
      List<FunctionFragment> functions = this.abi.functions;
      if (filter == Filter.ALL) return functions;
      else if (filter == Filter.VIEW) return functions.FindAll((item) => item.stateMutability == "view").ToList();
      else return functions.FindAll((item) => item.stateMutability != "view").ToList();
    }

    public FunctionFragment? GetFunction(string functionName)
    {
      return this.abi.functions.Find((item) => item.name == functionName);
    }

    public List<EventFragment> GetEvents()
    {
      return this.abi.events;
    }

    public EventFragment? GetEvent(string eventName)
    {
      return this.abi.events.Find((item) => item.name == eventName);
    }

    public List<ErrorFragment> GetErrors()
    {
      return this.abi.errors;
    }

    public ErrorFragment? GetError(string errorName)
    {
      return this.abi.errors.Find((item) => item.name == errorName);
    }

    public string GetFunctionFormat(FunctionFragment fragment)
    {
      List<string> inputs = new List<string>();
      foreach (ParamType input in fragment.inputs)
      {
        inputs.Add(input.type);
      }
      if (inputs.Count > 0)
      {
        string sInputs = string.Join(",", inputs);
        return fragment.name + "(" + sInputs + ")";
      }
      else return fragment.name + "()";
    }

    public string? GetSignature(string functionName)
    {
      FunctionFragment? fragment = this.GetFunction(functionName);
      if (fragment != null) return this.GetSignature(fragment);
      else return null;
    }

    public string? GetSignature(FunctionFragment fragment)
    {
      string functionFormat = this.GetFunctionFormat(fragment);
      return EtherEncrypt.GetKeccak256(functionFormat).Substring(0, 10);
    }
  }
}