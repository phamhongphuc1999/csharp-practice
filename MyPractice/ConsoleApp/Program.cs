using PGraph;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      Stopwatch watch = new Stopwatch();
      watch.Start();
      RandomGraphData r = new RandomGraphData(10000);
      List<int> result = r.CreateRawEdge(49995000, 10);
      watch.Stop();
      Console.WriteLine($"Time run: {watch.ElapsedMilliseconds}");
    }
  }
}
