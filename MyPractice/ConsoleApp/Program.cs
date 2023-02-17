using MyLibrary.Sorting;
using ConsoleApp.Exercise;

namespace ConsoleApp
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      // int[] a = new int[] { 1, -2, -3, 100, -56, -60, 10, 45, -4 };
      // (int, int, int) result = SimpleExercise.Exercise4(a, false);
      // (int, int, int) result1 = SimpleExercise.Exercise4(a);
      // Console.WriteLine($"start: {result.Item1}, end: {result.Item2}, sum: {result.Item3}");
      // Console.WriteLine($"start: {result1.Item1}, end: {result1.Item2}, sum: {result1.Item3}");

      Console.WriteLine($"nth: {SimpleExercise.Exercise6(1000, false)}");
    }
  }
}
