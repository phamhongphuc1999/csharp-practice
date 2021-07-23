// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Numerics;

namespace MyLibrary.Operator
{
    public static class UnaryOperatorService
    {
        public static string Mutipilation10(string number, int n)
        {
            if (n < 0) return Devision10(number, -n);
            if (n == 0) return number;
            int index = number.IndexOf('.');
            int length = number.Length;
            if (index < 0) index = length;
            else number = number.Remove(index, 1);
            int _index = index + n;
            if (_index < length) number = number.Insert(_index, ".");
            else if (_index > length)
            {
                int temp = _index - length;
                for (int i = 0; i < temp; i++) number += '0';
            }
            return CalculateUtilities.StandardizedDisplay(number);
        }

        public static string Devision10(string number, int n)
        {
            if (n < 0) return Mutipilation10(number, -n);
            if (n == 0) return number;
            int index = number.IndexOf('.');
            int length = number.Length;
            if (index < 0) index = length;
            else number = number.Remove(index, 1);
            int _index = index - n;
            if (_index > 0) number = number.Insert(_index, ".");
            else if (_index <= 0)
            {
                _index = -_index;
                for (int i = 0; i < _index; i++) number = '0' + number;
                number = "0." + number;
            }
            return CalculateUtilities.StandardizedDisplay(number);
        }

        public static string InverseNumber(string number)
        {
            return BinaryOperatorService.DivisionDecimal("1", number, 20);
        }

        public static string ExponentialInteger(string number, int exponent)
        {
            BigInteger element = BigInteger.Parse(number);
            return BigInteger.Pow(element, exponent).ToString();
        }

        public static string ExponentialDecimal(string number, int exponent)
        {
            if (exponent == 1) return number;
            else if (exponent % 2 == 0)
            {
                string result = ExponentialDecimal(number, exponent / 2);
                return BinaryOperatorService.MutipilationDecimal(result, result);
            }
            else
            {
                string result = ExponentialDecimal(number, exponent / 2);
                result = BinaryOperatorService.MutipilationDecimal(result, result);
                return BinaryOperatorService.MutipilationDecimal(result, number);
            }
        }

        private static bool IsSqrtInteger(BigInteger number, BigInteger root, int baseNumber)
        {
            BigInteger lowerBound = BigInteger.Pow(root, baseNumber);
            BigInteger upperBound = BigInteger.Pow(root + 1, baseNumber);
            return (number >= lowerBound && number < upperBound);
        }

        public static string SquareInteger(string number, int baseNumber)
        {
            BigInteger element = BigInteger.Parse(number);
            if (element == 0) return "0";
            if (element < 0 && baseNumber % 2 == 0) throw new ArithmeticException("NaN");
            int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(element, baseNumber)));
            BigInteger root = BigInteger.One << (bitLength / 2);
            while (!IsSqrtInteger(element, root, baseNumber))
            {
                root += element / root;
                root /= 2;
            }
            return root.ToString();
        }

        public static string SquareInteger(string number, int baseNumber, out string remainder)
        {
            BigInteger element = BigInteger.Parse(number);
            if (element == 0)
            {
                remainder = "0";
                return "0";
            }
            if (element < 0 && baseNumber % 2 == 0) throw new ArithmeticException("NaN");
            int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(element, baseNumber)));
            BigInteger root = BigInteger.One << (bitLength / 2);
            while (!IsSqrtInteger(element, root, baseNumber))
            {
                root += element / root;
                root /= 2;
            }
            remainder = (element - root * root).ToString();
            return root.ToString();
        }

        public static string SquareDecimal(string _decimal, int baseNumber, int accuracy)
        {
            int need = CalculateUtilities.ConvertDecimal(ref _decimal);
            int temp = need / baseNumber;
            int more = 0;
            if (need % baseNumber > 0) more = need * (temp + 1) - need;
            for (int i = 0; i < more; i++) _decimal += '0';
            string result = SquareInteger(_decimal, baseNumber);
            return Devision10(result, temp);
        }
    }
}
