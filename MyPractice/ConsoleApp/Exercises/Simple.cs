using MyNumber.Number;

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

    public static (int, int, int) MaxCrossSubArray(int[] arr, int start, int split, int end)
    {
      int lowerIndex = split;
      int upperIndex = split + 1;
      int maxLowerSum = arr[lowerIndex];
      int lowerSum = 0;
      int maxUpperSum = arr[upperIndex];
      int upperSum = 0;
      for (int i = split; i >= 0; i--)
      {
        lowerSum += arr[i];
        if (maxLowerSum < lowerSum)
        {
          maxLowerSum = lowerSum;
          lowerIndex = i;
        }
      }
      for (int i = split + 1; i <= end; i++)
      {
        upperSum += arr[i];
        if (maxUpperSum < upperSum)
        {
          maxUpperSum = upperSum;
          upperIndex = i;
        }
      }
      return (lowerIndex, upperIndex, maxLowerSum + maxUpperSum);
    }

    public static (int, int, int) FindMaxSubArray(int[] arr, int start, int end)
    {
      if (start < end)
      {
        int split = (start + end) / 2;
        (int, int, int) lowResult = FindMaxSubArray(arr, start, split);
        (int, int, int) upResult = FindMaxSubArray(arr, split + 1, end);
        (int, int, int) crossResult = MaxCrossSubArray(arr, start, split, end);
        (int, int, int) result = lowResult;
        if (result.Item3 < upResult.Item3) result = upResult;
        if (result.Item3 < crossResult.Item3) result = crossResult;
        return result;
      }
      return (start, end, arr[start]);
    }

    public static (int, int, int) LinearSubArray(int[] arr)
    {
      (int, int, int) realResult = (0, 0, arr[0]);
      (int, int, int) endResult = (0, 0, arr[0]);
      int len = arr.Length;
      for (int i = 1; i < len; i++)
      {
        if (endResult.Item3 < 0) endResult = (i, i, arr[i]);
        else endResult = (endResult.Item1, i, endResult.Item3 + arr[i]);
        if (realResult.Item3 < endResult.Item3) realResult = endResult;
      }
      return realResult;
    }

    public static (int, int, int) Exercise4(int[] arr, bool isRecurrent = true)
    {
      if (isRecurrent)
      {
        int len = arr.Length;
        return FindMaxSubArray(arr, 0, len - 1);
      }
      return LinearSubArray(arr);
    }

    public static UIntNumber RecurrentFibonacci(int n)
    {
      if (n <= 2) return new UIntNumber("1");
      else return RecurrentFibonacci(n - 1) + RecurrentFibonacci(n - 2);
    }

    public static UIntNumber DynamicFibonacci(int n)
    {
      if (n <= 2) return new UIntNumber("1");
      else
      {
        UIntNumber a1 = new UIntNumber("1");
        UIntNumber a2 = new UIntNumber("1");
        for (int i = 3; i <= n; i++)
        {
          UIntNumber temp = a2;
          a2 = a2 + a1;
          a1 = temp;
        }
        return a2;
      }
    }

    public static UIntNumber Exercise6(int n, bool isRecurrent = true)
    {
      if (isRecurrent) return RecurrentFibonacci(n);
      else return DynamicFibonacci(n);
    }
  }
}