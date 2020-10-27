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

        public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer, Config.QuickSortPivot pivot)
        {
            List<T> list = source.ToList();
            QuickListSort<T>(list, 0, list.Count - 1, comparer, pivot);
            return list;
        }

        private static void Merge<T>(List<T> source, int begin, int end, int splip, Func<T, T, bool> comparer)
        {

        }

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

        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            List<T> list = source.ToList();
            MergeListSort<T>(list, 0, list.Count - 1, comparer);
            return list;
        }
    }
}
