using PGraph;
using System.Collections.Generic;

namespace ConsoleApp
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      EdgeGraph graph = new EdgeGraph(20);
      graph.AddEdge(1, 4, 10);
      graph.AddEdge(4, 1, 10);
      graph.AddEdge(1, 0, 25);
      graph.AddEdge(0, 1, 25);
      graph.AddEdge(4, 2, 6);
      graph.AddEdge(2, 4, 6);
      graph.AddEdge(4, 6, 23);
      graph.AddEdge(6, 4, 23);
      graph.AddEdge(0, 2, 20);
      graph.AddEdge(2, 0, 20);
      graph.AddEdge(0, 3, 21);
      graph.AddEdge(3, 0, 21);
      graph.AddEdge(3, 5, 25);
      graph.AddEdge(5, 3, 25);
      graph.AddEdge(2, 5, 15);
      graph.AddEdge(5, 2, 15);
      graph.AddEdge(5, 6, 19);
      graph.AddEdge(6, 5, 19);
      graph.AddEdge(5, 7, 20);
      graph.AddEdge(7, 5, 20);
      graph.AddEdge(6, 8, 17);
      graph.AddEdge(8, 6, 17);
      graph.AddEdge(6, 7, 18);
      graph.AddEdge(7, 6, 18);
      graph.AddEdge(7, 8, 20);
      graph.AddEdge(8, 7, 20);
      Dictionary<int, long> result = Dijkstra.HandleEdgeGraphWithPriorityQueue(0, graph);
      foreach (KeyValuePair<int, long> item in result)
      {
        Console.WriteLine($"{item.Key}: {item.Value}");
      }
      Console.WriteLine("==========");
      result = Dijkstra.HandleEdgeGraph(0, graph);
      foreach (KeyValuePair<int, long> item in result)
      {
        Console.WriteLine($"{item.Key}: {item.Value}");
      }
    }
  }
}
