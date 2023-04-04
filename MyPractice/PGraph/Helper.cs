using System.Diagnostics;

namespace PGraph
{
  public static class Helper
  {
    public static void RunCase(int index)
    {
      (int, int, int) caseData = LoadFileData.CASE[index];
      int numberOfNodes = caseData.Item1;
      int numberOfEdges = caseData.Item2;
      int targetNode = caseData.Item3;
      string key = $"{numberOfNodes}_{numberOfEdges}";
      Dictionary<int, Dictionary<int, long>>? data = LoadFileData.GetGraphData($"{key}.json");
      Stopwatch watch = Stopwatch.StartNew();
      long frequency = Stopwatch.Frequency;
      if (data != null)
      {
        EdgeGraph graph = new EdgeGraph(numberOfNodes, data);
        long tick1 = watch.ElapsedTicks;
        graph.HandleDijkstra(targetNode);
        long tick2 = watch.ElapsedTicks;
        Console.WriteLine($"{key}-{targetNode}: {(float)(tick2 - tick1) / frequency / 60} m");

        tick1 = watch.ElapsedTicks;
        graph.HandleDijkstraWithPriorityQueue(targetNode);
        tick2 = watch.ElapsedTicks;
        Console.WriteLine($"{key}-{targetNode} with priority: {(float)(tick2 - tick1) / frequency / 60} m");
        Console.WriteLine("==========================================================");
      }
    }

    public static void RunAllDijkstra()
    {
      int len = LoadFileData.CASE.Length;
      for (int i = 0; i < len; i++) RunCase(i);
    }
  }
}