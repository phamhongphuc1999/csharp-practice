// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

namespace MyLibrary.Collection.LinkedCollection
{
    public class SingleNodeData<T>
    {
        public SingleNodeData<T> next;
        public T data;

        public SingleNodeData(T data)
        {
            this.data = data;
            this.next = null;
        }

        public static SingleNodeData<T> operator ++(SingleNodeData<T> node)
        {
            node = node.next;
            return node;
        }

        public static SingleNodeData<T> operator +(SingleNodeData<T> node, int value)
        {
            int count = 0;
            while (node != null && count < value)
                node = node.next;
            return node;
        }

        public override string ToString()
        {
            return data.ToString();
        }

        public override bool Equals(object obj)
        {
            DoubleNodeData<T> node = obj as DoubleNodeData<T>;
            if (node == null) return false;
            return this.data.Equals(node.data);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class DoubleNodeData<T>
    {
        public DoubleNodeData<T> next;
        public DoubleNodeData<T> prev;
        public T data;

        public DoubleNodeData(T data)
        {
            this.data = data;
            this.next = null;
            this.prev = null;
        }

        public static DoubleNodeData<T> operator ++(DoubleNodeData<T> node)
        {
            node = node.next;
            return node;
        }

        public static DoubleNodeData<T> operator +(DoubleNodeData<T> node, int value)
        {
            int count = 0;
            while (node != null && count < value)
                node = node.next;
            return node;
        }

        public static DoubleNodeData<T> operator --(DoubleNodeData<T> node)
        {
            node = node.prev;
            return node;
        }

        public static DoubleNodeData<T> operator -(DoubleNodeData<T> node, int value)
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
            DoubleNodeData<T> node = obj as DoubleNodeData<T>;
            if (node == null) return false;
            return this.data.Equals(node.data);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
