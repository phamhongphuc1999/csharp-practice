using System.Text.RegularExpressions;
using MyNumber.Services;

namespace MyNumber.NumberBase
{
  public static class BaseConvert
  {
    private static Dictionary<char, string> Represent = new Dictionary<char, string>() {
      {'0', "0"}, {'1', "1"}, {'2', "2"}, {'3', "3"}, {'4', "4"}, {'5', "5"}, {'6', "6"}, {'7', "7"}, {'8', "8"}, {'9', "9"},
      { 'a', "10" }, { 'A', "10" }, { 'b', "11" }, {'B', "11"},
      { 'c', "12" }, {'C', "12"}, { 'd', "13" }, {'D', "13"},
      { 'e', "14" }, {'E', "14"}, { 'f', "15" }, {'F', "15"} };

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

    public static string ConvertToDecimal(string originNum, NumerationSystem originBase)
    {
      if (BaseConvert.UIntValidate(originNum, originBase))
      {
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
      else throw new Exception("Invalid number");
    }

    public static string ConvertFromDecimal(string decimalNumber, NumerationSystem targetBase)
    {
      if (BaseConvert.UIntValidate(decimalNumber, NumerationSystem.DECIMAL))
      {
        return "";
      }
      else throw new Exception("Invalid number");
    }
  }
}