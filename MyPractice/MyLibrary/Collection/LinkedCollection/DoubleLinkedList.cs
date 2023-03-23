using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection.LinkedCollection
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
                pNext.Next = next;
                pNext.Prev = pPrev;
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
                    result = result.Next;
                return result;
            }
            set
            {
                if (_size <= index || index < 0) throw new IndexOutOfRangeException();
                DoubleNodeData<T> result = begin;
                for (int i = 0; i < index; i += 1)
                    result = result.Next;
                result = value;
            }
        }

        public void Concat(DoubleLinkedList<T> nodeList)
        {
            this.end.Next = nodeList.begin;
            nodeList.begin.Prev = this.end;
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
                pTemp = pTemp.Next;
            }
            return null;
        }

        public DoubleNodeData<T> FindLast(T value)
        {
            DoubleNodeData<T> pTemp = end;
            while (pTemp != null)
            {
                if (pTemp.Prev.data.Equals(value)) return pTemp;
                pTemp = pTemp.Prev;
            }
            return null;
        }

        public DoubleNodeData<T> AddFirst(T value)
        {
            DoubleNodeData<T> node = new DoubleNodeData<T>(value);
            node.Next = begin;
            begin = node;
            if (_size == 0) end = node;
            _size += 1;
            return node;
        }

        public void AddFirst(DoubleNodeData<T> node)
        {
            node.Next = begin;
            node.Prev = null;
            begin = node;
            if (_size == 0) end = node;
            _size += 1;
        }

        public DoubleNodeData<T> AddLast(T value)
        {
            DoubleNodeData<T> node = new DoubleNodeData<T>(value);
            end.Next = node;
            end = node;
            if (_size == 0) begin = node;
            _size += 1;
            return node;
        }

        public void AddLast(DoubleNodeData<T> node)
        {
            end.Next = node;
            node.Next = null;
            end = node;
            if (_size == 0) begin = node;
            _size += 1;
        }

        public DoubleNodeData<T> AddAfter(int index, T value)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            DoubleNodeData<T> node = new DoubleNodeData<T>(value);
            DoubleNodeData<T> indexNode = this[index];
            node.Next = indexNode.Next;
            node.Prev = indexNode;
            indexNode.Next = node;
            return node;
        }

        public void AddAfter(int index, DoubleNodeData<T> node)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            DoubleNodeData<T> indexNode = this[index];
            node.Next = indexNode.Next;
            node.Prev = indexNode;
            indexNode.Next = node;
        }

        public DoubleNodeData<T> AddBefore(int index, T value)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            DoubleNodeData<T> node = new DoubleNodeData<T>(value);
            DoubleNodeData<T> indexNode = this[index];
            node.Next = indexNode;
            node.Prev = indexNode.Prev;
            indexNode.Prev.Next = node;
            return node;
        }

        public void AddBefore(int index, DoubleNodeData<T> node)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            DoubleNodeData<T> indexNode = this[index];
            node.Next = indexNode;
            node.Prev = indexNode.Prev;
            indexNode.Prev.Next = node;
        }

        public void Remove(int index)
        {
            if (index >= 0 && index < _size)
            {
                DoubleNodeData<T> removeNode = this[index];
                DoubleNodeData<T> prevNode = removeNode.Prev;
                DoubleNodeData<T> nextNode = removeNode.Next;
                if (prevNode != null) prevNode.Next = nextNode;
                if (nextNode != null) nextNode.Prev = prevNode;
                _size--;
            }
        }

        public void RemoveFirst(T value)
        {
            DoubleNodeData<T> temp = begin;
            while (temp != null)
            {
                if (temp.data.Equals(value))
                {
                    DoubleNodeData<T> prevNode = temp.Prev;
                    DoubleNodeData<T> nextNode = temp.Next;
                    if (prevNode != null) prevNode.Next = nextNode;
                    if (nextNode != null) nextNode.Prev = prevNode;
                    --_size;
                    break;
                }
                temp++;
            }
        }

        public void RemoveLast(T value)
        {
            DoubleNodeData<T> temp = end;
            while (temp != null)
            {
                if (temp.data.Equals(value))
                {
                    DoubleNodeData<T> prevNode = temp.Prev;
                    DoubleNodeData<T> nextNode = temp.Next;
                    if (prevNode != null) prevNode.Next = nextNode;
                    if (nextNode != null) nextNode.Prev = prevNode;
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
                node = node.Next;
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
            private DoubleNodeData<T> currentItem;
            private int index;

            public DoubleLinkedListEnumerator(DoubleLinkedList<T> list)
            {
                this.list = list;
                currentItem = null;
                index = -1;
            }

            public DoubleNodeData<T> Current
            {
                get { return currentItem; }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (index >= 0) currentItem = currentItem.Next;
                index++;
                if (currentItem == null) return false;
                return true;
            }

            public void Reset()
            {
                currentItem = null;
            }
        }
    }
}
