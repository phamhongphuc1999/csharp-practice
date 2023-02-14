using System.Text.RegularExpressions;
using MyNumber.Services;

namespace MyNumber.NumberBase
{
  public static class BaseConvert
  {
    private static Dictionary<char, string> Represent = new Dictionary<char, string>() {
      {'0', "0"}, {'1', "1"}, {'2', "2"}, {'3', "3"}, {'4', "4"}, {'5', "5"}, {'6', "6"}, {'7', "7"}, {'8', "8"}, {'9', "9"},
      { 'a', "10" }, { 'A', "10" }, { 'b', "11" }, {'B', "11"}, { 'c', "12" }, {'C', "12"}, { 'd', "13" }, {'D', "13"},
      { 'e', "14" }, {'E', "14"}, { 'f', "15" }, {'F', "15"} };

    private static Dictionary<string, string> ReverseRepresent = new Dictionary<string, string>(){
      {"0", "0"}, {"1", "1"}, {"2", "2"}, {"3", "3"}, {"4", "4"}, {"5", "5"}, {"6", "6"}, {"7", "7"}, {"8", "8"}, {"9", "9"},
      {"10", "a"}, {"11", "b"}, {"12", "c"}, {"13", "d"}, {"14", "e"}, {"15", "f"}
    };

    private static string GetPattern(NumerationSystem numBase)
    {
      if (numBase == NumerationSystem.BINARY) return "[01]+";
      else if (numBase == NumerationSystem.NUMBER_3) return "[012]+";
      else if (numBase == NumerationSystem.NUMBER_4) return "[0-3]+";
      else if (numBase == NumerationSystem.NUMBER_5) return "[0-4]+";
      else if (numBase == NumerationSystem.NUMBER_6) return "[0-5]+";
      else if (numBase == NumerationSystem.NUMBER_7) return "[0-6]+";
      else if (numBase == NumerationSystem.NUMBER_8) return "[0-7]+";
      else if (numBase == NumerationSystem.NUMBER_9) return "[0-8]+";
      else if (numBase == NumerationSystem.DECIMAL) return "[0-9]+";
      else if (numBase == NumerationSystem.NUMBER_11) return "[a,A,0-9]+";
      else if (numBase == NumerationSystem.NUMBER_12) return "[a,A,b,B,0-9]+";
      else if (numBase == NumerationSystem.NUMBER_13) return "[a-c,A-C,0-9]+";
      else if (numBase == NumerationSystem.NUMBER_14) return "[a-d,A-D,0-9]+";
      else if (numBase == NumerationSystem.NUMBER_15) return "[a-e,A-E,0-9]+";
      else return "[a-f,A-F,0-9]+";
    }

    public static bool UIntValidate(string num, NumerationSystem numBase)
    {
      string pattern = BaseConvert.GetPattern(numBase);
      Match match = Regex.Match(num, pattern);
      return match.Length == num.Length;
    }

    public static string Format(string num, NumerationSystem numBase)
    {
      if (!BaseConvert.UIntValidate(num, numBase)) throw new Exception("Invalid number");
      bool check = true;
      int len = num.Length;
      int count = 0;
      while (count < len && check)
      {
        if (num[count] == '0') count += 1;
        else check = false;
      }
      if (!check) return num.Substring(count);
      else return "0";
    }

    public static string ConvertToDecimal(string originNum, NumerationSystem originBase)
    {
      if (!BaseConvert.UIntValidate(originNum, originBase)) throw new Exception("Invalid number");
      if (originBase == NumerationSystem.DECIMAL) return originNum;
      string result = "0";
      int len = originNum.Length - 1;
      string _base = ((int)originBase).ToString();
      string pow = "1";
      for (int i = len; i >= 0; i--)
      {
        string num = BaseConvert.Represent[originNum[i]];
        result = UIntService.Add(result, UIntService.Multiple(num, pow));
        pow = UIntService.Multiple(_base, pow);
      }
      return result;
    }

    public static string ConvertFromDecimal(string decimalNumber, NumerationSystem targetBase)
    {
      if (!BaseConvert.UIntValidate(decimalNumber, NumerationSystem.DECIMAL)) throw new Exception("Invalid number");
      if (targetBase == NumerationSystem.DECIMAL) return decimalNumber;
      string _base = ((int)targetBase).ToString();
      string result = "";
      string baseNumber = decimalNumber;
      while (UIntService.Compare(_base, baseNumber) <= 0)
      {
        (string, string) temp = UIntService.RealDivide(baseNumber, _base);
        result = ReverseRepresent[temp.Item2] + result;
        baseNumber = temp.Item1;
      }
      result = ReverseRepresent[baseNumber] + result;
      return result;
    }

    public static string Convert(string number, NumerationSystem originBase, NumerationSystem targetBase)
    {
      if (!BaseConvert.UIntValidate(number, originBase)) throw new Exception("Invalid number");
      string decimalNumber = BaseConvert.ConvertToDecimal(number, originBase);
      string result = BaseConvert.ConvertFromDecimal(decimalNumber, targetBase);
      return result;
    }
  }

  public static class IntConvert
  {
    public static bool IntValidate(string num, NumerationSystem numBase)
    {
      char sign = num[0];
      if (!(sign == '0' | sign == '1')) return false;
      return BaseConvert.UIntValidate(num.Substring(1), numBase);
    }

    public static string ConvertToDecimal(string originNum, NumerationSystem originBase)
    {
      if (!IntConvert.IntValidate(originNum, originBase)) throw new Exception("Invalid number");
      char sign = originNum[0];
      string result = BaseConvert.ConvertToDecimal(originNum.Substring(1), originBase);
      return sign + result;
    }

    public static string ConvertFromDecimal(string decimalNumber, NumerationSystem targetBase)
    {
      if (!IntConvert.IntValidate(decimalNumber, targetBase)) throw new Exception("Invalid number");
      char sign = decimalNumber[0];
      string result = BaseConvert.ConvertFromDecimal(decimalNumber.Substring(1), targetBase);
      return sign + result;
    }

    public static string Convert(string number, NumerationSystem originBase, NumerationSystem targetBase)
    {
      if (!IntConvert.IntValidate(number, originBase)) throw new Exception("Invalid number");
      string decimalNumber = IntConvert.ConvertToDecimal(number, originBase);
      string result = IntConvert.ConvertFromDecimal(decimalNumber, targetBase);
      return result;
    }
  }
}
