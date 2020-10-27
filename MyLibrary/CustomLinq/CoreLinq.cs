using System;
using System.Collections.Generic;

namespace MyLibrary.CustomLinq
{
    public static partial class CoreLinq
    {
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

        public static T CustomElementAt<T>(this IEnumerable<T> source, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException();
            int count = -1;
            foreach(T item in source)
            {
                count++;
                if (count == index) return item;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
