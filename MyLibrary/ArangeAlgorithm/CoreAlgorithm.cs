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
            return source;
        }

        //public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer, Config.QuickSortPivot pivot)
        //{

        //}
    }
}
