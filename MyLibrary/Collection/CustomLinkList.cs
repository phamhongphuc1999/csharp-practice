using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.Collection
{
    public class Data<T>
    {
        public Data<T> next;
        public T data;

        public Data(Data<T> next, T data)
        {
            this.next = next;
            this.data = data;
        }

        public Data(T data)
        {
            this.data = data;
            this.next = null;
        }
    }

    public class CustomLinkList<T>
    {
        protected int _size;
        protected Data<T> begin;
        protected Data<T> end;

        public CustomLinkList()
        {
            begin = end = null;
        }

        public CustomLinkList(IEnumerable<T> collection)
        {
            Data<T> first = new Data<T>(collection.ElementAt(0));
            begin = first;
            int count = collection.Count();
            for(int i = 1; i < count; i++)
            {

            }
        }
    }
}
