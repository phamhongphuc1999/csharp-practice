using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.CustomLinq;

namespace MyLibrary.Sorting
{
    public static partial class BaseSort
    {
        public static IEnumerable<T> BubbleSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer, bool isReverse)
        {
            List<T> arr = source.ToList();
            int count = arr.Count();
            for (int i = 0; i < count - 1; i++)
                for (int j = i + 1; j < count; j++)
                {
                    bool _compare = comparer(arr[i], arr[j]);
                    bool check = (isReverse && _compare) || (!isReverse && !_compare);
                    if (check) arr.SWAP(i, j);
                }
            return arr;
        }

        public static IEnumerable<T> InsertionSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> arr = source.ToList();
            int count = arr.Count();
            for (int i = 1; i < count; i++)
            {
                T value = arr[i];
                int j = i - 1;
                while (j >= 0 && !comparer(arr[j], value))
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = value;
            }
            return arr;
        }

        public static IEnumerable<T> SelectionSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> arr = source.ToList();
            int count = arr.Count();
            for (int i = 0; i < count - 1; i++)
            {
                int sIndex = i;
                for (int j = i + 1; j < count; j++)
                    if (!comparer(arr[j], arr[sIndex])) sIndex = j;
                arr.SWAP(i, sIndex);
            }
            return arr;
        }
    }
}