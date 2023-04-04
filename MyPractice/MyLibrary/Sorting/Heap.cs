using MyLibrary.CustomLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Sorting
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
                source.SWAP(index, largest);
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
            for (int i = count - 1; i >= 1; i--)
            {
                arr.SWAP(0, i);
                Heapfy(arr, 0, i, comparer);
            }
            return arr;
        }
    }
}
