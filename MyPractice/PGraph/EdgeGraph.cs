namespace PGraph
{
  public class EdgeGraph
  {
    public int nodeLimit;
    public Dictionary<int, Dictionary<int, long>> graph { get; private set; }

    public EdgeGraph(int nodeLimit)
    {
      this.nodeLimit = nodeLimit;
      this.graph = new Dictionary<int, Dictionary<int, long>>();
    }

    public EdgeGraph(int nodeLimit, Dictionary<int, Dictionary<int, long>> list, bool isCheck = true)
    {
      if (isCheck)
      {
        this.nodeLimit = nodeLimit;
        int len = list.Count;
        Dictionary<int, Dictionary<int, long>> _graph = new Dictionary<int, Dictionary<int, long>>();
        for (int i = 0; i < this.nodeLimit; i++)
        {
          if (list.ContainsKey(i))
          {
            _graph[i] = new Dictionary<int, long>();
            Dictionary<int, long> edges = list[i];
            foreach (KeyValuePair<int, long> edge in edges)
            {
              if (edge.Key < this.nodeLimit) _graph[i].Add(edge.Key, edge.Value);
            }
          }
        }
        this.graph = _graph;
      }
      else this.graph = list;
    }

    public Dictionary<int, long>? GetEdge(int node)
    {
      if (this.graph.ContainsKey(node)) return this.graph[node];
      return null;
    }

    public void AddEdge(int target, int node, long weight = 0)
    {
      if (target < this.nodeLimit && node < this.nodeLimit)
      {
        bool isContain = this.graph.ContainsKey(target);
        if (isContain) this.graph[target][node] = weight;
        else this.graph.Add(target, new Dictionary<int, long>() { { node, weight } });
      }
    }

    public void AddBothEdges(int node1, int node2, long weight = 0)
    {
      this.AddEdge(node1, node2, weight);
      this.AddEdge(node2, node1, weight);
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
            Dictionary<int, long>.KeyCollection nodes = this.graph[item].Keys;
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
            Dictionary<int, long>.KeyCollection nodes = this.graph[item].Keys;
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
      foreach (KeyValuePair<int, Dictionary<int, long>> nodeData in this.graph)
      {
        int nodeIndex = nodeData.Key;
        Dictionary<int, long> edges = nodeData.Value;
        result += $"node {nodeIndex}: ";
        foreach (KeyValuePair<int, long> edge in edges)
        {
          result += $"({edge.Key}, {edge.Value}), ";
        }
        result += "\n";
      }
      int len = result.Length;
      if (len >= 3) return result.Substring(0, result.Length - 3);
      else return result;
    }
  }
}
