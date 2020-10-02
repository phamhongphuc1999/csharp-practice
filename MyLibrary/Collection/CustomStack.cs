using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection
{
    public class CustomStack<T>
    {
        protected T[] _items;
        protected int _size;
        protected int _capacity;
        protected int _step;

        public CustomStack()
        {
            _items = new T[10];
            _size = 0;
            _capacity = 10;
            _step = 10;
        }

        public CustomStack(int capacity, int step)
        {
            int _step = 10;
            int _capacity = 10;
            if (step > 0) _step = step;
            if (capacity > 0) _capacity = capacity;
            this._capacity = _capacity;
            this._step = _step;
            _items = new T[this._capacity];
            _size = 0;
        }

        public CustomStack(int capacity)
        {
            int _capacity = 10;
            if (capacity > 0) _capacity = capacity;
            this._capacity = _capacity;
            _items = new T[this._capacity];
            this._step = 10;
            _size = 0;
        }

        public CustomStack(IEnumerable<T> collection)
        {
            int count = collection.Count();
            _capacity = 10 * (count / 10) + 10;
            _step = 10;
            _items = new T[_capacity];
            collection.ToArray().CopyTo(_items, 0);
            _size = count;
        }

        public int Count
        {
            get { return _size; }
        }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value >= _capacity)
                {
                    _capacity = value;
                    Array.Resize<T>(ref _items, _capacity);
                }
            }
        }

        public int Step
        {
            get { return _step; }
            set
            {
                if (value > 0) _step = value;
            }
        }

        public void Push(T item)
        {
            if (_size < _capacity) _items[_size++] = item;
            else
            {
                _capacity += _step;
                Array.Resize<T>(ref _items, _capacity);
                _items[_size++] = item;
            }
        }

        public T Pop()
        {
            if (_size > 0) return _items[--_size];
            else throw new IndexOutOfRangeException();
        }

        public T Peek()
        {
            if (_size > 0) return _items[_size - 1];
            else throw new IndexOutOfRangeException();
        }

        public void Clean()
        {
            Array.Resize(ref _items, 10);
            _size = 0;
            _capacity = 10;
        }

        public int[] ToArray()
        {
            int[] result = new int[_size];
            result.CopyTo(_items, 0);
            return result;
        }

        public override string ToString()
        {
            return this.Peek().ToString();
        }

        public bool TryPop(out T result)
        {
            if (_size > 0)
            {
                result = _items[--_size];
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
                result = _items[_size - 1];
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }

        public bool Contain(T item)
        {
            foreach (T x in _items)
                if (x.Equals(item)) return true;
            return false;
        }
    }
}
