// Copyright (c) Microsoft. All Rights Reserved.
//  License under the Apache License, Version 2.0.
//  My library with C Sharp.
//  Owner by Pham Hong Phuc

using System;
using System.Numerics;

namespace MyLibrary.Operator
{
    public static class BinaryOperatorService
    {
        public static string AddInteger(string number1, string number2)
        {
            try
            {
                BigInteger element1 = BigInteger.Parse(number1);
                BigInteger element2 = BigInteger.Parse(number2);
                return BigInteger.Add(element1, element2).ToString();
            }
            catch
            {
                throw new Exception("This number format wrong");
            }
        }

        public static string SubtractInteger(string number1, string number2)
        {
            try
            {
                BigInteger element1 = BigInteger.Parse(number1);
                BigInteger element2 = BigInteger.Parse(number2);
                return BigInteger.Subtract(element1, element2).ToString();
            }
            catch
            {
                throw new Exception("This number format wrong");
            }
        }

        public static string MutipilationInteger(string number1, string number2)
        {
            try
            {
                BigInteger element1 = BigInteger.Parse(number1);
                BigInteger element2 = BigInteger.Parse(number2);
                return BigInteger.Multiply(element1, element2).ToString();
            }
            catch
            {
                throw new Exception("This number format wrong");
            }
        }

        public static string DivisionInteger(string number1, string number2)
        {
            if (number2 == "0") throw new DivideByZeroException();
            try
            {
                BigInteger element1 = BigInteger.Parse(number1);
                BigInteger element2 = BigInteger.Parse(number2);
                return BigInteger.Divide(element1, element2).ToString();
            }
            catch
            {
                throw new Exception("This number format wrong");
            }
        }

        public static string DivisionInteger(string number1, string number2, out string remainder)
        {
            try
            {
                BigInteger element1 = BigInteger.Parse(number1);
                BigInteger element2 = BigInteger.Parse(number2);
                BigInteger bRemainder = new BigInteger();
                BigInteger result = BigInteger.DivRem(element1, element2, out bRemainder);
                remainder = bRemainder.ToString();
                return result.ToString();
            }
            catch
            {
                throw new Exception("This number format wrong");
            }
        }

        public static string AddDecimal(string decimal1, string decimal2)
        {
            int need1 = CalculateUtilities.ConvertDecimal(ref decimal1);
            int need2 = CalculateUtilities.ConvertDecimal(ref decimal2);
            int need = 0;
            if (need1 > need2)
            {
                need = need1;
                int temp = need1 - need2;
                for (int i = 0; i < temp; i++) decimal2 += '0';
            }
            else
            {
                need = need2;
                int temp = need2 - need1;
                for (int i = 0; i < temp; i++) decimal1 += '0';
            }
            string result = AddInteger(decimal1, decimal2);
            if (need == 0) return result;
            return UnaryOperatorService.Devision10(result, need);
        }

        public static string SubtractDecimal(string decimal1, string decimal2)
        {
            int need1 = CalculateUtilities.ConvertDecimal(ref decimal1);
            int need2 = CalculateUtilities.ConvertDecimal(ref decimal2);
            int need = 0;
            if (need1 > need2)
            {
                need = need1;
                int temp = need1 - need2;
                for (int i = 0; i < temp; i++) decimal2 += '0';
            }
            else
            {
                need = need2;
                int temp = need2 - need1;
                for (int i = 0; i < temp; i++) decimal1 += '0';
            }
            string result = SubtractInteger(decimal1, decimal2);
            if (need == 0) return result;
            return UnaryOperatorService.Devision10(result, need);
        }

        public static string MutipilationDecimal(string decimal1, string decimal2)
        {
            if (decimal2 == "0") throw new DivideByZeroException();
            int need1 = CalculateUtilities.ConvertDecimal(ref decimal1);
            int need2 = CalculateUtilities.ConvertDecimal(ref decimal2);
            int need = need1 + need2;
            string result = MutipilationInteger(decimal1, decimal2);
            if (need == 0) return result;
            return UnaryOperatorService.Devision10(result, need);
        }

        public static string DivisionDecimal(string decimal1, string decimal2, int accuracy)
        {
            int need1 = CalculateUtilities.ConvertDecimal(ref decimal1);
            int need2 = CalculateUtilities.ConvertDecimal(ref decimal2);
            if (need1 > need2)
            {
                int temp = need1 - need2;
                for (int i = 0; i < temp; i++) decimal2 += '0';
            }
            else
            {
                int temp = need2 - need1;
                for (int i = 0; i < temp; i++) decimal1 += '0';
            }
            string remainder = "";
            string result = DivisionInteger(decimal1, decimal2, out remainder);
            if (accuracy == 0 || remainder == "0") return result;
            for (int i = 0; i < accuracy; i++) remainder += '0';
            string sTemp = DivisionInteger(remainder, decimal2);
            while (sTemp.Length < accuracy) sTemp = '0' + sTemp;
            return result + '.' + sTemp;
        }
    }
}
