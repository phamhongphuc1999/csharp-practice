using System;

namespace MyLibrary.Collection
{
    public abstract class CustomBaseCollection<T>
    {
        protected T[] _items;
        protected int _size;
        protected int _capacity;
        protected int _step;

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
    }
}
