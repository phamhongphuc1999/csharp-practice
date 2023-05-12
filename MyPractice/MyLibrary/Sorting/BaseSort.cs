using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.CustomLinq;

namespace MyLibrary.Sorting
{
    public enum PivotType
    {
        HEADER,
        END,
        MEDIUM
    }

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

        public static int Partition<T>(List<T> source, int begin, int end, int pivot, Func<T, T, bool> comparer)
        {
            int low = begin, hight = end;
            T value = source[pivot];
            while (true)
            {
                while (comparer(source[low], value)) low++;
                while (comparer(value, source[hight])) hight--;
                if (low < hight) source.SWAP(low, hight);
                else return hight;
            }
        }

        public static int RandomPartition<T>(List<T> source, int begin, int end, Func<T, T, bool> comparer)
        {
            int low = begin, hight = end;
            Random random = new Random();
            int pivot = random.Next(begin, end);
            return Partition(source, begin, end, pivot, comparer);
        }

        private static void QuickListSort<T>(List<T> source, int begin, int end, PivotType type, Func<T, T, bool> comparer)
        {
            if (begin < end)
            {
                int partition = 0;
                if (type == PivotType.HEADER) partition = Partition(source, begin, end, begin, comparer);
                else if (type == PivotType.END) partition = Partition(source, begin, end, end, comparer);
                else partition = Partition(source, begin, end, (begin + end) / 2, comparer);
                QuickListSort(source, begin, partition, type, comparer);
                QuickListSort(source, partition + 1, end, type, comparer);
            }
        }

        private static void QuickListSort<T>(List<T> source, int begin, int end, Func<T, T, bool> comparer)
        {
            if (begin < end)
            {
                int partition = RandomPartition(source, begin, end, comparer);
                QuickListSort(source, begin, partition, comparer);
                QuickListSort(source, partition + 1, end, comparer);
            }
        }

        public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> source, PivotType type, Func<T, T, bool> comparer)
        {
            List<T> list = source.ToList();
            QuickListSort(list, 0, list.Count - 1, type, comparer);
            return list;
        }

        public static IEnumerable<T> RandomQuickSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> list = source.ToList();
            QuickListSort(list, 0, list.Count - 1, comparer);
            return list;
        }
    }
}
