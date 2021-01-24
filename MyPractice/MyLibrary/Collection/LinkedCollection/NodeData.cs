// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

namespace MyLibrary.Collection.LinkedCollection
{
    public class SingleNodeData<T>
    {
        public SingleNodeData<T> Next { get; set; }
        public T data;

        public SingleNodeData(T data)
        {
            this.data = data;
            this.Next = null;
        }

        public static SingleNodeData<T> operator ++(SingleNodeData<T> node)
        {
            node = node.Next;
            return node;
        }

        public static SingleNodeData<T> operator +(SingleNodeData<T> node, int value)
        {
            int count = 0;
            while (node != null && count < value)
                node = node.Next;
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
        public DoubleNodeData<T> Next { get; set; }
        public DoubleNodeData<T> Prev { get; set; }
        public T data;

        public DoubleNodeData(T data)
        {
            this.data = data;
            this.Next = null;
            this.Prev = null;
        }

        public static DoubleNodeData<T> operator ++(DoubleNodeData<T> node)
        {
            node = node.Next;
            return node;
        }

        public static DoubleNodeData<T> operator +(DoubleNodeData<T> node, int value)
        {
            int count = 0;
            while (node != null && count < value)
                node = node.Next;
            return node;
        }

        public static DoubleNodeData<T> operator --(DoubleNodeData<T> node)
        {
            node = node.Prev;
            return node;
        }

        public static DoubleNodeData<T> operator -(DoubleNodeData<T> node, int value)
        {
            int count = 0;
            while (node != null && count < value)
                node = node.Prev;
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
