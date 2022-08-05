using System.Text.RegularExpressions;

namespace MyNumber.Services
{
    public static class UIntService
    {
        public static bool IsNumber(string number)
        {
            Match match = Regex.Match(number, "[0-9]+");
            return match.Length == number.Length;
        }

        public static string FormatNumber(string number)
        {
            if (!UIntService.IsNumber(number)) throw new NotANumber();
            bool check = true;
            int len = number.Length;
            int count = 0;
            while(count < len && check)
            {
                if (number[count] == '0') count += 1;
                else check = false;
            }
            if (!check) return number.Substring(count);
            else return "0";
        }

        public static int Compare(string number1, string number2)
        {
            string num1 = UIntService.FormatNumber(number1);
            string num2 = UIntService.FormatNumber(number2);
            int len1 = num1.Length;
            int len2 = num2.Length;
            if (len1 > len2) return 1;
            else if (len1 < len2) return -1;
            else
            {
                bool check = true;
                int count = 0;
                int result = 0;
                while(count < len1 && check)
                {
                    if (num1[count] < num2[count])
                    {
                        check = false;
                        result = -1;
                    }
                    else if (num1[count] > num2[count])
                    {
                        check = false;
                        result = 1;
                    }
                    else count += 1;
                }
                return result;
            }
        }

        public static string Add(string number1, string number2)
        {
            string num1 = UIntService.FormatNumber(number1);
            string num2 = UIntService.FormatNumber(number2);
            int len1 = num1.Length;
            int len2 = num2.Length;
            string result = "";
            int count = 1;
            int remain = 0;
            while(count <= len1 && count <= len2)
            {
                int n1 = num1[len1 - count] - 48;
                int n2 = num2[len2 - count] - 48;
                int temp = n1 + n2 + remain;
                result = (temp % 10) + result;
                remain = temp / 10;
                count++;
            }
            while(count <= len1)
            {
                int n1 = num1[len1 - count] - 48;
                int temp = n1 + remain;
                result = temp % 10 + result;
                remain = temp / 10;
                count++;
            }
            while(count <= len2)
            {
                int n2 = num2[len2 - count] - 48;
                int temp = n2 + remain;
                result = temp % 10 + result;
                remain = temp / 10;
                count++;
            }
            if (remain > 0) result = remain + result;
            return result;
        }

        public static string Subtract(string number1, string number2)
        {
            string num1 = UIntService.FormatNumber(number1);
            string num2 = UIntService.FormatNumber(number2);
            int len1 = num1.Length;
            int len2 = num2.Length;
            string result = "";
            int count = 1;
            int remain = 0;
            while (count <= len1 && count <= len2)
            {
                int n1 = num1[len1 - count] - 48;
                int n2 = num2[len2 - count] - 48;
                if (n1 >= n2 + remain)
                {
                    result = (n1 - n2 - remain) + result;
                    remain = 0;
                }
                else
                {
                    result = (n1 + 10 - n2 - remain) + result;
                    remain = 1;
                }
                count++;
            }
            while(count <= len1)
            {
                int n1 = num1[len1 - count] - 48;
                if (n1 >= remain)
                {
                    result = (n1 - remain) + result;
                    remain = 0;
                }
                else
                {
                    result = (10 + n1 - remain) + result;
                    remain = 1;
                }
                count++;
            }
            while(count <= len2)
            {
                int n2 = num2[len2 - count] - 48;
                if (n2 >= remain)
                {
                    result = (n2 - remain) + result;
                    remain = 0;
                }
                else
                {
                    result = (10 + n2 - remain) + result;
                    remain = 1;
                }
                count++;
            }
            return result;
        }

        private static string SingleMultiple(string number, char cNumber)
        {
            string result = "0";
            int cNum = cNumber - 48;
            for (int i = 1; i <= cNum; i++)
            {
                result = UIntService.Add(result, number);
            }
            return result;
        }

        public static string Multiple (string number1, string number2)
        {
            string num1 = UIntService.FormatNumber(number1);
            string num2 = UIntService.FormatNumber(number2);
            if (num1 == "0" || num2 == "0") return "0";
            else
            {
                string result = "";
                foreach(char cNum1 in num1)
                {
                    string temp = UIntService.SingleMultiple(num2, cNum1);
                    result = result + "0";
                    result = UIntService.Add(temp, result);
                }
                return result;
            }
        }

