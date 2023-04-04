using System.Diagnostics;

namespace PGraph
{
  public static class Helper
  {
    public static void RunAllDijkstra()
    {
      Dictionary<string, Dictionary<int, Dictionary<int, long>>> store = new Dictionary<string, Dictionary<int, Dictionary<int, long>>>();
      foreach ((int, int, int) caseData in LoadFileData.CASE)
      {
        int numberOfNodes = caseData.Item1;
        int numberOfEdges = caseData.Item2;
        string key = $"{numberOfNodes}_{numberOfEdges}";
        Dictionary<int, Dictionary<int, long>>? data = LoadFileData.GetGraphData($"{key}.json");
        if (data != null) store.Add(key, data);
      }
      Stopwatch watch = new Stopwatch();
      foreach ((int, int, int) caseData in LoadFileData.CASE)
      {
        int numberOfNodes = caseData.Item1;
        int numberOfEdges = caseData.Item2;
        int targetNode = caseData.Item3;
        string key = $"{numberOfNodes}_{numberOfEdges}";
        Dictionary<int, Dictionary<int, long>>? data = store.GetValueOrDefault(key);
        if (data != null)
        {
          EdgeGraph graph = new EdgeGraph(numberOfNodes, data);
          watch.Restart();
          DijkstraData result = graph.HandleDijkstra(targetNode);
          watch.Stop();
          Console.WriteLine($"{key}: {watch.ElapsedMilliseconds} mn");

          watch.Restart();
          result = graph.HandleDijkstraWithPriorityQueue(targetNode);
          watch.Stop();
          Console.WriteLine($"{key} with priority: {watch.ElapsedMilliseconds} mn");
          Console.WriteLine("==========================================================");
        }
      }
    }
  }
}