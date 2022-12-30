// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection.Queue
{
    internal class Support
    {
        public static int AddOneUnit(int element, int capacity)
        {
            if (element < capacity - 1) return (element + 1);
            return 0;
        }

        public static int SubtractOneUnit(int element, int capacity)
        {
            if (element > 0) return (element - 1);
            return (capacity - 1);
        }
    }

    public class CustomQueue<T>
    {
        protected T[] _items;
        protected int begin;
        protected int end;
        protected int _size;
        protected int _capacity;
        protected int _step;

        public CustomQueue()
        {
            _capacity = _step = 10;
            begin = end = 0;
            _size = 0;
            _items = new T[_capacity];
        }

        public CustomQueue(int capacity, int step)
        {
            _capacity = _step = 10;
            if (capacity > 0) _capacity = capacity;
            if (step > 0) _step = step;
            begin = end = 0;
            _size = 0;
            _items = new T[_capacity];
        }

        public CustomQueue(int capacity)
        {
            _capacity = _step = 10;
            if (capacity > 0) _capacity = capacity;
            begin = end = 0;
            _size = 0;
            _items = new T[_capacity];
        }

        public CustomQueue(IEnumerable<T> collection)
        {
            _size = collection.Count();
            _capacity = 10 * (_size / 10) + 10;
            _step = 10;
            _items = new T[_capacity];
            collection.ToArray().CopyTo(_items, 0);
        }

        public int Count
        {
            get { return _size; }
        }

        public int Capacity
        {
            get { return _capacity; }
        }

        public int Step
        {
            get { return _step; }
            set
            {
                if (value > 0) _step = value;
            }
        }

        public void Enqueue(T item)
        {
            _size += 1;
            if (_size < _capacity)
            {
                end = Support.AddOneUnit(end, _capacity);
                _items[end] = item;
            }
            else
            {
                int capacity = _capacity;
                T[] temp = new T[capacity];
                for (int i = begin, index = 0; index < _capacity; i = Support.AddOneUnit(i, _capacity), index++)
                    temp[index] = _items[i];
                _capacity += _step;
                Array.Resize<T>(ref _items, _capacity);
                for (int i = 0; i < capacity; i++)
                    _items[i] = temp[i];
                _items[_size - 1] = item;
                begin = 0;
                end = _size - 1;
            }
        }

        public T Dequeue()
        {
            if (_size > 0)
            {
                end = Support.SubtractOneUnit(end, _capacity);
                _size--;
                return _items[end];
            }
            throw new IndexOutOfRangeException();
        }

        public T Peek()
        {
            if (_size > 0) return _items[end];
            else throw new IndexOutOfRangeException();
        }

        public bool TryDequeue(out T result)
        {
            if (_size > 0)
            {
                end = Support.SubtractOneUnit(end, _capacity);
                _size--;
                result = _items[end];
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
            if (_size > 0)
            {
                result = _items[end];
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