        public static string Divide (string dividend, string divisor)
        {
            if (divisor == "0") throw new DivideByZeroException();
            else if (divisor == "1") return dividend;
            else
            {
                string result = "";
                string remain = "";
                foreach (char cDividend in dividend)
                {
                    remain = remain + cDividend;
                    if (remain == "0")
                    {
                        result = result + "0";
                        remain = "";
                    }
                    else
                    {
                        int remainCompare = UIntService.Compare(remain, divisor);
                        if (remainCompare == 0)
                        {
                            result = result + "1";
                            remain = "";
                        }
                        else if (remainCompare == -1)
                        {
                            if (result.Length > 0) result = result + "0";
                        }
                        else
                        {
                            int count = 1;
                            string total = divisor;
                            string preTotal = "";
                            int check = -1;
                            while (count <= 9 && check == -1)
                            {
                                preTotal = total;
                                total = UIntService.Add(total, remain);
                                count++;
                                check = UIntService.Compare(total, remain);
                            }
                            if (count > 9)
                            {
                                result = result + "9";
                                remain = UIntService.Subtract(remain, preTotal);
                            }
                            else if (check == 0)
                            {
                                result = result + count;
                                remain = "";
                            }
                            else
                            {
                                result = result + (count - 1);
                                remain = UIntService.Subtract(remain, preTotal);
                            }
                        }
                    }
                }
                return result == "" ? "0" : result;
            }
        }

        public static string DivideMod(string dividend, string divisor)
        {
            if (divisor == "0") throw new DivideByZeroException();
            else if (divisor == "1") return "0";
            else
            {
                string result = "0";
                foreach(char cDividend in dividend)
                {
                    if (result == "0") result = cDividend.ToString();
                    else result = result + cDividend;
                    int resultCompare = UIntService.Compare(result, divisor);
                    if (resultCompare == 0) result = "0";
                    else if (resultCompare == 1)
                    {
                        string preTotal = divisor;
                        string total = UIntService.Add(divisor, divisor);
                        int check = UIntService.Compare(total, result);
                        while (check == -1)
                        {
                            preTotal = total;
                            total = UIntService.Add(total, divisor);
                            check = UIntService.Compare(total, result);
                        }
                        if (check == 0) result = "0";
                        else result = UIntService.Subtract(result, preTotal);
                    }
                }
                return UIntService.FormatNumber(result);
            }
        }

        public static (string, string) RealDivide(string dividend, string divisor)
        {
            return (UIntService.Divide(dividend, divisor), UIntService.DivideMod(dividend, divisor));
        }

        public static string Multiply10(string number1, string number2)
        {
            string result = "";
            string temp = "0";
            string tNum2 = number2;
            while (UIntService.Compare(tNum2, "0") == 1)
            {
                (string rInteger, string rDecimal) = UIntService.RealDivide(tNum2, "2");
                if (rDecimal == "1") result = result + temp;
                temp = temp + temp;
                tNum2 = rInteger;
            }
            return number1 + result;
        }

        public static string Pow(string number1, string number2)
        {
            string result = "1";
            string tNum1 = number1;
            string tNum2 = number2;
            while(UIntService.Compare(tNum2, "0") == 1)
            {
                (string rInteger, string rDecimal) = UIntService.RealDivide(tNum2, "2");
                if (rDecimal == "1") result = UIntService.Multiple(result, tNum1);
                tNum1 = UIntService.Multiple(tNum1, tNum1);
                tNum2 = rInteger;
            }
            return result;
        }

        public static string CalculateGreatestCommonFactor(string number1, string number2)
        {
            if (number1 == "0" || number1 == "1") return number1;
            else if (number2 == "0" || number2 == "1") return number2;
            else
            {
                string num1 = number1;
                string num2 = number2;
                int check = UIntService.Compare(num1, num2);
                while (check != 0)
                {
                    if (check == 1) num1 = UIntService.Subtract(num1, num2);
                    else num2 = UIntService.Subtract(num2, num1);
                    check = UIntService.Compare(num1, num2);
                }
                return num1;
            }
        }
    }
}

