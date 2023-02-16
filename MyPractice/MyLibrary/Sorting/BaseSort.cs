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

        private static void Merge<T>(List<T> source, int start, int split, int end, Func<T, T, bool> comparer)
        {
            int lowerLen = split - start + 1;
            int upperLen = end - split;
            T[] lowerArr = new T[lowerLen];
            T[] upperArr = new T[upperLen];
            for (int i = 0; i < lowerLen; i++)
                lowerArr[i] = source[start + i];
            for (int i = 0; i < upperLen; i++)
                upperArr[i] = source[split + 1 + i];
            int lowerCounter = 0;
            int upperCounter = 0;
            int counter = start;
            while (lowerCounter < lowerLen && upperCounter < upperLen)
            {
                T lowerValue = lowerArr[lowerCounter];
                T upperValue = upperArr[upperCounter];
                if (comparer(lowerValue, upperValue))
                {
                    source[counter] = lowerValue;
                    lowerCounter++;

                }
                else
                {
                    source[counter] = upperValue;
                    upperCounter++;
                }
                counter++;
            }
            while (lowerCounter < lowerLen)
            {
                source[counter] = lowerArr[lowerCounter];
                counter++;
                lowerCounter++;
            }
            while (upperCounter < upperLen)
            {
                source[counter] = upperArr[upperCounter];
                counter++;
                upperCounter++;
            }
        }

        private static void RecurrentMergeSort<T>(List<T> source, int start, int end, Func<T, T, bool> comparer)
        {
            if (start < end)
            {
                int split = (start + end) / 2;
                RecurrentMergeSort(source, start, split, comparer);
                RecurrentMergeSort(source, split + 1, end, comparer);
                Merge(source, start, split, end, comparer);
            }
        }

        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> arr = source.ToList();
            int count = arr.Count();
            RecurrentMergeSort(arr, 0, count - 1, comparer);
            return arr;
        }
    }
}