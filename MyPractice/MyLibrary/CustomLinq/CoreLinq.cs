using MyLibrary.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using SelectType = MyLibrary.Sort.Config.PivotType;

namespace MyLibrary.CustomLinq
{
    public static partial class CoreLinq
    {
        public static bool SWAP<T>(this List<T> source, int x, int y)
        {
            int count = source.Count;
            if (x < 0 || y < 0 || x >= count || y >= count) return false;
            if (x == y) return true;
            T temp = source[x];
            source[x] = source[y];
            source[y] = temp;
            return true;
        }

        public static IEnumerable<TResult> CustomSelect<T, TResult>(this IEnumerable<T> source, Func<T, TResult> func)
        {
            foreach (T item in source)
                yield return func(item);
        }

        public static IEnumerable<TResult> CustomSelect<T, TResult>(this IEnumerable<T> source, Func<T, T, TResult> func)
        {
            int count = source.CustomCount();
            for (int i = 0; i < count - 1; i++)
                yield return func(source.CustomElementAt(i), source.CustomElementAt(i + 1));
        }

        public static IEnumerable<T> CustomWhere<T>(this IEnumerable<T> source, Func<T, bool> func)
        {
            foreach (T item in source)
                if (func(item)) yield return item;
        }

        public static IEnumerable<TResult> CustomJoin<T, TInner, TKey, TResult>(this IEnumerable<T> source, IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector)
        {
            int count = source.CustomCount();
            if (count != inner.CustomCount()) throw new Exception();
            for (int i = 0; i < count; i++)
            {
                T outerValue = source.CustomElementAt(i);
                TInner innerValue = inner.CustomElementAt(i);
                TKey outerKey = outerKeySelector(outerValue);
                TKey innerKey = innerKeySelector(innerValue);
                if (outerKey.Equals(innerKey)) yield return resultSelector(outerValue, innerValue);
            }
        }

        public static IEnumerable<TResult> CustomJoin<T, TInner, TKey, TResult>(this IEnumerable<T> source, IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            int count = source.CustomCount();
            if (count != inner.CustomCount()) throw new Exception();
            for (int i = 0; i < count; i++)
            {
                T outerValue = source.CustomElementAt(i);
                TInner innerValue = inner.CustomElementAt(i);
                TKey outerKey = outerKeySelector(outerValue);
                TKey innerKey = innerKeySelector(innerValue);
                if (comparer.Equals(outerKey, innerKey)) yield return resultSelector(outerValue, innerValue);
            }
        }

        public static IEnumerable<T> CustomConcat<T>(this IEnumerable<T> source, IEnumerable<T> second)
        {
            foreach (T item in source)
                yield return item;
            foreach (T item in second)
                yield return item;
        }

        public static IEnumerable<T> CustomConcatWithoutSame<T>(this IEnumerable<T> source, IEnumerable<T> second)
        {
            Dictionary<T, int> temp = new Dictionary<T, int>();
            foreach (T item in source)
                temp[item] = 1;
            foreach (T item in second)
                temp[item] = 1;
            foreach (KeyValuePair<T, int> item in temp)
                yield return item.Key;
        }

        public static IEnumerable<T> CustomRemoveSame<T>(this IEnumerable<T> source)
        {
            Dictionary<T, int> temp = new Dictionary<T, int>();
            foreach (T item in source)
                temp[item] = 1;
            foreach (KeyValuePair<T, int> item in temp)
                yield return item.Key;
        }

        public static int CustomCount<T>(this IEnumerable<T> source)
        {
            int count = 0;
            foreach (T item in source) count++;
            return count;
        }

        public static int CustomCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int count = 0;
            foreach (T item in source)
                if (predicate(item)) count++;
            return count;
        }

        public static T CustomElementAt<T>(this IEnumerable<T> source, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException();
            int count = -1;
            foreach (T item in source)
            {
                count++;
                if (count == index) return item;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static int CustomBinarySearch<T>(this IEnumerable<T> source, T value, int begin, int end, Func<T, T, int> comparer)
        {
            if (begin == end)
            {
                if (comparer(value, source.CustomElementAt(begin)) == 0) return begin;
                else return -1;
            }
            int middle = (begin + end) / 2;
            int result = comparer(value, source.CustomElementAt(middle));
            if (result == 0) return begin;
            else if (result < 0) return CustomBinarySearch(source, value, begin, middle, comparer);
            else return CustomBinarySearch(source, value, middle + 1, end, comparer);
        }

        public static int CustomSearchPosition<T>(this IEnumerable<T> source, T value, int begin, int end, Func<T, T, bool> comparer)
        {
            if (begin == end)
            {
                bool result = comparer(source.CustomElementAt(begin), value);
                if (result) return begin + 1;
                else return begin - 1;
            }
            int middle = (begin + end) / 2;
            bool temp = comparer(value, source.CustomElementAt(middle));
            if (temp) return CustomSearchPosition(source, value, middle + 1, end, comparer);
            else return CustomSearchPosition(source, value, begin, middle, comparer);
        }

        private static T QuickSelectList<T>(List<T> source, int begin, int end, int index, SelectType type, Func<T, T, bool> comparer)
        {
            if (end - begin + 1 >= index)
            {
                int partition = 0;
                if (type == SelectType.HEADER) partition = CommonSort.Partition(source, begin, end, begin, comparer);
                else if (type == SelectType.END) partition = CommonSort.Partition(source, begin, end, end, comparer);
                else partition = CommonSort.Partition(source, begin, end, (begin + end) / 2, comparer);
                if (index + begin < partition) QuickSelectList(source, begin, partition, index, type, comparer);
                else if (index + begin > partition) QuickSelectList(source, partition + 1, end, index - partition + begin - 1, type, comparer);
                else return source[partition];
            }
            throw new IndexOutOfRangeException();
        }

        private static T QuickSelectList<T>(List<T> source, int begin, int end, int index, Func<T, T, bool> comparer)
        {
            if (end - begin + 1 >= index)
            {
                int partition = CommonSort.RandomPartition(source, begin, end, comparer);
                if (index + begin < partition) QuickSelectList(source, begin, partition, index, comparer);
                else if (index + begin > partition) QuickSelectList(source, partition + 1, end, index - partition + begin - 1, comparer);
                else return source[partition];
            }
            throw new IndexOutOfRangeException();
        }

        public static T QuickSelect<T>(this IEnumerable<T> source, int begin, int end, int index, SelectType type, Func<T, T, bool> comparer)
        {
            List<T> list = source.ToList();
            return QuickSelectList(list, begin, end, index, type, comparer);
        }

        public static T RandomQuickSelect<T>(this IEnumerable<T> source, int begin, int end, int index, Func<T, T, bool> comparer)
        {
            List<T> list = source.ToList();
            return QuickSelectList(list, begin, end, index, comparer);
        }
    }
}
