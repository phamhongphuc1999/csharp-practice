using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.ArangeAlgorithm
{
    public static partial class CoreAlgorithm
    {
        public static IEnumerable<T> BubbleSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> arr = source.ToList();
            int count = source.Count();
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (!comparer(arr[i], arr[j]))
                    {
                        T temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            return arr;
        }

        public static int PartitionHeader<T>(List<T> source, int low, int hight, Func<T, T, bool> comparer)
        {
            T pivot = source[low];
            int left = low;
            int right = hight - 1;
            while (true)
            {
                while (comparer(source[left], pivot) && (left <= right)) left++;
                while (comparer(pivot, source[right]) && (right >= left)) right--;
                if (left >= right) break;
                T temp = source[left];
                source[left] = source[right];
                source[right] = temp;
            }
            T temp1 = source[low];
            source[low] = source[right];
            source[right] = temp1;
            return left;

        }

        //public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer, Config.QuickSortPivot pivot)
        //{

        //}
    }
}
