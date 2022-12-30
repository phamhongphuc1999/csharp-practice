// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using MyLibrary.CustomLinq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLibrary.Collection.LinkedCollection
{
    class PriorityDoubleLinkedList<T> : IEnumerable<DoubleNodeData<T>>
    {
        protected DoubleLinkedList<T> save;
        protected Func<T, T, bool> comparer;

        public PriorityDoubleLinkedList(Func<T, T, bool> comparer)
        {
            this.comparer = comparer;
            save = new DoubleLinkedList<T>();
        }

        public PriorityDoubleLinkedList(Func<T, T, bool> comparer, IEnumerable<T> collection)
        {
            this.comparer = comparer;
            save = new DoubleLinkedList<T>();
            foreach (T item in collection)
            {
                if (save.Count == 0) save.AddFirst(item);
                else
                {
                    DoubleNodeData<T> temp = new DoubleNodeData<T>(item);
                    int index = save.CustomBinarySearch(temp, 0, save.Count, (x, y) =>
                    {
                        if (comparer(x.data, y.data)) return -1;
                        else return 1;
                    });
                    save.AddAfter(index, temp);
                }
            }
        }

        public DoubleNodeData<T> this[int index]
        {
            get
            {
                if (this.Count <= index || index < 0) throw new IndexOutOfRangeException();
                DoubleNodeData<T> result = this.First;
                for (int i = 0; i < index; i += 1)
                    result = result.Next;
                return result;
            }
        }

        public DoubleNodeData<T> First
        {
            get { return save.First; }
        }

        public DoubleNodeData<T> Last
        {
            get { return save.Last; }
        }

        public int Count
        {
            get { return save.Count; }
        }

        public DoubleNodeData<T> Add(T item)
        {
            DoubleNodeData<T> temp = new DoubleNodeData<T>(item);
            int index = save.CustomBinarySearch(temp, 0, save.Count, (x, y) =>
            {
                if (comparer(x.data, y.data)) return -1;
                else return 1;
            });
            save.AddAfter(index, temp);
            return temp;
        }

        //public DoubleNodeData<T> Remove(int index)
        //{

        //}

        public IEnumerator<DoubleNodeData<T>> GetEnumerator()
        {
            return (IEnumerator<DoubleNodeData<T>>)new PriorityDoubleLinkedListIEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class PriorityDoubleLinkedListIEnumerator : IEnumerator<DoubleNodeData<T>>
        {
            private PriorityDoubleLinkedList<T> list;
            private DoubleNodeData<T> currentItem;
            private int index;

            public PriorityDoubleLinkedListIEnumerator(PriorityDoubleLinkedList<T> list)
            {
                this.list = list;
                currentItem = null;
                index = -1;
            }

            public DoubleNodeData<T> Current
            {
                get { return currentItem; }
            }

            object IEnumerator.Current => throw new System.NotImplementedException();

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                currentItem = currentItem.Next;
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