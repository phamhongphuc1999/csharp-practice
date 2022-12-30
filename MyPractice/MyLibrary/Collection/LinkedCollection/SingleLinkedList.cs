// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection.LinkedCollection
{
    public class SingleLinkedList<T> : IEnumerable<SingleNodeData<T>>
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
                pNext.Next = next;
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
                    result = result.Next;
                return result;
            }
        }

        public void Concat(SingleLinkedList<T> nodeList)
        {
            this.end.Next = nodeList.begin;
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
            while (pTemp != null)
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
            while (pTemp != null)
            {
                if (pTemp.data.Equals(value)) result = pTemp;
                pTemp += 1;
            }
            return result;
        }

        public SingleNodeData<T> AddFirst(T value)
        {
            SingleNodeData<T> node = new SingleNodeData<T>(value);
            node.Next = begin;
            begin = node;
            if (_size == 0) end = node;
            _size++;
            return node;
        }

        public void AddFirst(SingleNodeData<T> node)
        {
            node.Next = begin;
            begin = node;
            if (_size == 0) end = node;
            _size++;
        }

        public SingleNodeData<T> AddLast(T value)
        {
            SingleNodeData<T> node = new SingleNodeData<T>(value);
            end.Next = node;
            end = node;
            if (_size == 0) begin = node;
            _size++;
            return node;
        }

        public void AddLast(SingleNodeData<T> node)
        {
            end.Next = node;
            end = node;
            node.Next = null;
            if (_size == 0) begin = node;
            _size++;
        }

        public SingleNodeData<T> AddAfter(int index, T value)
        {
            if (index >= _size || index < 0) throw new ArgumentOutOfRangeException();
            SingleNodeData<T> pTemp = begin;
            for (int i = 1; i < index; i++)
                pTemp++;
            SingleNodeData<T> node = new SingleNodeData<T>(value);
            SingleNodeData<T> next = pTemp.Next;
            pTemp.Next = node;
            node.Next = next;
            return node;
        }

        public void AddAfter(int index, SingleNodeData<T> node)
        {
            if (index >= _size || index < 0) throw new ArgumentOutOfRangeException();
            SingleNodeData<T> pTemp = begin;
            for (int i = 1; i < index; i++)
                pTemp++;
            SingleNodeData<T> next = pTemp.Next;
            pTemp.Next = node;
            node.Next = next;
        }

        public SingleNodeData<T> AddBefore(int index, T value)
        {
            if (index >= _size || index < 0) throw new ArgumentOutOfRangeException();
            SingleNodeData<T> pTemp = begin;
            for (int i = 1; i <= index; i++)
                pTemp++;
            SingleNodeData<T> node = new SingleNodeData<T>(value);
            SingleNodeData<T> next = pTemp.Next;
            pTemp.Next = node;
            node.Next = next;
            return node;
        }

        public void AddBefore(int index, SingleNodeData<T> node)
        {
            if (index >= _size || index < 0) throw new ArgumentOutOfRangeException();
            SingleNodeData<T> pTemp = begin;
            for (int i = 1; i <= index; i++)
                pTemp++;
            SingleNodeData<T> next = pTemp.Next;
            pTemp.Next = node;
            node.Next = next;
        }

        public IEnumerator<SingleNodeData<T>> GetEnumerator()
        {
            return (IEnumerator<SingleNodeData<T>>)new SingleLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class SingleLinkedListEnumerator : IEnumerator<SingleNodeData<T>>
        {
            private SingleLinkedList<T> list;
            private int index;
            private SingleNodeData<T> currentItem;

            public SingleLinkedListEnumerator(SingleLinkedList<T> list)
            {
                this.list = list;
                index = -1;
                currentItem = list.begin;
            }

            public SingleNodeData<T> Current
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
