using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection.LinkedCollection
{
    public class SingleLinkedList<T>: IEnumerable<SingleNodeData<T>>
    {
        protected int _size;
        protected SingleNodeData<T> begin;
        protected SingleNodeData<T> end;

        public SingleLinkedList()
        {
            begin = end = null;
            _size = 0;
        }

        public SingleLinkedList(IEnumerable<T> collection)
        {
            begin = new SingleNodeData<T>(collection.ElementAt(0));
            SingleNodeData<T> pNext = begin;
            int count = collection.Count();
            for (int i = 1; i < count; i++)
            {
                SingleNodeData<T> next = new SingleNodeData<T>(collection.ElementAt(i));
                pNext.next = next;
                pNext = next;
            }
            end = pNext;
            _size = count;
        }

        public SingleNodeData<T> this[int index]
        {
            get
            {
                if (_size <= index || index < 0) throw new IndexOutOfRangeException();
                SingleNodeData<T> result = begin;
                for (int i = 0; i < index; i += 1)
                    result = result.next;
                return result;
            }
            set
            {
                if (_size <= index || index < 0) throw new IndexOutOfRangeException();
                SingleNodeData<T> result = begin;
                for (int i = 0; i < index; i += 1)
                    result = result.next;
                result = value;
            }
        }

        public void Concat(SingleLinkedList<T> nodeList)
        {
            this.end.next = nodeList.begin;
            this.end = nodeList.end;
            _size += nodeList.Count;
        }

        public SingleNodeData<T> First
        {
            get { return begin; }
        }

        public SingleNodeData<T> Last
        {
            get { return end; }
        }

        public int Count
        {
            get { return _size; }
        }

        public SingleNodeData<T> Find(T value)
        {
            SingleNodeData<T> pTemp = begin;
            while(pTemp != null)
            {
                if (pTemp.data.Equals(value)) return pTemp;
                pTemp += 1;
            }
            return null;
        }

        public SingleNodeData<T> FindLast(T value)
        {
            SingleNodeData<T> pTemp = begin;
            SingleNodeData<T> result = null;
            while(pTemp != null)
            {
                if (pTemp.data.Equals(value)) result = pTemp;
                pTemp += 1;
            }
            return result;
        }

        public SingleNodeData<T> AddFirst(T value)
        {
            SingleNodeData<T> node = new SingleNodeData<T>(value);
            node.next = begin;
            begin = node;
            if (_size == 0) end = node;
            _size++;
            return node;
        }

        public void AddFirst(SingleNodeData<T> node)
        {
            node.next = begin;
            begin = node;
            if (_size == 0) end = node;
            _size++;
        }

        public SingleNodeData<T> AddLast(T value)
        {
            SingleNodeData<T> node = new SingleNodeData<T>(value);
            end.next = node;
            end = node;
            if (_size == 0) begin = node;
            _size++;
            return node;
        }

        public void AddLast(SingleNodeData<T> node)
        {
            end.next = node;
            end = node;
            node.next = null;
            if (_size == 0) begin = node;
            _size++;
        }

        //public SingleNodeData<T> AddAfter(int index, T value)
        //{
        //    if (index >= _size || index < 0) throw new ArgumentOutOfRangeException();
        //}

        public void AddAfter(int index, SingleNodeData<T> node)
        {
            if (index >= _size || index < 0) throw new ArgumentOutOfRangeException();

        }

        //public SingleNodeData<T> AddBefore(int index, T value)
        //{
        //    if (index >= _size || index < 0) throw new ArgumentOutOfRangeException();

        //}

        public void AddBefore(int index, SingleNodeData<T> node)
        {
            if (index >= _size || index < 0) throw new ArgumentOutOfRangeException();

        }

        public IEnumerator<SingleNodeData<T>> GetEnumerator()
        {
            //return (IEnumerator<SingleNodeData<T>>)new SingleLinkedListEnumerator(this);
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class SingleLinkedListEnumerator : IEnumerator<SingleNodeData<T>>
        {
            public SingleNodeData<T> Current => throw new NotImplementedException();

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
