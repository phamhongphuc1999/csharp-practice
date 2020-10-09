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
    }

    public class CustomLinkedList<T>
    {
        protected int _size;
        protected NodeData<T> begin;
        protected NodeData<T> end;

        public CustomLinkedList()
        {
            begin = end = null;
        }

        public CustomLinkedList(IEnumerable<T> collection)
        {
            begin = new NodeData<T>(collection.ElementAt(0));
            NodeData<T> pNext = begin;
            NodeData<T> pPrev = null;
            int count = collection.Count();
            for(int i = 1; i < count; i++)
            {
                NodeData<T> next = new NodeData<T>(collection.ElementAt(i));
                pNext.next = next;
                pNext.prev = pPrev;
                pPrev = pNext;
                pNext = next;
            }
            end = pNext;
        }

        public NodeData<T>? First
        {
            get { return begin; }
        }

        public NodeData<T> Find(T value)
        {
            NodeData<T> pTemp = begin;
            while(pTemp != null)
            {
                if (pTemp.data.Equals(value)) return pTemp;
                pTemp = pTemp.next;
            }
            return null;
        }

        public NodeData<T> FindLast(T value)
        {
            NodeData<T> pTemp = end;
            while(pTemp != null)
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
            return node;
        }
        
        public void AddFirst(NodeData<T> node)
        {
            node.next = begin;
            node.prev = null;
            begin = node;
        }

        public NodeData<T> AddLast(T value)
        {
            NodeData<T> node = new NodeData<T>(value);
            end.next = node;
            end = node;
            return node;
        }

        public void AddLast(NodeData<T> node)
        {
            end.next = node;
            node.next = null;
            end = node;
        }

        //public NodeData<T> AddAfter()
        //{

        //}
    }
}
