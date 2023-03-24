using Newtonsoft.Json;

namespace PGraph
{
  public class RandomGraphData
  {
    public int numberOfNode;
    public int maxEdges;

    public RandomGraphData(int numberOfNode)
    {
      this.numberOfNode = numberOfNode;
      this.maxEdges = numberOfNode * (numberOfNode - 1);
    }

    public static string DisplayEdges(Dictionary<(int, int), long> edges)
    {
      string result = "";
      foreach (KeyValuePair<(int, int), long> edge in edges)
      {
        result += $"({edge.Key.Item1}, {edge.Key.Item2}, {edge.Value}), ";
      }
      return result;
    }

    private void CreatePartialEdge(int index, int begin, int end, List<int> source, List<Dictionary<int, long>> store)
    {
      Random random = new Random();
      for (int node = begin; node <= end; node++)
      {
        int numberOfNode2 = source[node];
        // Dictionary<int, long> data = new Dictionary<int, long>();
        for (int i = 0; i < numberOfNode2; i++)
        {
          int node2 = node;
          while (node2 == node || store[node].ContainsKey(node2))
          {
            node2 = random.Next(0, this.numberOfNode);
          }
          long weight = random.NextInt64(1, 10000);
          store[node].Add(node2, weight);
        }
      }
    }

    public List<int> CreateRawEdge(int numberOdEdge, int numberOfThread = 10)
    {
      List<int> result = new List<int>();
      int currentEdge = 0;
      Random random = new Random();
      for (int i = 0; i < this.numberOfNode - 1; i++)
      {
        int remainEdge = numberOdEdge - currentEdge;
        int remainNode = this.numberOfNode - i;
        int minValue = Math.Max(remainEdge - (remainNode - 1) * (this.numberOfNode - 1), 0);
        int maxValue = Math.Min(this.numberOfNode - 1, remainEdge);
        int rEdge = random.Next(minValue, maxValue);
        currentEdge += rEdge;
        result.Add(rEdge);
      }
      result.Add(numberOdEdge - currentEdge);
      List<Dictionary<int, long>> store = new List<Dictionary<int, long>>(this.numberOfNode);
      for (int i = 0; i < this.numberOfNode; i++) store.Add(new Dictionary<int, long>());
      int batchSize = this.numberOfNode / numberOfThread;
      CountdownEvent countdownEvent = new CountdownEvent(numberOfThread);
      for (int i = 0; i < numberOfThread; i++)
      {
        ThreadPool.QueueUserWorkItem((x) =>
        {
          if (x != null)
          {
            int xIndex = (int)x;
            int beginIndex = xIndex * batchSize;
            int endIndex = xIndex * batchSize + batchSize - 1;
            CreatePartialEdge(xIndex + 1, beginIndex, endIndex, result, store);
            Console.WriteLine($"thread {xIndex + 1} finished: {beginIndex} - {endIndex}");
          }
          countdownEvent.Signal();
        }, i);
      }
      ThreadPool.QueueUserWorkItem((x) =>
      {
        int beginIndex = 9 * batchSize;
        CreatePartialEdge(numberOfThread, beginIndex, this.numberOfNode - 1, result, store);
        Console.WriteLine($"thread {numberOfThread} finished: {beginIndex} - {this.numberOfNode - 1}");
        countdownEvent.Signal();
      });
      countdownEvent.Wait();

      int counter = 0;
      foreach (Dictionary<int, long> item in store)
      {
        counter += item.Count();
      }
      string json = JsonConvert.SerializeObject(store, Formatting.Indented);
      File.WriteAllText("path.json", json);
      // Console.WriteLine(json);
      Console.WriteLine($"--f-f-f-f-f-f-f-f----{counter}");
      return result;
    }

    public Dictionary<(int, int), long> CreateEdge(int numberOfEdge)
    {
      Dictionary<(int, int), long> result = new Dictionary<(int, int), long>();
      int targetEdges = Math.Min(numberOfEdge, this.maxEdges);
      Random random = new Random();
      while (result.Count() < targetEdges)
      {
        int node1 = random.Next(0, this.numberOfNode);
        int node2 = node1;
        while (node2 == node1) node2 = random.Next(0, this.numberOfNode);
        if (!result.ContainsKey((node1, node2)))
        {
          long weight = random.NextInt64(1, 10000);
          result.Add((node1, node2), weight);
        }
      }
      return result;
    }

    public EdgeGraph CreateEdgeGraph(Dictionary<(int, int), long> edges)
    {
      Dictionary<int, Dictionary<int, Edge>> graph = new Dictionary<int, Dictionary<int, Edge>>();
      foreach (KeyValuePair<(int, int), long> edge in edges)
      {
        int key = edge.Key.Item1;
        int itemKey = edge.Key.Item2;
        Edge realEdge = new Edge(edge.Key.Item2, edge.Value);
        if (graph.ContainsKey(key)) graph[key].Add(itemKey, realEdge);
        else graph[key] = new Dictionary<int, Edge>() { { itemKey, realEdge } };
      }
      return new EdgeGraph(this.numberOfNode, graph);
    }
  }
}