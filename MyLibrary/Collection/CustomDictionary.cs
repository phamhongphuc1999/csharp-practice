﻿// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

namespace MyLibrary.Collection
{
    public class KeyValueData<TKey, TValue>
    {
        private TKey key;
        private TValue value;

        public KeyValueData()
        {
        }

        public KeyValueData(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public TKey Key
        {
            get { return key; }
        }

        public TValue Value
        {
            get { return value; }
        }

        public override string ToString()
        {
            return $"({Key.ToString()}, {Value.ToString()})";
        }
    }

    //internal class HashClass
    //{
    //    public static int HashBaseASCII()
    //}

    public class CustomDictionary
    {
        
    }
}
