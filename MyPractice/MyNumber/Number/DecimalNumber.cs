using MyNumber.Services;

namespace MyNumber.Number
{
  public class DecimalNumber : IComparable, IComparable<DecimalNumber>, IEquatable<DecimalNumber>
  {
    protected string coreNumber = "";

    public string CoreNumber
    {
      get { return coreNumber; }
      set
      {
        coreNumber = DecimalService.FormatNumber(value);
      }
    }

    public DecimalNumber(string coreNumber)
    {
      this.CoreNumber = DecimalService.FormatNumber(coreNumber);
    }

    public int CompareTo(object? obj)
    {
      DecimalNumber? num = obj as DecimalNumber;
      if (num is null) return 1;
      else return this.CompareTo(num);
    }

    public int CompareTo(DecimalNumber? other)
    {
      if (other is null) return 1;
      else return DecimalService.Compare(this.CoreNumber, other.CoreNumber);
    }

    public bool Equals(DecimalNumber? other)
    {
      if (other is null) return false;
      else return this.Equals(other);
    }

    public override bool Equals(object? obj)
    {
      DecimalNumber? num = obj as DecimalNumber;
      if (num is null) return true;
      else return this.Equals(num);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return this.CoreNumber;
    }

    public static DecimalNumber ENumber(string uintNum, int accuracy)
    {
      string result = EUtils.GetENumber(uintNum, accuracy);
      return new DecimalNumber(result);
    }

    public static DecimalNumber EPow(string x, string uintNum, int accuracy)
    {
      string result = EUtils.GetEPow(x, uintNum, accuracy);
      return new DecimalNumber(result);
    }

    public DecimalNumber Ceiling(int exponent)
    {
      string result = DecimalService.Ceiling(this.CoreNumber, exponent);
      return new DecimalNumber(result);
    }

    public bool IsLessThan(DecimalNumber number)
    {
      int result = DecimalService.Compare(this.CoreNumber, number.CoreNumber);
      return result == -1 ? true : false;
    }

    public static bool operator <(DecimalNumber number1, DecimalNumber number2)
    {
      return number1.IsLessThan(number2);
    }

    public bool IsGresterThan(DecimalNumber number)
    {
      int result = DecimalService.Compare(this.CoreNumber, number.CoreNumber);
      return result == 1 ? true : false;
    }

    public static bool operator >(DecimalNumber number1, DecimalNumber number2)
    {
      return number1.IsGresterThan(number2);
    }

    public bool IsEqual(DecimalNumber number)
    {
      int result = DecimalService.Compare(this.CoreNumber, number.CoreNumber);
      return result == 0 ? true : false;
    }

    public static bool operator ==(DecimalNumber number1, DecimalNumber number2)
    {
      return number1.IsEqual(number2);
    }

    public bool IsNotEqual(DecimalNumber number)
    {
      int result = DecimalService.Compare(this.CoreNumber, number.CoreNumber);
      return result != 0 ? true : false;
    }

    public static bool operator !=(DecimalNumber number1, DecimalNumber number2)
    {
      return number1.IsNotEqual(number2);
    }

    public (int, UIntNumber, UIntNumber) GetIntegerAndDecimal()
    {
      (int sign, string integerPart, string decimalPart) = DecimalService.GetIntegerAndDecimal(this.CoreNumber);
      return (sign, new UIntNumber(integerPart), new UIntNumber(decimalPart));
    }

    public static bool IsNumber(string number)
    {
      return DecimalService.IsNumber(number);
    }

    public static DecimalNumber Parst(string coreNumber)
    {
      return new DecimalNumber(coreNumber);
    }

    public static int Compare(DecimalNumber number1, DecimalNumber number2)
    {
      return DecimalService.Compare(number1.CoreNumber, number2.CoreNumber);
    }

    public static DecimalNumber operator +(DecimalNumber number)
    {
      return number;
    }

    public static DecimalNumber operator -(DecimalNumber number)
    {
      string sNumber = number.CoreNumber;
      if (sNumber[0] == '-') return new DecimalNumber(sNumber.Substring(1));
      else return new DecimalNumber($"-${sNumber}");
    }

    public DecimalNumber Add(DecimalNumber number)
    {
      string result = DecimalService.Add(this.CoreNumber, number.CoreNumber);
      return new DecimalNumber(result);
    }

    public static DecimalNumber operator +(DecimalNumber number1, DecimalNumber number2)
    {
      return number1.Add(number2);
    }

    public static DecimalNumber operator +(DecimalNumber number1, UIntNumber number2)
    {
      string result = DecimalService.Add(number1.CoreNumber, number2.CoreNumber);
      return new DecimalNumber(result);
    }

