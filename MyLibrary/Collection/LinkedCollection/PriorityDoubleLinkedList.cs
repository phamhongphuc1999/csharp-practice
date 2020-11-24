// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System.Collections;
using System.Collections.Generic;

namespace MyLibrary.Collection.LinkedCollection
{
    class PriorityDoubleLinkedList<T> : IEnumerable<DoubleNodeData<T>>
    {
        public IEnumerator<DoubleNodeData<T>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        private class PriorityDoubleLinkedListIEnumerator : IEnumerator<DoubleNodeData<T>>
        {
            public DoubleNodeData<T> Current => throw new System.NotImplementedException();

            object IEnumerator.Current => throw new System.NotImplementedException();

            public void Dispose()
            {
                throw new System.NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new System.NotImplementedException();
            }

            public void Reset()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
