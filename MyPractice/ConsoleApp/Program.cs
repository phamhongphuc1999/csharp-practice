using BlockchainInteraction.ContractHandler;
using MyNumber.NumberBase;

namespace ConsoleText
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      // var contractInterface = new ContractInterface("BlockchainInteraction/abis/BEP20.json");
      // Console.WriteLine(contractInterface.abi.functions.Count);

      Console.WriteLine(BaseConvert.ConvertToDecimal("AbCd123FFFFF123970", NumerationSystem.HEXADECIMAL));
    }
  }
}