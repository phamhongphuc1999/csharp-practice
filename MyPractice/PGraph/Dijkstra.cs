using System;
using System.Collections.Generic;

namespace PGraph
{
  public static class Dijkstra
  {
    public static Dictionary<int, long> HandleEdgeGraph(int targetNode, EdgeGraph graph)
    {
      Dictionary<int, long> result = new Dictionary<int, long>() { { targetNode, 0 } };
      int currentMinNode = targetNode;
      List<int> finishedNode = new List<int>();
      bool isNotFinish = true;
      while (isNotFinish)
      {
        finishedNode.Add(currentMinNode);
        Dictionary<int, Edge>? edges = graph.GetEdge(currentMinNode);
        if (edges == null) isNotFinish = false;
        else
        {
          if (edges.Count == 0) isNotFinish = false;
          foreach (KeyValuePair<int, Edge> edge in edges)
          {
            if (result.ContainsKey(edge.Key))
            {
              long currentWeight = result[edge.Key];
              long targetWeight = result[currentMinNode] + edge.Value.weight;
              result[edge.Key] = Math.Min(currentWeight, targetWeight);
            }
            else result[edge.Key] = result[currentMinNode] + edge.Value.weight;
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
      return result;
    }

    public static Dictionary<int, long> HandleEdgeGraphWithPriorityQueue(int targetNode, EdgeGraph graph)
    {
      Dictionary<int, long> result = new Dictionary<int, long>();
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
          Dictionary<int, Edge>? edges = graph.GetEdge(node);
          if (edges != null)
          {
            if (edges.Count > 0)
            {
              foreach (KeyValuePair<int, Edge> edge in edges)
              {
                if (result.ContainsKey(edge.Key))
                {

                  long currentWeight = result[edge.Key];
                  long targetWeight = result[node] + edge.Value.weight;
                  if (currentWeight > targetWeight) store.Enqueue(edge.Key, edge.Value.weight);
                }
                else store.Enqueue(edge.Key, result[node] + edge.Value.weight);
              }
            }
          }
        }
      }
      return result;
    }
  }
}