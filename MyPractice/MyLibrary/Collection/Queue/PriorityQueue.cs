// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Collections.Generic;

namespace MyLibrary.Collection.Queue
{
    public class PriorityQueue<T>
    {
        protected List<T> _items;
        protected Func<T, T, bool> comparer;

        public PriorityQueue(Func<T, T, bool> comparer)
        {
            _items = new List<T>();
            this.comparer = comparer;
        }

        public PriorityQueue(IEnumerable<T> collection, Func<T, T, bool> comparer)
        {
            _items = new List<T>(collection);
            this.comparer = comparer;
            Heap.BuildHeap(_items, _items.Count, this.comparer);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public int Capacity
        {
            get { return _items.Capacity; }
        }

        public void Enqueue(T item)
        {
            _items.Add(item);
            Heap.BuildHeap(_items, _items.Count, this.comparer);
        }

        public T Dequeue()
        {
            if(_items.Count > 0)
            {
                T result = _items[0];
                _items.RemoveAt(0);
                Heap.BuildHeap(_items, _items.Count, this.comparer);
                return result;
            }
            throw new IndexOutOfRangeException();
        }

        public T Peek()
        {
            if (_items.Count > 0) return _items[0];
            throw new IndexOutOfRangeException();
        }

        public bool TryDequeue(out T result)
        {
            if (_items.Count > 0)
            {
                result = _items[0];
                _items.RemoveAt(0);
                Heap.BuildHeap(_items, _items.Count, this.comparer);
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }

        public bool TryPeek(out T result)
        {
            if (_items.Count > 0)
            {
                result = _items[0];
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }
    }
}
