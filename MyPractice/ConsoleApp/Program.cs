using PGraph;
using System.Collections.Generic;

namespace ConsoleApp
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      EdgeGraph graph = new EdgeGraph(20);
      graph.AddEdge(1, 2);
      graph.AddEdge(1, 3);
      graph.AddEdge(2, 5);
      graph.AddEdge(3, 2);
      graph.AddEdge(3, 4);
      graph.AddEdge(4, 1);
      graph.AddEdge(5, 6);
      graph.AddEdge(6, 3);
      graph.AddEdge(6, 4);
      graph.AddEdge(6, 8);
      graph.AddEdge(6, 7);
      graph.AddEdge(6, 9);
      graph.AddEdge(7, 9);
      graph.AddEdge(7, 8);
      graph.AddEdge(8, 11);
      graph.AddEdge(9, 10);
      graph.AddEdge(10, 9);
      Console.WriteLine(graph);
      List<int> result = graph.DFS(1);
      foreach (int item in result)
      {
        Console.Write($"{item}, ");
      }
      Console.WriteLine();
      List<int> result1 = graph.BFS(1);
      foreach (int item in result1)
      {
        Console.Write($"{item}, ");
      }
    }
  }
}
