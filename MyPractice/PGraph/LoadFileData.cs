using Newtonsoft.Json;
using System.Diagnostics;

namespace PGraph
{
  public static class LoadFileData
  {
    public static (int, int, int)[] CASE = new (int, int, int)[] {
      (100, 50, 74), (100, 100, 56), (100, 200, 60), (100, 4950, 0),
      (1000, 500, 458), (1000, 1000, 352), (1000, 2000, 771), (1000, 499500, 0),
      (10000, 5000, 7399), (10000, 10000, 0), (10000, 20000, 0), (10000, 49995000, 0)
    };

    public static int[] ThreadCase = new int[] { 7, 9, 10, 11 };

    public static Dictionary<int, Dictionary<int, long>>? GetGraphData(string fileName)
    {
      StreamReader r = new StreamReader($"PGraph/graph-data/{fileName}");
      string json = r.ReadToEnd();
      Dictionary<int, Dictionary<int, long>>? result = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, long>>>(json);
      r.Close();
      return result;
    }

    public static void SaveGraphData(string fileName, Dictionary<int, Dictionary<int, long>> data)
    {
      FileStream fileStream = File.Open($"PGraph/graph-data/{fileName}", FileMode.Append);
      StreamWriter file = new StreamWriter(fileStream);
      string json = JsonConvert.SerializeObject(data, Formatting.Indented);
      file.Write(json);
      file.Close();
    }

    public static void SaveGraphData(string fileName, string data)
    {
      FileStream fileStream = File.Open($"PGraph/graph-data/{fileName}", FileMode.Append);
      StreamWriter file = new StreamWriter(fileStream);
      file.Write(data);
      file.Close();
    }

    public static void SaveCase(int numberOfCase)
    {
      (int, int, int) data = CASE[numberOfCase];
      Console.WriteLine($"nodes: {data.Item1}, edges: {data.Item2}");
      RandomGraphData r = new RandomGraphData(data.Item1);
      if (ThreadCase.Contains(numberOfCase))
      {
        Dictionary<int, Dictionary<int, long>> rawGraph = r.CreateRawGraphThread(data.Item2, 5);
        SaveGraphData($"{data.Item1}_{data.Item2}.json", rawGraph);
      }
      else
      {
        Dictionary<int, Dictionary<int, long>> rawGraph = r.CreateRawGraph(data.Item2);
        SaveGraphData($"{data.Item1}_{data.Item2}.json", rawGraph);
      }
    }

    public static void RenderRandomGraph()
    {
      Stopwatch watch = new Stopwatch();
      for (int i = 0; i <= 11; i++)
      {
        watch.Start();
        LoadFileData.SaveCase(i);
        watch.Stop();
        Console.WriteLine($"Time running case {i + 1}: {watch.ElapsedMilliseconds} ms\n====================");
      }
    }
  }
}
