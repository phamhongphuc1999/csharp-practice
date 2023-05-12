using System.Collections.Concurrent;

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

    private void CreatePartialEdge(int index, int begin, int end, List<int> source, ConcurrentQueue<(int, Dictionary<int, long>)> cp)
    {
      Random random = new Random();
      for (int node = begin; node <= end; node++)
      {
        int numberOfNode2 = source[node];
        Dictionary<int, long> data = new Dictionary<int, long>();
        for (int i = 0; i < numberOfNode2; i++)
        {
          int node2 = node;
          while (node2 == node || data.ContainsKey(node2))
          {
            node2 = random.Next(0, this.numberOfNode);
          }
          long weight = random.NextInt64(1, 10000);
          data.Add(node2, weight);
        }
        cp.Enqueue((node, data));
      }
    }

    public Dictionary<int, Dictionary<int, long>> CreateRawGraphThread(int numberOdEdge, int numberOfThread = 10)
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
      ConcurrentQueue<(int, Dictionary<int, long>)> cq = new ConcurrentQueue<(int, Dictionary<int, long>)>();
      int batchSize = this.numberOfNode / numberOfThread;
      CountdownEvent countdownEvent = new CountdownEvent(numberOfThread);
      for (int i = 0; i < numberOfThread - 1; i++)
      {
        ThreadPool.QueueUserWorkItem((x) =>
        {
          if (x != null)
          {
            int xIndex = (int)x;
            int beginIndex = xIndex * batchSize;
            int endIndex = xIndex * batchSize + batchSize - 1;
            List<int> copyData = result.GetRange(beginIndex, batchSize);
            CreatePartialEdge(xIndex + 1, beginIndex, endIndex, result, cq);
            Console.WriteLine($"thread {xIndex + 1} finished: {beginIndex} - {endIndex}");
          }
          countdownEvent.Signal();
        }, i);
      }
      ThreadPool.QueueUserWorkItem((x) =>
      {
        int beginIndex = (numberOfThread - 1) * batchSize;
        CreatePartialEdge(numberOfThread, beginIndex, this.numberOfNode - 1, result, cq);
        Console.WriteLine($"thread {numberOfThread} finished: {beginIndex} - {this.numberOfNode - 1}");
        countdownEvent.Signal();
      });
      countdownEvent.Wait();
      List<(int, Dictionary<int, long>)> temp = cq.ToList();
      Dictionary<int, Dictionary<int, long>> finalResult = new Dictionary<int, Dictionary<int, long>>();
      foreach ((int, Dictionary<int, long>) item in temp)
      {
        int node = item.Item1;
        Dictionary<int, long> edges = item.Item2;
        if (edges.Count > 0) finalResult[node] = edges;
      }
      return finalResult;
    }

    public Dictionary<int, Dictionary<int, long>> CreateRawGraph(int numberOfEdge)
    {
      Dictionary<(int, int), long> edges = new Dictionary<(int, int), long>();
      int targetEdges = Math.Min(numberOfEdge, this.maxEdges);
      Random random = new Random();
      while (edges.Count() < targetEdges)
      {
        int node1 = random.Next(0, this.numberOfNode);
        int node2 = node1;
        while (node2 == node1) node2 = random.Next(0, this.numberOfNode);
        if (!edges.ContainsKey((node1, node2)))
        {
          long weight = random.NextInt64(1, 10000);
          edges.Add((node1, node2), weight);
        }
      }
      Dictionary<int, Dictionary<int, long>> graph = new Dictionary<int, Dictionary<int, long>>();
      foreach (KeyValuePair<(int, int), long> edge in edges)
      {
        int key = edge.Key.Item1;
        int itemKey = edge.Key.Item2;
        if (graph.ContainsKey(key)) graph[key].Add(itemKey, edge.Value);
        else graph[key] = new Dictionary<int, long>() { { itemKey, edge.Value } };
      }
      return graph;
    }
  }
}
