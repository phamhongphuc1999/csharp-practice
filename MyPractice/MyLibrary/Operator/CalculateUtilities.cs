// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

namespace MyLibrary.Operator
{
    public static class CalculateUtilities
    {
        public static string StandardizedDisplay(string element)
        {
            bool sign = false;
            if (element[0] == '-')
            {
                sign = true;
                element = element.Substring(1);
            }
            int index = element.IndexOf('.');
            int length = element.Length - 1;
            string _integer = "", _decimal = "";
            if (index == 0)
            {
                _integer = "0";
                _decimal = element.Substring(1);
            }
            else if (index == length) _integer = element.Substring(0, length);
            else if (index < 0) _integer = element;
            else
            {
                string[] temp = element.Split('.');
                _integer = temp[0];
                _decimal = temp[1];
            }
            while (_integer.Length > 1)
                if (_integer[0] == '0') _integer = _integer.Substring(1);
                else break;
            while (true)
            {
                int dLength = _decimal.Length - 1;
                if (dLength < 0) break;
                if (_decimal[dLength] == '0') _decimal = _decimal.Substring(0, dLength);
                else break;
            }
            string result = _decimal.Length == 0 ? _integer : _integer + '.' + _decimal;
            return sign ? '-' + result : result;
        }

        public static int ConvertDecimal(ref string dec)
        {
            int length = dec.Length;
            int index = dec.IndexOf('.');
            if (index < 0) return 0;
            string[] temp = dec.Split('.');
            if (temp.Length > 1) dec = temp[0] + temp[1];
            return length - index - 1;
        }
    }
}
