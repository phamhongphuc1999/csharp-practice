namespace ConsoleApp.Exercise
{
  public static class SimpleExercise
  {
    public static int[] Exercise1(int[] A, int[] B, int n)
    {
      int remain = 0;
      int[] result = new int[n + 1];
      for (int i = n - 1; i >= 0; i--)
      {
        int i1 = A[i];
        int i2 = B[i];
        int sum = i1 + i2 + remain;
        if (sum <= 1)
        {
          result[i + 1] = sum;
          remain = 0;
        }
        else if (sum == 2)
        {
          result[i + 1] = 0;
          remain = 1;
        }
        else if (sum == 3)
        {
          result[i + 1] = 1;
          remain = 1;
        }
      }
      if (remain == 1) result[0] = 1;
      else result[0] = 0;
      return result;
    }
  }
}