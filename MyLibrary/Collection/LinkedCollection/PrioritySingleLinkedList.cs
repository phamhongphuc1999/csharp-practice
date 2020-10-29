using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLibrary.Collection.LinkedCollection
{
    public class PrioritySingleLinkedList<T>: IEnumerable<SingleNodeData<T>>
    {
        protected Func<T, T, bool> priority;
        protected SingleLinkedList<T> _items;

        public PrioritySingleLinkedList(Func<T, T, bool> priority)
        {
            this.priority = priority;
            _items = new SingleLinkedList<T>();
        }

        public PrioritySingleLinkedList(IEnumerable<T> collection, Func<T, T, bool> priority)
        {
            this.priority = priority;
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public SingleNodeData<T> First
        {
            get { return _items.First; }
        }

        public SingleNodeData<T> Last
        {
            get { return _items.Last; }
        }

        public SingleNodeData<T> this[int index]
        {
            get
            {
                if (this.Count <= index || index < 0) throw new IndexOutOfRangeException();
                SingleNodeData<T> result = this.First;
                for (int i = 0; i < index; i += 1)
                    result = result.next;
                return result;
            }
        }

        private static int PrivateBinarySearch(PrioritySingleLinkedList<T> list, T value, Func<T, T, bool> comparer, int begin, int end)
        {
            if(begin == end)
            {
                T temp = list[begin].data;
                if (temp.Equals(value)) return begin;
                else return -1;
            }
            int middle = (begin + end) / 2;
            T mTemp = list[middle].data;
            if (mTemp.Equals(value)) return middle;
            else if (comparer(value, mTemp))
                return PrivateBinarySearch(list, value, comparer, middle + 1, end);
            else return PrivateBinarySearch(list, value, comparer, begin, middle);
        }

        public static int BinarySearch(PrioritySingleLinkedList<T> list, T value, Func<T, T, bool> comparer)
        {
            return PrioritySingleLinkedList<T>.PrivateBinarySearch(list, value, comparer, 0, list.Count);
        }

        public IEnumerator<SingleNodeData<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class PrioritySingleLinkedListIEnumerator : IEnumerator<SingleNodeData<T>>
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