    public static DecimalNumber operator +(UIntNumber number1, DecimalNumber number2)
    {
      string result = DecimalService.Add(number1.CoreNumber, number2.CoreNumber);
      return new DecimalNumber(result);
    }

    public static DecimalNumber operator +(DecimalNumber number1, IntNumber number2)
    {
      string result = DecimalService.Add(number1.CoreNumber, number2.CoreNumber);
      return new DecimalNumber(result);
    }

    public static DecimalNumber operator +(IntNumber number1, DecimalNumber number2)
    {
      string result = DecimalService.Add(number1.CoreNumber, number2.CoreNumber);
      return new DecimalNumber(result);
    }

    public DecimalNumber Subtract(DecimalNumber number)
    {
      string result = DecimalService.Subtract(this.CoreNumber, number.CoreNumber);
      return new DecimalNumber(result);
    }

    public static DecimalNumber operator -(DecimalNumber number1, DecimalNumber number2)
    {
      return number1.Add(number2);
    }

    public static DecimalNumber operator -(DecimalNumber number1, UIntNumber number2)
    {
      string result = DecimalService.Subtract(number1.CoreNumber, number2.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public static DecimalNumber operator -(UIntNumber number1, DecimalNumber number2)
    {
      string result = DecimalService.Subtract(number1.CoreNumber, number2.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public static DecimalNumber operator -(DecimalNumber number1, IntNumber number2)
    {
      string result = DecimalService.Subtract(number1.CoreNumber, number2.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public static DecimalNumber operator -(IntNumber number1, DecimalNumber number2)
    {
      string result = DecimalService.Subtract(number1.CoreNumber, number2.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public DecimalNumber Multiply10(UIntNumber number)
    {
      return DecimalNumber.Parst(DecimalService.Multiply10(this.CoreNumber, number.CoreNumber));
    }

    public DecimalNumber Multiply(DecimalNumber number)
    {
      string result = DecimalService.Multiply(this.CoreNumber, number.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public static DecimalNumber operator *(DecimalNumber number1, DecimalNumber number2)
    {
      return number1.Multiply(number2);
    }

    public static DecimalNumber operator *(DecimalNumber number1, UIntNumber number2)
    {
      string result = DecimalService.Multiply(number1.CoreNumber, number2.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public static DecimalNumber operator *(UIntNumber number1, DecimalNumber number2)
    {
      string result = DecimalService.Multiply(number1.CoreNumber, number2.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public static DecimalNumber operator *(DecimalNumber number1, IntNumber number2)
    {
      string result = DecimalService.Multiply(number1.CoreNumber, number2.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public static DecimalNumber operator *(IntNumber number1, DecimalNumber number2)
    {
      string result = DecimalService.Multiply(number1.CoreNumber, number2.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public DecimalNumber Divide10(UIntNumber number)
    {
      return DecimalNumber.Parst(DecimalService.DivideUInt10(this.CoreNumber, number.CoreNumber));
    }

    public DecimalNumber Divide(DecimalNumber number, int accuracy)
    {
      return DecimalNumber.Parst(DecimalService.Divide(this.CoreNumber, number.CoreNumber, accuracy));
    }

    public static DecimalNumber operator /(DecimalNumber number1, DecimalNumber number2)
    {
      return number1.Divide(number2, 10);
    }

    public static DecimalNumber operator /(DecimalNumber number1, UIntNumber number2)
    {
      return DecimalNumber.Parst(DecimalService.Divide(number1.CoreNumber, number2.CoreNumber, 10));
    }

    public static DecimalNumber operator /(UIntNumber number1, DecimalNumber number2)
    {
      return DecimalNumber.Parst(DecimalService.Divide(number1.CoreNumber, number2.CoreNumber, 10));
    }

    public static DecimalNumber operator /(DecimalNumber number1, IntNumber number2)
    {
      return DecimalNumber.Parst(DecimalService.Divide(number1.CoreNumber, number2.CoreNumber, 10));
    }

    public static DecimalNumber operator /(IntNumber number1, DecimalNumber number2)
    {
      return DecimalNumber.Parst(DecimalService.Divide(number1.CoreNumber, number2.CoreNumber, 10));
    }

    public DecimalNumber Pow(UIntNumber number)
    {
      string result = DecimalService.Pow(this.CoreNumber, number.CoreNumber);
      return DecimalNumber.Parst(result);
    }

    public DecimalNumber Pow(IntNumber number)
    {
      (int sign, UIntNumber num) = number.GetUInt();
      string result = DecimalService.Pow(this.CoreNumber, num.CoreNumber);
      if (sign == 1) return DecimalNumber.Parst(result);
      else
      {
        string result1 = DecimalService.Divide("1", result, 10);
        return DecimalNumber.Parst(result1);
      }
    }
  }
}
