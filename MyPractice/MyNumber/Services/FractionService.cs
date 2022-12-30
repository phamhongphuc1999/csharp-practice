namespace MyNumber.Services
{
  public static class FractionService
  {
    public static bool IsFraction((string, string) number)
    {
      (string numerator, string denominator) = number;
      return IntService.IsNumber(numerator) && UIntService.IsNumber(denominator);
    }

    public static (string, string) FormatFraction((string, string) number)
    {
      (string numerator, string denominator) = number;
      (int sign, string _numerator) = IntService.GetUIntNumber(numerator);
      string temp = UIntService.CalculateGreatestCommonFactor(_numerator, denominator);
      if (temp == "1") return (numerator, denominator);
      else
      {
        string newNumerator = UIntService.Divide(_numerator, temp);
        string newDenominator = UIntService.Divide(denominator, temp);
        if (sign == 1) return (newNumerator, newDenominator);
        else return ("-" + newNumerator, newDenominator);
      }
    }

    public static int Compare((string, string) number1, (string, string) number2)
    {
      (string numerator1, string denominator1) = number1;
      (string numerator2, string denominator2) = number2;
      string num1 = IntService.Multiple(numerator1, denominator2);
      string num2 = IntService.Multiple(numerator2, denominator1);
      return IntService.Compare(num1, num2);
    }

    public static (string, string) Add((string, string) number1, (string, string) number2)
    {
      (string numerator1, string denominator1) = number1;
      (string numerator2, string denominator2) = number2;
      string newNumerator = IntService.Add(IntService.Multiple(numerator1, denominator2), IntService.Multiple(numerator2, denominator1));
      string newDenominator = IntService.Multiple(denominator1, denominator2);
      return FractionService.FormatFraction((newNumerator, newDenominator));
    }

    public static (string, string) Subtract((string, string) number1, (string, string) number2)
    {
      (string numerator1, string denominator1) = number1;
      (string numerator2, string denominator2) = number2;
      string newNumerator = IntService.Subtract(IntService.Multiple(numerator1, denominator2), IntService.Multiple(numerator2, denominator1));
      string newDenominator = IntService.Multiple(denominator1, denominator2);
      return FractionService.FormatFraction((newNumerator, newDenominator));
    }

    public static (string, string) Multiple((string, string) number1, (string, string) number2)
    {
      (string numerator1, string denominator1) = number1;
      (string numerator2, string denominator2) = number2;
      string newNumerator = IntService.Multiple(numerator1, numerator2);
      string newDenominator = IntService.Multiple(denominator1, denominator2);
      return FractionService.FormatFraction((newNumerator, newDenominator));
    }

    public static (string, string) Divide((string, string) number1, (string, string) number2)
    {
      (string numerator1, string denominator1) = number1;
      (string numerator2, string denominator2) = number2;
      string newNumerator = IntService.Multiple(numerator1, denominator2);
      string newDenominator = IntService.Multiple(denominator1, numerator2);
      return FractionService.FormatFraction((newNumerator, newDenominator));
    }
  }
}

