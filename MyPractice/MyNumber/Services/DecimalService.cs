using System.Text.RegularExpressions;

namespace MyNumber.Services
{
  internal enum Mode
  {
    NORMAL, MULTIPLY, DIVIDE
  }

  public static class DecimalService
  {
    public static (int, string, string) GetIntegerAndDecimal(string number)
    {
      int sign = 1;
      string realNum = number;
      if (realNum[0] == '-')
      {
        sign = -1;
        realNum = realNum.Substring(1);
      }
      if (realNum[0] == '.') return (sign, "0", realNum.Substring(1));
      else
      {
        string[] parts = realNum.Split('.');
        if (parts.Length > 1) return (sign, parts[0], parts[1]);
        else return (sign, parts[0], "");
      }
    }

    public static string FormatNumber(string number)
    {
      (int sign, string intNumber, string decimalNumber) = DecimalService.GetIntegerAndDecimal(number);
      string intNumberFormat = UIntService.FormatNumber(intNumber);
      int count = 1;
      int len = decimalNumber.Length;
      bool check = true;
      string decimalNumberFormat = "";
      while (count <= len && check)
      {
        if (decimalNumber[len - count] == '0') count++;
        else check = false;
      }
      if (!check) decimalNumberFormat = decimalNumber.Substring(0, len - count);
      else decimalNumberFormat = "";
      string result = "";
      if (decimalNumberFormat == "") result = intNumberFormat;
      else result = intNumberFormat + "." + decimalNumberFormat;
      return sign == -1 ? "-" + result : result;
    }

    public static (int, string, string) DeepGetIntegerAndDecimal(string number)
    {
      string numberFormat = DecimalService.FormatNumber(number);
      return DecimalService.GetIntegerAndDecimal(numberFormat);
    }

    public static bool IsNumber(string number)
    {
      Match match = Regex.Match(number, "[0-9]*.[0-9]*");
      if (match.Length == number.Length) return true;
      else
      {
        match = Regex.Match(number, "-[0-9]*.[0-9]*");
        return match.Length == number.Length;
      }
    }

    public static string DivideUInt10(string number, string uintNum)
    {
      int lenInt = number.Length;
      string len = lenInt.ToString();
      int lenCompare = UIntService.Compare(len, uintNum);
      if (lenCompare == 0) return $"0.${number}";
      else if (lenCompare == 1)
      {
        int index = Int32.Parse(uintNum);
        string integerPart = number.Substring(0, lenInt - index - 1);
        string decimalPart = number.Substring(lenInt - index);
        return $"{integerPart}.{decimalPart}";
      }
      else
      {
        string result = "";
        string temp = "0";
        string tNum2 = UIntService.Subtract(uintNum, len);
        while (UIntService.Compare(tNum2, "0") == 1)
        {
          (string rInteger, string rDecimal) = UIntService.RealDivide(tNum2, "2");
          if (rDecimal == "1") result = result + temp;
          temp = temp + temp;
          tNum2 = rInteger;
        }
        return $"0.${result}${number}";
      }
    }

    public static int Compare(string number1, string number2)
    {
      (int sign1, string intNumber1, string decimalNumber1) = DecimalService.DeepGetIntegerAndDecimal(number1);
      (int sign2, string intNumber2, string decimalNumber2) = DecimalService.DeepGetIntegerAndDecimal(number2);
      if (sign1 == -1 && sign2 == 1) return -1;
      else if (sign1 == 1 && sign2 == -1) return 1;
      else
      {
        int len1 = decimalNumber1.Length;
        int len2 = decimalNumber2.Length;
        int count = 0;
        bool check = true;
        int result = 1;
        while (count < len1 && count < len2 && check)
        {
          if (decimalNumber1[count] < decimalNumber2[count])
          {
            result = -1;
            check = false;
          }
          else if (decimalNumber1[count] > decimalNumber2[count]) check = false;
          count++;
        }
        if (!check) return sign1 * result;
        else if (count < len1) return sign1;
        else if (count < len2) return -sign1;
        else return 0;
      }
    }

    private static string MultiplyUIntDecimal10(string number, string uintNum)
    {
      (int sign, string intNumber, string decimalNumber) = DecimalService.DeepGetIntegerAndDecimal(number);
      string result = "";
      if (decimalNumber == "0") result = UIntService.Multiply10(number, uintNum);
      else
      {
        string lenDecimal = decimalNumber.Length.ToString();
        int decimalCompare = UIntService.Compare(lenDecimal, uintNum);
        if (decimalCompare == 0) result = intNumber + decimalNumber;
        else if (decimalCompare == 1)
        {
          int index = Int32.Parse(uintNum) - 1;
          string integerPart = intNumber + decimalNumber.Substring(0, index);
          string decimalPart = decimalNumber.Substring(index + 1);
          result = $"${integerPart}.${decimalNumber}";
        }
        else
        {
          string temp = UIntService.Multiply10(decimalNumber, UIntService.Subtract(uintNum, lenDecimal));
          result = intNumber + temp;
        }
      }
      return sign == 1 ? DecimalService.FormatNumber(result) : "-" + DecimalService.FormatNumber(result);
    }

    private static string DivideUIntDecimal10(string number, string uintNum)
    {
      (int sign, string intNumber, string decimalNumber) = DecimalService.DeepGetIntegerAndDecimal(number);
      string temp = DecimalService.DivideUInt10(intNumber, uintNum);
      string result = temp + decimalNumber;
      return sign == 1 ? DecimalService.FormatNumber(result) : "-" + DecimalService.FormatNumber(result);
    }

