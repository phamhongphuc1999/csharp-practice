using MyNumber.Services;

namespace MyNumber
{
  public static class EUtils
  {
    public static string GetENumber(string uintNum, int accuracy)
    {
      if (!UIntService.IsNumber(uintNum)) throw new NotANumber();
      string result = "2";
      string count = "2";
      string temp = "1";
      while (UIntService.Compare(count, uintNum) <= 0)
      {
        temp = UIntService.Multiple(temp, count);
        result = DecimalService.Add(DecimalService.Divide("1", temp, accuracy), result);
        count = UIntService.Add(count, "1");
      }
      return result;
    }

    public static string GetEPow(string x, string uintNum, int accuracy)
    {
      if (!UIntService.IsNumber(uintNum)) throw new NotANumber();
      if (!DecimalService.IsNumber(x)) throw new NotANumber();
      string result = DecimalService.Add("1", x);
      string count = "2";
      string temp = "1";
      string xTemp = x;
      while (UIntService.Compare(count, uintNum) <= 0)
      {
        temp = UIntService.Multiple(temp, count);
        xTemp = DecimalService.Multiply(xTemp, x);
        result = DecimalService.Add(DecimalService.Divide(xTemp, temp, accuracy), result);
        count = UIntService.Add(count, "1");
      }
      return result;
    }
  }
}
