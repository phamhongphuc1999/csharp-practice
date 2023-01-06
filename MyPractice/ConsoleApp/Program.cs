using BlockchainInteraction;
using BlockchainInteraction.ContractHandler;

namespace ConsoleText
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var contractInterface = new ContractInterface("BlockchainInteraction/abis/BEP20.json");
      Console.WriteLine(contractInterface.abi.functions.Count);
    }
  }
}