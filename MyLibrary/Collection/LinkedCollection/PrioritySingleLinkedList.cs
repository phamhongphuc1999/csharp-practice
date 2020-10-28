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