    public static string Multiply10(string number, string intNum)
    {
      string sign = "";
      string num = number;
      if (number[0] == '-')
      {
        sign = "-";
        num = number.Substring(1);
      }
      if (intNum[0] != '-') return sign + DecimalService.MultiplyUIntDecimal10(num, intNum);
      else return sign + DecimalService.DivideUIntDecimal10(num, intNum.Substring(1));
    }

    private static (string, string, string) TransformInt(string number1, string number2, Mode mode)
    {
      (int sign1, string intNumber1, string decimalNumber1) = DecimalService.DeepGetIntegerAndDecimal(number1);
      (int sign2, string intNumber2, string decimalNumber2) = DecimalService.DeepGetIntegerAndDecimal(number2);
      string decimalLen1 = decimalNumber1.Length.ToString();
      string decimalLen2 = decimalNumber2.Length.ToString();
      string tempNum1 = "";
      string tempNum2 = "";
      int decimalCompare = UIntService.Compare(decimalLen1, decimalLen2);
      string bigLen = decimalLen1;
      string smallLen = decimalLen2;
      if (decimalCompare >= 0)
      {
        tempNum1 = DecimalService.Multiply10(number1, decimalLen1);
        tempNum2 = DecimalService.Multiply10(number2, decimalLen1);
      }
      else
      {
        tempNum1 = DecimalService.Multiply10(number1, decimalLen2);
        tempNum2 = DecimalService.Multiply10(number2, decimalLen2);
        bigLen = decimalLen2;
        smallLen = decimalLen1;
      }
      if (mode == Mode.NORMAL) return (tempNum1, tempNum2, bigLen);
      else if (mode == Mode.MULTIPLY) return (tempNum1, tempNum2, IntService.Add(bigLen, smallLen));
      else return (tempNum1, tempNum2, bigLen);
    }

    public static string Add(string number1, string number2)
    {
      (string tempNum1, string tempNum2, string len) = DecimalService.TransformInt(number1, number2, Mode.NORMAL);
      return DecimalService.DivideUIntDecimal10(IntService.Add(tempNum1, tempNum2), len);
    }

    public static string Subtract(string number1, string number2)
    {
      (string tempNum1, string tempNum2, string len) = DecimalService.TransformInt(number1, number2, Mode.NORMAL);
      return DecimalService.DivideUIntDecimal10(IntService.Subtract(tempNum1, tempNum2), len);
    }

    public static string Multiply(string number1, string number2)
    {
      (string tempNum1, string tempNum2, string len) = DecimalService.TransformInt(number1, number2, Mode.MULTIPLY);
      return DecimalService.DivideUIntDecimal10(IntService.Multiple(tempNum1, tempNum2), len);
    }

    public static string Ceiling(string number, int exponent)
    {
      (int sign, string integerNum, string decimalNum) = DecimalService.DeepGetIntegerAndDecimal(number);
      int len = decimalNum.Length;
      if (len <= exponent) return number;
      else if (exponent == 0) return integerNum;
      else
      {
        string exDecimalNum = decimalNum.Substring(0, exponent - 1);
        int cNum = decimalNum[exponent] - 48;
        bool realUp = (sign == 1 && cNum >= 5) || (sign == -1 && cNum < 5);
        if (realUp)
        {
          string moreUInt = UIntService.Divide("1", exDecimalNum);
          exDecimalNum = DecimalService.Add($"0.${exDecimalNum}", moreUInt);
          if (sign == 1) return $"${integerNum}.${exDecimalNum.Substring(2)}";
          else return $"-${integerNum}.${exDecimalNum.Substring(2)}";
        }
        else
        {
          if (sign == 1) return $"${integerNum}.${exDecimalNum}";
          else return $"-${integerNum}.${exDecimalNum}";
        }
      }
    }

    public static string Divide(string number1, string number2, int accuracy)
    {
      (string tempNum1, string tempNum2, string len) = DecimalService.TransformInt(number1, number2, Mode.DIVIDE);
      (int sign1, string tNum1) = IntService.GetUIntNumber(tempNum1);
      (int sign2, string tNum2) = IntService.GetUIntNumber(tempNum2);
      (string rawResult, string remain) = UIntService.RealDivide(tNum1, tNum2);
      string remainResult = "";
      int _len = (tNum2.Length + 1);
      string sLen = _len.ToString();
      int rAccuracy = 0;
      while (rAccuracy < accuracy && remain != "0")
      {
        remain = UIntService.Multiply10(remain, sLen);
        (string temp1, string temp2) = UIntService.RealDivide(remain, tNum2);
        while (temp1.Length < _len) temp1 = "0" + temp1;
        remainResult = remainResult + temp1;
        rAccuracy = remainResult.Length;
        remain = temp2;
      }
      if (sign1 * sign2 > 0) return DecimalService.Ceiling($"${rawResult}.${remainResult}", accuracy);
      else return DecimalService.Ceiling($"-${rawResult}.${remainResult}", accuracy);
    }

    public static string Pow(string number, string uintNum)
    {
      (int sign, string intNumber, string decimalNumber) = DecimalService.DeepGetIntegerAndDecimal(number);
      string decimalLen = decimalNumber.Length.ToString();
      string temp = UIntService.Pow(intNumber + decimalNumber, uintNum);
      string result = DecimalService.DivideUIntDecimal10(temp, DecimalService.Multiply(decimalLen, uintNum));
      if (sign == 1) return result;
      else
      {
        if (UIntService.DivideMod(uintNum, "2") == "0") return result;
        else return $"-${result}";
      }
    }
  }
}
