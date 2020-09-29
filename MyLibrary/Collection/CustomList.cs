using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection
{
    public class CustomList<T> : CustomBaseCollection<T>, IEnumerable<T>
    {
        public CustomList()
        {
            _items = new T[10];
            _size = 0;
            _capacity = 10;
            _step = 10;
        }

        public CustomList(int capacity, int step)
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

        public CustomList(int capacity)
        {
            int _capacity = 10;
            if (capacity > 0) _capacity = capacity;
            this._capacity = _capacity;
            _items = new T[this._capacity];
            this._step = 10;
            _size = 0;
        }

        public CustomList(IEnumerable<T> collection)
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

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < _size)
                    return _items[index];
                else throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < _size)
                    _items[index] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        public void Add(T item)
        {
            if(_size >= _capacity)
            {
                _capacity += _step;
                Array.Resize<T>(ref _items, _capacity);
            }
            _items[_size++] = item;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            int count = collection.Count();
            int size = _size + count;
            if (size > _capacity)
            {
                _capacity = 10 * (size / 10) + 10;
                Array.Resize<T>(ref _items, _capacity);
            }
            collection.ToArray().CopyTo(_items, _size);
            _size = size;
        }

        public void Insert(T item, int index)
        {
            if (_size >= _capacity)
            {
                _capacity += _step;
                Array.Resize<T>(ref _items, _capacity);
            }
            T[] temp = new T[_size - index];
            _items.CopyTo(temp, index);
            _items[index] = item;
            _items.Concat(temp);
        }

        public void InsertRange(IEnumerable<T> collection, int index)
        {

        }

        public bool Contain(T item)
        {
            foreach (T x in _items)
                if (item.Equals(x)) return true;
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)new CustomListtEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class CustomListtEnumerator : IEnumerator<T>
        {
            private CustomList<T> list;
            private int index;

            public CustomListtEnumerator(CustomList<T> list)
            {
                this.list = list;
                index = -1;
            }

            public T Current
            {
                get { return list[index]; }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                index++;
                if (index >= list.Count) return false;
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        }
    }
}
