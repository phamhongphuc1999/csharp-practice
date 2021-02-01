// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Sort
{
    public static partial class Heap
    {
        private static int LEFT(int i)
        {
            return 2 * i + 1;
        }

        private static int RIGHT(int i)
        {
            return 2 * i + 2;
        }

        public static void Heapfy<T>(List<T> source, int index, int length, Func<T, T, bool> comparer)
        {
            int left = LEFT(index);
            int right = RIGHT(index);
            int largest = index;
            if (left < length && !comparer(source[index], source[left])) largest = left;
            if (right < length && !comparer(source[largest], source[right])) largest = right;
            if (largest != index)
            {
                T temp = source[index];
                source[index] = source[largest];
                source[largest] = temp;
                Heapfy(source, largest, length, comparer);
            }
        }

        public static void BuildHeap<T>(List<T> source, int length, Func<T, T, bool> comparer)
        {
            int value = source.Count / 2;
            for (int i = value; i >= 0; i--)
                Heapfy(source, i, length, comparer);
        }

        public static IEnumerable<T> HeapSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> arr = source.ToList();
            int count = arr.Count;
            BuildHeap(arr, count, comparer);
            for(int i = count - 1; i >= 1; i--)
            {
                T temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                Heapfy(arr, 0, i, comparer);
            }
            return arr;
        }
    }
}
