using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomCollection
{
    class CustomList<T>
    {
        private T[] items;
        private int size;
        private int capacity;
        private int step;

        public CustomList()
        {
            items = new T[10];
            size = 0;
            capacity = 10;
            step = 10;
        }

        public CustomList(int capacity, int step)
        {
            int _step = 10;
            int _capacity = 10;
            if (step > 0) _step = step;
            if (capacity > 0) _capacity = capacity;
            items = new T[_capacity];
            this.capacity = _capacity;
            this.step = _step;
            size = 0;
        }

        public CustomList(int capacity)
        {
            int _capacity = 10;
            if (capacity > 0) _capacity = capacity;
            items = new T[_capacity];
            this.capacity = capacity;
            this.step = 10;
            size = 0;
        }

        public CustomList(IEnumerable<T> collection)
        {
            int count = collection.Count();
            items.CopyTo(collection.ToArray(), count);
            size = 0;
            capacity = count;
        }

        public int Count
        {
            get { return size; }
        }

        public int Capacity
        {
            get { return capacity; } 
            set
            {
                if (value >= size) Array.Resize<T>(ref items, value);
            }
        }

        public int Step
        {
            get { return step; }
            set
            {
                if (value > 0) step = value;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < size)
                    return items[index];
                else throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < size)
                    items[index] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        public void Add(T item)
        {
            if (size < capacity) items[size++] = item;
            else
            {
                Array.Resize<T>(ref items, this.Count + this.Step);
                items[size++] = item;
            }
        }

        public bool Contain(T item)
        {
            foreach (T x in items)
                if (item.Equals(x)) return true;
            return false;
        }
    }
}
