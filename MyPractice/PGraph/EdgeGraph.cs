using System;
using System.Collections.Generic;

namespace PGraph
{
  public struct Edge
  {
    public int node;
    public long weight;

    public Edge(int node, long weight)
    {
      this.node = node;
      this.weight = weight;
    }
  }

  public class EdgeGraph
  {
    protected int nodeLimit;
    protected Dictionary<int, Dictionary<int, Edge>> graph;

    public EdgeGraph(int nodeLimit)
    {
      this.nodeLimit = nodeLimit;
      this.graph = new Dictionary<int, Dictionary<int, Edge>>();
    }

    public EdgeGraph(int nodeLimit, Dictionary<int, Dictionary<int, Edge>> list)
    {
      this.nodeLimit = nodeLimit;
      int len = list.Count;
      int counter = len < nodeLimit ? len : nodeLimit;
      this.graph = new Dictionary<int, Dictionary<int, Edge>>();
      for (int i = 0; i < counter; i++)
      {
        Dictionary<int, Edge> edges = list[i];
        foreach (KeyValuePair<int, Edge> edge in edges)
        {
          if (edge.Key < this.nodeLimit && edge.Value.node < this.nodeLimit) this.graph[edge.Key].Add(edge.Value.node, edge.Value);
        }
      }
    }

    public void AddEdge(int node, Edge edge)
    {
      if (node < this.nodeLimit && edge.node < this.nodeLimit)
      {
        bool isContain = this.graph.ContainsKey(node);
        if (isContain) this.graph[node][edge.node] = edge;
        else this.graph.Add(node, new Dictionary<int, Edge>() { { edge.node, edge } });
      }
    }

    public void AddEdge(int target, int node, long weight = 0)
    {
      this.AddEdge(target, new Edge(node, weight));
    }

    public void RemoveEdge(int node, int edge)
    {
      bool isContain = this.graph.ContainsKey(node);
      if (isContain) this.graph[node].Remove(edge);
    }

    public List<int> DFS(int startNode)
    {
      if (startNode < this.nodeLimit)
      {
        List<int> result = new List<int>();
        List<int> visited = new List<int>() { startNode };
        Stack<int> store = new Stack<int>();
        store.Push(startNode);
        while (store.Count > 0)
        {
          int item = store.Pop();
          result.Add(item);
          try
          {
            Dictionary<int, Edge>.KeyCollection nodes = this.graph[item].Keys;
            foreach (int node in nodes)
            {
              if (!visited.Contains(node))
              {
                store.Push(node);
                visited.Add(node);
              }
            }
          }
          catch { }
        }
        return result;
      }
      else return new List<int>();
    }

    public List<int> BFS(int startNode)
    {
      if (startNode < this.nodeLimit)
      {
        List<int> result = new List<int>();
        List<int> visited = new List<int>() { startNode };
        Queue<int> store = new Queue<int>();
        store.Enqueue(startNode);
        while (store.Count > 0)
        {
          int item = store.Dequeue();
          result.Add(item);
          try
          {
            Dictionary<int, Edge>.KeyCollection nodes = this.graph[item].Keys;
            foreach (int node in nodes)
            {
              if (!visited.Contains(node))
              {
                store.Enqueue(node);
                visited.Add(node);
              }
            }
          }
          catch { }
        }
        return result;
      }
      else return new List<int>();
    }

    public override string ToString()
    {
      string result = "";
      foreach (KeyValuePair<int, Dictionary<int, Edge>> nodeData in this.graph)
      {
        int nodeIndex = nodeData.Key;
        Dictionary<int, Edge> edges = nodeData.Value;
        result += $"node {nodeIndex}: ";
        foreach (KeyValuePair<int, Edge> edge in edges)
        {
          result += $"({edge.Key}, {edge.Value.weight}), ";
        }
        result += "\n";
      }
      int len = result.Length;
      if (len >= 3) return result.Substring(0, result.Length - 3);
      else return result;
    }
  }
}