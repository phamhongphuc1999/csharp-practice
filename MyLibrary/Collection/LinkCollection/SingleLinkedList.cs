using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection.LinkCollection
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

        public IEnumerator<SingleNodeData<T>> GetEnumerator()
        {
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
