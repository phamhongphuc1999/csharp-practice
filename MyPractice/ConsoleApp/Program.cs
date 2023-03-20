using MyLibrary.Sorting;
using ConsoleApp.Exercise;

namespace ConsoleApp
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      int[] D = new int[] { 10000, 20000, 50000, 100000 };
      Exercise8 entity = new Exercise8(10, 5, "AB", "CDE");
      Console.WriteLine(entity.Handle());
    }
  }
}
