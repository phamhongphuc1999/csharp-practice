using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection.LinkCollection
{
    public class DoubleLinkedList<T> : IEnumerable<DoubleNodeData<T>>
    {
        protected int _size;
        protected DoubleNodeData<T> begin;
        protected DoubleNodeData<T> end;

        public DoubleLinkedList()
        {
            begin = end = null;
            _size = 0;
        }

        public DoubleLinkedList(IEnumerable<T> collection)
        {
            begin = new DoubleNodeData<T>(collection.ElementAt(0));
            DoubleNodeData<T> pNext = begin;
            DoubleNodeData<T> pPrev = null;
            int count = collection.Count();
            for (int i = 1; i < count; i++)
            {
                DoubleNodeData<T> next = new DoubleNodeData<T>(collection.ElementAt(i));
                pNext.next = next;
                pNext.prev = pPrev;
                pPrev = pNext;
                pNext = next;
            }
            end = pNext;
            _size = count;
        }

        public DoubleNodeData<T> this[int index]
        {
            get
            {
                if (_size <= index || index < 0) throw new IndexOutOfRangeException();
                DoubleNodeData<T> result = begin;
                for (int i = 0; i < index; i += 1)
                    result = result.next;
                return result;
            }
            set
            {
                if (_size <= index || index < 0) throw new IndexOutOfRangeException();
                DoubleNodeData<T> result = begin;
                for (int i = 0; i < index; i += 1)
                    result = result.next;
                result = value;
            }
        }

        public void Concat(DoubleLinkedList<T> nodeList)
        {
            this.end.next = nodeList.begin;
            this.end = nodeList.end;
            _size += nodeList.Count;
        }

        public DoubleNodeData<T> First
        {
            get { return begin; }
        }

        public DoubleNodeData<T> Last
        {
            get { return end; }
        }

        public int Count
        {
            get { return _size; }
        }

        public DoubleNodeData<T> Find(T value)
        {
            DoubleNodeData<T> pTemp = begin;
            while (pTemp != null)
            {
                if (pTemp.data.Equals(value)) return pTemp;
                pTemp = pTemp.next;
            }
            return null;
        }

        public DoubleNodeData<T> FindLast(T value)
        {
            DoubleNodeData<T> pTemp = end;
            while (pTemp != null)
            {
                if (pTemp.prev.data.Equals(value)) return pTemp;
                pTemp = pTemp.prev;
            }
            return null;
        }

        public DoubleNodeData<T> AddFirst(T value)
        {
            DoubleNodeData<T> node = new DoubleNodeData<T>(value);
            node.next = begin;
            begin = node;
            if (_size == 0) end = node;
            _size += 1;
            return node;
        }

        public void AddFirst(DoubleNodeData<T> node)
        {
            node.next = begin;
            node.prev = null;
            begin = node;
            if (_size == 0) end = node;
            _size += 1;
        }

        public DoubleNodeData<T> AddLast(T value)
        {
            DoubleNodeData<T> node = new DoubleNodeData<T>(value);
            if (_size == 0) begin = end = node;
            else
            {
                end.next = node;
                end = node;
            }
            _size += 1;
            return node;
        }

        public void AddLast(DoubleNodeData<T> node)
        {
            if (_size == 0) begin = end = node;
            else
            {
                end.next = node;
                node.next = null;
                end = node;
            }
            _size += 1;
        }

        public DoubleNodeData<T> AddAfter(int index, T value)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            DoubleNodeData<T> node = new DoubleNodeData<T>(value);
            DoubleNodeData<T> indexNode = this[index];
            node.next = indexNode.next;
            node.prev = indexNode;
            indexNode.next = node;
            return node;
        }

        public void AddAfter(int index, DoubleNodeData<T> node)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            DoubleNodeData<T> indexNode = this[index];
            node.next = indexNode.next;
            node.prev = indexNode;
            indexNode.next = node;
        }

        public DoubleNodeData<T> AddBefore(int index, T value)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            DoubleNodeData<T> node = new DoubleNodeData<T>(value);
            DoubleNodeData<T> indexNode = this[index];
            node.next = indexNode;
            node.prev = indexNode.prev;
            indexNode.prev.next = node;
            return node;
        }

        public void AddBefore(int index, DoubleNodeData<T> node)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            DoubleNodeData<T> indexNode = this[index];
            node.next = indexNode;
            node.prev = indexNode.prev;
            indexNode.prev.next = node;
        }

        public void Remove(int index)
        {
            if (index >= 0 && index < _size)
            {
                DoubleNodeData<T> removeNode = this[index];
                DoubleNodeData<T> prevNode = removeNode.prev;
                DoubleNodeData<T> nextNode = removeNode.next;
                if (prevNode != null) prevNode.next = nextNode;
                if (nextNode != null) nextNode.prev = prevNode;
                _size--;
            }
        }

        public void RemoveFirst(T value)
        {
            DoubleNodeData<T> temp = begin;
            while(temp != null)
            {
                if (temp.data.Equals(value))
                {
                    DoubleNodeData<T> prevNode = temp.prev;
                    DoubleNodeData<T> nextNode = temp.next;
                    if (prevNode != null) prevNode.next = nextNode;
                    if (nextNode != null) nextNode.prev = prevNode;
                    --_size;
                    break;
                }
                temp++;
            }
        }

        public void RemoveLast(T value)
        {
            DoubleNodeData<T> temp = end;
            while(temp != null)
            {
                if (temp.data.Equals(value))
                {
                    DoubleNodeData<T> prevNode = temp.prev;
                    DoubleNodeData<T> nextNode = temp.next;
                    if (prevNode != null) prevNode.next = nextNode;
                    if (nextNode != null) nextNode.prev = prevNode;
                    --_size;
                    break;
                }
                temp--;
            }
        }

        public void ForEach(Action<DoubleNodeData<T>> action)
        {
            DoubleNodeData<T> node = begin;
            while (node != null)
            {
                action(node);
                node = node.next;
            }
        }

        public override string ToString()
        {
            string result = "";
            DoubleNodeData<T> pTemp = begin;
            while (pTemp != null) result += pTemp.ToString();
            return result;
        }

        public IEnumerator<DoubleNodeData<T>> GetEnumerator()
        {
            return (IEnumerator<DoubleNodeData<T>>)new DoubleLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class DoubleLinkedListEnumerator : IEnumerator<DoubleNodeData<T>>
        {
            private DoubleLinkedList<T> list;
            private DoubleNodeData<T> begin;

            public DoubleLinkedListEnumerator(DoubleLinkedList<T> list)
            {
                this.list = list;
                begin = null;
            }

            public DoubleNodeData<T> Current
            {
                get { return begin; }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                begin = begin.next;
                if (begin == null) return false;
                return true;
            }

            public void Reset()
            {
                begin = null;
            }
        }
    }
}
