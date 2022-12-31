using BlockchainInteraction;

namespace ConsoleText
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var etherQuery = new EtherQuery("https://bsc-dataseed.binance.org");
      var result = etherQuery.chainId();
      result.Wait();
      string? abc = result.Result;
      Console.WriteLine(abc);
    }
  }
}