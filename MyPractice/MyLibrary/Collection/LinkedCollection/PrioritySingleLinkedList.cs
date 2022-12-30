// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using MyLibrary.CustomLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection.LinkedCollection
{
    public class PrioritySingleLinkedList<T> : IEnumerable<SingleNodeData<T>>
    {
        protected Func<T, T, bool> comparer;
        protected SingleLinkedList<T> save;

        public PrioritySingleLinkedList(Func<T, T, bool> comparer)
        {
            this.comparer = comparer;
            save = new SingleLinkedList<T>();
        }

        public PrioritySingleLinkedList(Func<T, T, bool> comparer, IOrderedEnumerable<T> collection)
        {
            this.comparer = comparer;
            save = new SingleLinkedList<T>();
            foreach (T item in collection)
            {
                SingleNodeData<T> temp = new SingleNodeData<T>(item);
                if (save.Count == 0) save.AddFirst(temp);
                else
                {
                    int index = save.CustomBinarySearch(temp, 0, save.Count, (x, y) =>
                    {
                        if (comparer(x.data, y.data)) return -1;
                        else return 1;
                    });
                    save.AddAfter(index, temp);
                }
            }
        }

        public int Count
        {
            get { return save.Count; }
        }

        public SingleNodeData<T> First
        {
            get { return save.First; }
        }

        public SingleNodeData<T> Last
        {
            get { return save.Last; }
        }

        public SingleNodeData<T> this[int index]
        {
            get
            {
                if (this.Count <= index || index < 0) throw new IndexOutOfRangeException();
                SingleNodeData<T> result = this.First;
                for (int i = 0; i < index; i += 1)
                    result = result.Next;
                return result;
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
