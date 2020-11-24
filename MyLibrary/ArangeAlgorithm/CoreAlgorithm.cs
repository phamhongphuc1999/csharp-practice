// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.ArangeAlgorithm
{
    public static partial class CoreAlgorithm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="low"></param>
        /// <param name="hight"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private static int PartitionHeader<T>(List<T> source, int low, int hight, Func<T, T, bool> comparer)
        {
            int left = low + 1;
            int right = hight;
            T partition = source[low];
            while(true)
            {
                while (left <= hight && comparer(source[left], partition)) left++;
                while (right >= low && comparer(partition, source[right])) right--;
                if (left < right)
                {
                    T temp = source[left];
                    source[left] = source[right];
                    source[right] = temp;
                    left++; right--;
                }
                else break;
            }
            T temp1 = source[right];
            source[right] = source[low];
            source[low] = temp1;
            return right;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="low"></param>
        /// <param name="hight"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private static int PartitionEnd<T>(List<T> source, int low, int hight, Func<T, T, bool> comparer)
        {
            int left = low;
            int right = hight - 1;
            T partition = source[hight];
            while (true)
            {
                while (left <= hight && comparer(source[left], partition)) left++;
                while (right >= low && comparer(partition, source[right])) right--;
                if (left < right)
                {
                    T temp = source[left];
                    source[left] = source[right];
                    source[right] = temp;
                    left++; right--;
                }
                else break;
            }
            T temp1 = source[left];
            source[left] = source[hight];
            source[hight] = temp1;
            return left;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="low"></param>
        /// <param name="hight"></param>
        /// <param name="comparer"></param>
        /// <param name="pivot"></param>
        private static void QuickListSort<T>(List<T> source, int low, int hight, Func<T, T, bool> comparer, Config.QuickSortPivot pivot)
        {
            if (low < hight)
            {
                if(pivot == Config.QuickSortPivot.HEADER)
                {
                    int partition = PartitionHeader<T>(source, low, hight, comparer);
                    QuickListSort<T>(source, low, partition - 1, comparer, pivot);
                    QuickListSort<T>(source, partition + 1, hight, comparer, pivot);
                }
                else
                {
                    int partition = PartitionEnd<T>(source, low, hight, comparer);
                    QuickListSort<T>(source, low, partition - 1, comparer, pivot);
                    QuickListSort<T>(source, partition + 1, hight, comparer, pivot);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <param name="pivot"></param>
        /// <returns></returns>
        public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer, Config.QuickSortPivot pivot = Config.QuickSortPivot.HEADER)
        {
            List<T> list = source.ToList();
            QuickListSort<T>(list, 0, list.Count - 1, comparer, pivot);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="splip"></param>
        /// <param name="comparer"></param>
        public static void Merge<T>(List<T> source, int begin, int end, int splip, Func<T, T, bool> comparer)
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
            while(cLow < lowLength && cHight < hightLength)
            {
                if (comparer(low[cLow], hight[cHight])) source[cSource++] = low[cLow++];
                else source[cSource++] = hight[cHight++];
            }
            while (cLow < lowLength) source[cSource++] = low[cLow++];
            while (cHight < hightLength) source[cSource++] = hight[cHight++];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="comparer"></param>
        private static void MergeListSort<T>(List<T> source, int begin, int end, Func<T, T, bool> comparer)
        {
            if(begin < end - 1)
            {
                int splip = (begin + end) / 2;
                MergeListSort(source, begin, splip, comparer);
                MergeListSort(source, splip + 1, end, comparer);
                Merge(source, begin, end, splip, comparer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> list = source.ToList();
            MergeListSort<T>(list, 0, list.Count - 1, comparer);
            return list;
        }
    }
}
