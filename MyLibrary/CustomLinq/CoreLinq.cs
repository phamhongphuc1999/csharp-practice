// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<TResult> CustomJoin<T, TInner, TKey, TResult>(this IEnumerable<T> source, IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector)
        {
            int count = source.CustomCount();
            if (count != inner.CustomCount()) throw new Exception();
            for(int i = 0; i < count; i++)
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

        //public static IOrderedEnumerable<T> CustomOrder<T, TKey>(this IEnumerable<T> source, Func<T, TKey> KeySelecter)
        //{

        //}

        //public static IOrderedEnumerable<T> CustomOrder<T, TKey>(this IEnumerable<T> source, Func<T, TKey> KeySelecter, Func<TKey, TKey, bool> comparer)
        //{

        //}

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
    }
}
