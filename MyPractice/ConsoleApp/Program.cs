using MyLibrary.Sorting;
using ConsoleApp.Exercise;

namespace ConsoleApp
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      int[] a = new int[] { 31, 41, 59, 26, 41, 58, 100, 1000, 1, 0 };
      IEnumerable<int> b = a.SelectionSort((item1, item2) =>
      {
        return item1 < item2;
      });
      foreach (int item in b)
      {
        Console.WriteLine(item);
      }
    }
  }
}
