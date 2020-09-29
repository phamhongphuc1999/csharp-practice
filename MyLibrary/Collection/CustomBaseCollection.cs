using System;

namespace MyLibrary.Collection
{
    public abstract class CustomBaseCollection<T>
    {
        protected T[] _items;
        protected int _size;
        protected int _capacity;
        protected int _step;
    }
}
