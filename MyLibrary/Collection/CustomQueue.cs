using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection
{
    public class CustomQueue<T>
    {
        public T[] _items;
        protected int _beginIndex;
        protected int _endIndex;
        protected int _capacity;
        protected int _step;
        
        public CustomQueue()
        {
            _items = new T[10];
            _beginIndex = _endIndex = 0;
            _capacity = 10;
            _step = 10;
            _beginIndex = 0;
        }

        public CustomQueue(int capacity, int step)
        {
            int _step = 10;
            int _capacity = 10;
            if (step > 0) _step = step;
            if (capacity > 0) _capacity = capacity;
            this._capacity = _capacity;
            this._step = _step;
            _items = new T[this._capacity];
            _beginIndex = _endIndex = 0;
            _beginIndex = 0;
        }

        public CustomQueue(int capacity)
        {
            int _capacity = 10;
            if (capacity > 0) _capacity = capacity;
            this._capacity = _capacity;
            _items = new T[this._capacity];
            this._step = 10;
            _beginIndex = _endIndex = 0;
            _beginIndex = 0;
        }

        public CustomQueue(IEnumerable<T> collection)
        {
            int count = collection.Count();
            _capacity = 10 * (count / 10) + 10;
            _step = 10;
            _items = new T[_capacity];
            collection.ToArray().CopyTo(_items, 0);
            _beginIndex = 0;
            _endIndex = count;
            _beginIndex = 0;
        }

        public int Count
        {
            get { return _endIndex - _beginIndex; }
        }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value >= _capacity)
                {
                    _capacity = value;
                    Array.Resize(ref _items, value);
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

        public void Enqueue(T item)
        {
            
        }

        //public T Dequeue()
        //{

        //}

        //public T Peek()
        //{

        //}
    }
}
