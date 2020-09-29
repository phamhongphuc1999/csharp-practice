using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection
{
    public class CustomQueue<T>: CustomBaseCollection<T>
    {
        public CustomQueue()
        {
            _items = new T[10];
            _size = 0;
            _capacity = 10;
            _step = 10;
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
            _size = 0;
        }

        public CustomQueue(int capacity)
        {
            int _capacity = 10;
            if (capacity > 0) _capacity = capacity;
            this._capacity = _capacity;
            _items = new T[this._capacity];
            this._step = 10;
            _size = 0;
        }

        public CustomQueue(IEnumerable<T> collection)
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
                if (value >= _size) Array.Resize<T>(ref _items, value);
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
