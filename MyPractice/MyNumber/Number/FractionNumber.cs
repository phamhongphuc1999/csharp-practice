using MyNumber.Services;

namespace MyNumber.Number
{
  public class FractionNumber
  {
    protected (string, string) coreNumber = ("", "");

    public (string, string) CoreNumber
    {
      get { return coreNumber; }
      set
      {
        coreNumber = FractionService.FormatFraction(value);
      }
    }

    public FractionNumber(string numerator, string denominator)
    {
      this.coreNumber = FractionService.FormatFraction((numerator, denominator));
    }

    public FractionNumber(IntNumber numerator, UIntNumber denominator)
    {
      this.coreNumber = FractionService.FormatFraction((numerator.CoreNumber, denominator.CoreNumber));
    }

    public override string ToString()
    {
      (string numerator, string denominator) = coreNumber;
      if (numerator == "0") return "0";
      else if (denominator == "1") return numerator;
      else return $"{numerator}/${denominator}";
    }

    public bool IsLessThan(FractionNumber number)
    {
      int result = FractionService.Compare(this.CoreNumber, number.CoreNumber);
      return result == -1 ? true : false;
    }
  }
}

