// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.CustomLinq;

namespace MyLibrary.Sort
{
    public static partial class CommonSort
    {
        public static IEnumerable<T> BubbleSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> arr = source.ToList();
            int count = source.Count();
            for (int i = 0; i < count - 1; i++)
                for (int j = i + 1; j < count; j++)
                    if (!comparer(arr[i], arr[j])) arr.SWAP(i, j);
            return arr;
        }

        public static IEnumerable<T> InsertSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> arr = source.ToList();
            int count = source.Count();
            for (int i = 1; i < count; i++)
            {
                T value = arr[i];
                int index = i - 1;
                while (index >= 0 && comparer(value, arr[index]))
                {
                    arr[index + 1] = arr[index];
                    index--;
                }
                arr[index] = value;
            }
            return arr;
        }

        public static IEnumerable<T> SelectSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> arr = source.ToList();
            int count = source.Count();
            for (int i = 0; i < count - 1; i++)
            {
                int key = i;
                for (int j = i + 1; j < count; j++)
                    if (comparer(arr[j], arr[i])) key = j;
                if (key != i) arr.SWAP(i, key);
            }
            return arr;
        }

        public static int Partition<T>(List<T> source, int begin, int end, int pivot, Func<T, T, bool> comparer)
        {
            int low = begin, hight = end;
            T value = source[pivot];
            while (low <= hight)
            {
                while (comparer(source[low], value)) low++;
                while (comparer(value, source[hight])) hight--;
                source.SWAP(low, hight);
                low++; hight--;
            }
            source.SWAP(low, hight);
            return hight;
        }

        public static int RandomPartition<T>(List<T> source, int begin, int end, Func<T, T, bool> comparer)
        {
            int low = begin, hight = end;
            Random random = new Random();
            int pivot = random.Next(begin, end);
            T value = source[pivot];
            while (low <= hight)
            {
                while (comparer(source[low], value)) low++;
                while (comparer(value, source[hight])) hight--;
                source.SWAP(low, hight);
            }
            source.SWAP(low, hight);
            return hight;
        }

        private static void QuickListSort<T>(List<T> source, int begin, int end, Config.PivotType type, Func<T, T, bool> comparer)
        {
            if (begin < end)
            {
                int partition = 0;
                if (type == Config.PivotType.HEADER) partition = Partition(source, begin, end, begin, comparer);
                else if (type == Config.PivotType.END) partition = Partition(source, begin, end, end, comparer);
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

        public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> source, Config.PivotType type, Func<T, T, bool> comparer)
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

        private static void Merge<T>(List<T> source, int begin, int end, int splip, Func<T, T, bool> comparer)
        {
            int lowLength = splip - begin + 1;
            int hightLength = end - splip;
            T[] low = new T[lowLength];
            T[] hight = new T[hightLength];
            for (int i = 0; i < lowLength; i++)
                low[i] = source[i];
            for (int i = 0; i < hightLength; i++)
                hight[i] = source[i + splip];
            int cLow = 0, cHight = 0, cSource = begin;
            while (cLow < lowLength && cHight < hightLength)
            {
                if (comparer(low[cLow], hight[cHight])) source[cSource++] = low[cLow++];
                else source[cSource++] = hight[cHight++];
            }
            while (cLow < lowLength) source[cSource++] = low[cLow++];
            while (cHight < hightLength) source[cSource++] = hight[cHight++];
        }

        private static void MergeListSort<T>(List<T> source, int begin, int end, Func<T, T, bool> comparer)
        {
            if (begin < end - 1)
            {
                int splip = (begin + end) / 2;
                MergeListSort(source, begin, splip, comparer);
                MergeListSort(source, splip + 1, end, comparer);
                Merge(source, begin, end, splip, comparer);
            }
        }

        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> list = source.ToList();
            MergeListSort<T>(list, 0, list.Count - 1, comparer);
            return list;
        }
    }
}
