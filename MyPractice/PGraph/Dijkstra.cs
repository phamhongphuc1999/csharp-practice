namespace PGraph
{
  public struct DijkstraData
  {
    public Dictionary<int, long> result;
    public Dictionary<int, int> trace;
    public DijkstraData(Dictionary<int, long> result, Dictionary<int, int> trace)
    {
      this.result = result;
      this.trace = trace;
    }
  }

  public static partial class Dijkstra
  {
    public static int[]? TracePath(int destinationNode, Dictionary<int, int> trace)
    {
      List<int> result = new List<int>() { destinationNode };
      bool isSuccess = false;
      int traceNode = destinationNode;
      while (true)
      {
        if (trace.ContainsKey(traceNode))
        {
          int preNode = trace[traceNode];
          if (preNode == -1)
          {
            isSuccess = true;
            break;
          }
          traceNode = preNode;
          result.Add(preNode);
        }
        else break;
      }
      if (isSuccess)
      {
        int[] realResult = result.ToArray();
        Array.Reverse(realResult);
        return realResult;
      }
      else return null;
    }

    public static DijkstraData HandleDijkstra(this EdgeGraph graph, int targetNode)
    {
      Dictionary<int, long> result = new Dictionary<int, long>() { { targetNode, 0 } };
      Dictionary<int, int> trace = new Dictionary<int, int>() { { targetNode, -1 } };
      int currentMinNode = targetNode;
      List<int> finishedNode = new List<int>();
      bool isNotFinish = true;
      while (isNotFinish)
      {
        finishedNode.Add(currentMinNode);
        Dictionary<int, long>? edges = graph.GetEdge(currentMinNode);
        if (edges == null) isNotFinish = false;
        else
        {
          if (edges.Count == 0) isNotFinish = false;
          foreach (KeyValuePair<int, long> edge in edges)
          {
            if (result.ContainsKey(edge.Key))
            {
              long currentWeight = result[edge.Key];
              long targetWeight = result[currentMinNode] + edge.Value;
              if (currentWeight > targetWeight)
              {
                result[edge.Key] = targetWeight;
                if (trace.ContainsKey(edge.Key)) trace[edge.Key] = currentMinNode;
                else trace.Add(edge.Key, currentMinNode);
              }
            }
            else
            {
              result[edge.Key] = result[currentMinNode] + edge.Value;
              if (trace.ContainsKey(edge.Key)) trace[edge.Key] = currentMinNode;
              else trace.Add(edge.Key, currentMinNode);
            }
          }
          int tempMinNode = currentMinNode;
          long minWeight = Int64.MaxValue;
          foreach (KeyValuePair<int, long> weightData in result)
          {
            if (minWeight > weightData.Value && !finishedNode.Contains(weightData.Key))
            {
              minWeight = weightData.Value;
              currentMinNode = weightData.Key;
            }
          }
          if (currentMinNode == tempMinNode) isNotFinish = false;
        }
      }
      return new DijkstraData(result, trace);
    }

    public static DijkstraData HandleDijkstraWithPriorityQueue(this EdgeGraph graph, int targetNode)
    {
      Dictionary<int, long> result = new Dictionary<int, long>();
      Dictionary<int, int> trace = new Dictionary<int, int>() { { targetNode, -1 } };
      List<int> finishedNode = new List<int>();
      PriorityQueue<int, long> store = new PriorityQueue<int, long>();
      store.Enqueue(targetNode, 0);
      while (store.Count > 0)
      {
        store.TryDequeue(out int node, out long weight);
        if (!finishedNode.Contains(node))
        {
          finishedNode.Add(node);
          result.Add(node, weight);
          Dictionary<int, long>? edges = graph.GetEdge(node);
          if (edges != null)
          {
            if (edges.Count > 0)
            {
              foreach (KeyValuePair<int, long> edge in edges)
              {
                if (result.ContainsKey(edge.Key))
                {
                  long currentWeight = result[edge.Key];
                  long targetWeight = result[node] + edge.Value;
                  if (currentWeight > targetWeight)
                  {
                    store.Enqueue(edge.Key, edge.Value);
                    if (trace.ContainsKey(edge.Key)) trace[edge.Key] = node;
                    else trace.Add(edge.Key, node);
                  }
                }
                else
                {
                  store.Enqueue(edge.Key, result[node] + edge.Value);
                  if (trace.ContainsKey(edge.Key)) trace[edge.Key] = node;
                  else trace.Add(edge.Key, node);
                }
              }
            }
          }
        }
      }
      return new DijkstraData(result, trace);
    }
  }
}