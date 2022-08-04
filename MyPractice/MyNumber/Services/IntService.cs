using System.Text.RegularExpressions;

namespace MyNumber.Services
{
    public static class IntService
    {
        public static bool IsNumber(string number)
        {
            Regex re = new Regex("^[- | 0-9][0-9]*");
            return re.IsMatch(number);
        }

        public static (int, string) GetUIntNumber(string number)
        {
            if (number[0] == '-') return (-1, number.Substring(1));
            else return (1, number);
        }

        public static string FormatNumber(string number)
        {
            if (!IntService.IsNumber(number)) throw new NotANumber();
            (int sign, string integerNum) = IntService.GetUIntNumber(number);
            string result = UIntService.FormatNumber(integerNum);
            if (result == "0") return "0";
            else if (sign == 1) return result;
            else return "-" + result;
        }

        public static (int, string) DeepIntNumber(string number)
        {
            string result = IntService.FormatNumber(number);
            return IntService.GetUIntNumber(result);
        }

        public static int Compare(string number1, string number2)
        {
            (int sign1, string realNum1) = IntService.DeepIntNumber(number1);
            (int sign2, string realNum2) = IntService.DeepIntNumber(number2);
            if (sign1 < sign2) return -1;
            else if (sign1 > sign2) return 1;
            else return sign1 * UIntService.Compare(realNum1, realNum2);
        }

        public static string Add(string number1, string number2)
        {
            (int sign1, string realNum1) = IntService.DeepIntNumber(number1);
            (int sign2, string realNum2) = IntService.DeepIntNumber(number2);
            if (sign1 == -1 && sign2 == -1) return "-" + UIntService.Add(realNum1, realNum2);
            else if (sign1 == 1 && sign2 == 1) return UIntService.Add(realNum1, realNum2);
            else
            {
                int compare = UIntService.Compare(realNum1, realNum2);
                if (compare == 0) return "0";
                else if (compare == -1)
                {
                    if (sign1 == -1) return UIntService.Subtract(realNum2, realNum1);
                    else return "-" + UIntService.Subtract(realNum2, realNum1);
                }
                else
                {
                    if (sign1 == -1) return "-" + UIntService.Subtract(realNum1, realNum2);
                    else return UIntService.Subtract(realNum1, realNum2);
                }
            }
        }

        public static string Subtract(string number1, string number2)
        {
            (int sign1, string realNum1) = IntService.DeepIntNumber(number1);
            (int sign2, string realNum2) = IntService.DeepIntNumber(number2);
            if (sign1 == -1 && sign2 == 1) return "-" + UIntService.Add(realNum1, realNum2);
            else if (sign1 == 1 && sign2 == -1) return UIntService.Add(realNum1, realNum2);
            else
            {
                int compare = UIntService.Compare(realNum1, realNum2);
                if (compare == 0) return "0";
                else if (compare == 1)
                {
                    if (sign1 == 1) return UIntService.Subtract(realNum1, realNum2);
                    else return "-" + UIntService.Subtract(realNum1, realNum2);
                }
                else
                {
                    if (sign1 == 1) return "-" + UIntService.Subtract(realNum2, realNum1);
                    else return UIntService.Subtract(realNum2, realNum1);
                }
            }
        }

        public static string Multiple(string number1, string number2)
        {
            (int sign1, string realNum1) = IntService.DeepIntNumber(number1);
            (int sign2, string realNum2) = IntService.DeepIntNumber(number2);
            string result = UIntService.Multiple(realNum1, realNum2);
            if (sign1 * sign2 > 0) return result;
            else return "-" + result;
        }

        public static string Divide(string dividend, string divisor)
        {
            (int sign1, string realDividend) = IntService.DeepIntNumber(dividend);
            (int sign2, string realDivisor) = IntService.DeepIntNumber(divisor);
            string result = UIntService.Divide(realDividend, realDivisor);
            if (sign1 * sign2 > 0) return result;
            else return "-" + result;
        }

        public static string Multiple10(string number1, string number2)
        {
            (int sign1, string realNum1) = IntService.DeepIntNumber(number1);
            string result = UIntService.Multiply10(realNum1, number2);
            if (sign1 == -1) return "-" + result;
            else return result;
        }

        public static string Pow(string number1, string number2)
        {
            (int sign1, string realNum1) = IntService.DeepIntNumber(number1);
            string result = UIntService.Pow(realNum1, number2);
            string modNum2 = UIntService.DivideMod(number2, "2");
            if (sign1 == -1 && modNum2 == "1") return "-" + result;
            else return result;
        }
    }
}

