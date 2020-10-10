using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection
{
    public class NodeData<T>
    {
        public NodeData<T> next;
        public NodeData<T> prev;
        public T data;

        public NodeData(T data)
        {
            this.data = data;
            this.next = null;
            this.prev = null;
        }

        public static NodeData<T> operator ++(NodeData<T> node)
        {
            node = node.next;
            return node;
        }

        public static NodeData<T> operator +(NodeData<T> node, int value)
        {
            int count = 0;
            while (node != null && count < value)
                node = node.next;
            return node;
        }

        public static NodeData<T> operator --(NodeData<T> node)
        {
            node = node.prev;
            return node;
        }

        public static NodeData<T> operator -(NodeData<T> node, int value)
        {
            int count = 0;
            while (node != null && count < value)
                node = node.prev;
            return node;
        }

        public override string ToString()
        {
            return data.ToString();
        }

        public override bool Equals(object obj)
        {
            NodeData<T> node = obj as NodeData<T>;
            if (node == null) return false;
            return this.data.Equals(node.data);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CustomLinkedList<T> : IEnumerable<NodeData<T>>
    {
        protected int _size;
        protected NodeData<T> begin;
        protected NodeData<T> end;

        public CustomLinkedList()
        {
            begin = end = null;
            _size = 0;
        }

        public CustomLinkedList(IEnumerable<T> collection)
        {
            begin = new NodeData<T>(collection.ElementAt(0));
            NodeData<T> pNext = begin;
            NodeData<T> pPrev = null;
            int count = collection.Count();
            for (int i = 1; i < count; i++)
            {
                NodeData<T> next = new NodeData<T>(collection.ElementAt(i));
                pNext.next = next;
                pNext.prev = pPrev;
                pPrev = pNext;
                pNext = next;
            }
            end = pNext;
            _size = count;
        }

        public NodeData<T> this[int index]
        {
            get
            {
                if (_size <= index || index < 0) throw new IndexOutOfRangeException();
                NodeData<T> result = begin;
                for (int i = 0; i < index; i += 1)
                    result = result.next;
                return result;
            }
            set
            {
                if (_size <= index || index < 0) throw new IndexOutOfRangeException();
                NodeData<T> result = begin;
                for (int i = 0; i < index; i += 1)
                    result = result.next;
                result = value;
            }
        }

        public void Concat(CustomLinkedList<T> nodeList)
        {
            this.end.next = nodeList.begin;
            this.end = nodeList.end;
            _size += nodeList.Count;
        }

        public NodeData<T> First
        {
            get { return begin; }
        }

        public NodeData<T> Last
        {
            get { return end; }
        }

        public int Count
        {
            get { return _size; }
        }

        public NodeData<T> Find(T value)
        {
            NodeData<T> pTemp = begin;
            while (pTemp != null)
            {
                if (pTemp.data.Equals(value)) return pTemp;
                pTemp = pTemp.next;
            }
            return null;
        }

        public NodeData<T> FindLast(T value)
        {
            NodeData<T> pTemp = end;
            while (pTemp != null)
            {
                if (pTemp.prev.data.Equals(value)) return pTemp;
                pTemp = pTemp.prev;
            }
            return null;
        }

        public NodeData<T> AddFirst(T value)
        {
            NodeData<T> node = new NodeData<T>(value);
            node.next = begin;
            begin = node;
            if (_size == 0) end = node;
            _size += 1;
            return node;
        }

        public void AddFirst(NodeData<T> node)
        {
            node.next = begin;
            node.prev = null;
            begin = node;
            if (_size == 0) end = node;
            _size += 1;
        }

        public NodeData<T> AddLast(T value)
        {
            NodeData<T> node = new NodeData<T>(value);
            if (_size == 0) begin = end = node;
            else
            {
                end.next = node;
                end = node;
            }
            _size += 1;
            return node;
        }

        public void AddLast(NodeData<T> node)
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

        public NodeData<T> AddAfter(int index, T value)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            NodeData<T> node = new NodeData<T>(value);
            NodeData<T> indexNode = this[index];
            node.next = indexNode.next;
            node.prev = indexNode;
            indexNode.next = node;
            return node;
        }

        public void AddAfter(int index, NodeData<T> node)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            NodeData<T> indexNode = this[index];
            node.next = indexNode.next;
            node.prev = indexNode;
            indexNode.next = node;
        }

        public NodeData<T> AddBefore(int index, T value)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            NodeData<T> node = new NodeData<T>(value);
            NodeData<T> indexNode = this[index];
            node.next = indexNode;
            node.prev = indexNode.prev;
            indexNode.prev.next = node;
            return node;
        }

        public void AddBefore(int index, NodeData<T> node)
        {
            if (_size <= index || index < 0) throw new IndexOutOfRangeException();
            NodeData<T> indexNode = this[index];
            node.next = indexNode;
            node.prev = indexNode.prev;
            indexNode.prev.next = node;
        }

        public void Remove(int index)
        {
            if (index >= 0 && index < _size)
            {
                NodeData<T> removeNode = this[index];
                NodeData<T> prevNode = removeNode.prev;
                NodeData<T> nextNode = removeNode.next;
                if (prevNode != null) prevNode.next = nextNode;
                if (nextNode != null) nextNode.prev = prevNode;
                _size--;
            }
        }

        public void RemoveFirst(T value)
        {
            NodeData<T> temp = begin;
            while(temp != null)
            {
                if (temp.data.Equals(value))
                {
                    NodeData<T> prevNode = temp.prev;
                    NodeData<T> nextNode = temp.next;
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
            NodeData<T> temp = end;
            while(temp != null)
            {
                if (temp.data.Equals(value))
                {
                    NodeData<T> prevNode = temp.prev;
                    NodeData<T> nextNode = temp.next;
                    if (prevNode != null) prevNode.next = nextNode;
                    if (nextNode != null) nextNode.prev = prevNode;
                    --_size;
                    break;
                }
                temp--;
            }
        }

        public void ForEach(Action<NodeData<T>> action)
        {
            NodeData<T> node = begin;
            while (node != null)
            {
                action(node);
                node = node.next;
            }
        }

        public override string ToString()
        {
            string result = "";
            NodeData<T> pTemp = begin;
            while (pTemp != null) result += pTemp.ToString();
            return result;
        }

        public IEnumerator<NodeData<T>> GetEnumerator()
        {
            return (IEnumerator<NodeData<T>>)new CustomLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class CustomLinkedListEnumerator : IEnumerator<NodeData<T>>
        {
            private CustomLinkedList<T> list;
            private NodeData<T> begin;

            public CustomLinkedListEnumerator(CustomLinkedList<T> list)
            {
                this.list = list;
                begin = null;
            }

            public NodeData<T> Current
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
