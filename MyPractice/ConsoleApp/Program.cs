using BlockchainInteraction.ContractHandler;
using MyNumber.NumberBase;

namespace ConsoleText
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      // var contractInterface = new ContractInterface("BlockchainInteraction/abis/BEP20.json");
      // Console.WriteLine(contractInterface.abi.functions.Count);c

      Console.WriteLine(IntConvert.Convert("111111010101110000101010000100001011100011", NumerationSystem.BINARY, NumerationSystem.DECIMAL));
    }
  }
}