﻿using MyNumber.Services;
using MyNumber.NumberBase;

namespace MyNumber.Number
{
  public class UIntNumber : IComparable, IComparable<UIntNumber>, IEquatable<UIntNumber>
  {
    protected string coreNumber = "";

    public string CoreNumber
    {
      get { return coreNumber; }
      set
      {
        coreNumber = UIntService.FormatNumber(value);
      }
    }

    public UIntNumber(string coreNumber, NumerationSystem numBase = NumerationSystem.DECIMAL)
    {
      if (numBase == NumerationSystem.DECIMAL) this.CoreNumber = UIntService.FormatNumber(coreNumber);
      else
      {
        string decimalNum = BaseConvert.ConvertToDecimal(coreNumber, numBase);
        this.CoreNumber = UIntService.FormatNumber(decimalNum);
      }
    }

    public int CompareTo(object? obj)
    {
      UIntNumber? num = obj as UIntNumber;
      if (num is null) return 1;
      else return this.CompareTo(num);
    }

    public bool Equals(UIntNumber? other)
    {
      if (other is null) return false;
      else return this.Equals(other);
    }

    public int CompareTo(UIntNumber? other)
    {
      if (other is null) return 1;
      else return UIntService.Compare(this.CoreNumber, other.CoreNumber);
    }

    public override bool Equals(object? obj)
    {
      UIntNumber? num = obj as UIntNumber;
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

    public bool IsLessThan(UIntNumber number)
    {
      int result = UIntService.Compare(this.CoreNumber, number.CoreNumber);
      return result == -1 ? true : false;
    }

    public static bool operator <(UIntNumber number1, UIntNumber number2)
    {
      return number1.IsLessThan(number2);
    }

    public bool IsLessThanOrEqual(UIntNumber number)
    {
      int result = UIntService.Compare(this.CoreNumber, number.CoreNumber);
      return result <= 0 ? true : false;
    }

    public static bool operator <=(UIntNumber number1, UIntNumber number2)
    {
      return number1.IsLessThanOrEqual(number2);
    }

    public bool IsGreaterThan(UIntNumber number)
    {
      int result = UIntService.Compare(this.CoreNumber, number.CoreNumber);
      return result == 1 ? true : false;
    }

    public static bool operator >(UIntNumber number1, UIntNumber number2)
    {
      return number1.IsGreaterThan(number2);
    }

    public bool IsGreaterThanOrEqual(UIntNumber number)
    {
      int result = UIntService.Compare(this.CoreNumber, number.CoreNumber);
      return result >= 0 ? true : false;
    }

    public static bool operator >=(UIntNumber number1, UIntNumber number2)
    {
      return number1.IsGreaterThanOrEqual(number2);
    }

    public bool IsEqual(UIntNumber number)
    {
      int result = UIntService.Compare(this.CoreNumber, number.CoreNumber);
      return result == 0 ? true : false;
    }

    public static bool operator ==(UIntNumber number1, UIntNumber number2)
    {
      return number1.IsEqual(number2);
    }

    public bool IsNotEqual(UIntNumber number)
    {
      int result = UIntService.Compare(this.CoreNumber, number.CoreNumber);
      return result != 0 ? true : false;
    }

    public static bool operator !=(UIntNumber number1, UIntNumber number2)
    {
      return number1.IsNotEqual(number2);
    }

    public static bool IsNumber(string number)
    {
      return UIntService.IsNumber(number);
    }

    public static UIntNumber Parse(string coreNumber)
    {
      return new UIntNumber(coreNumber);
    }

    public static int Compare(UIntNumber number1, UIntNumber number2)
    {
      return UIntService.Compare(number1.CoreNumber, number2.CoreNumber);
    }

    public static UIntNumber operator +(UIntNumber number)
    {
      return number;
    }

    public UIntNumber Add(UIntNumber number)
    {
      string result = UIntService.Add(this.CoreNumber, number.CoreNumber);
      return new UIntNumber(result);
    }

    public static UIntNumber operator +(UIntNumber number1, UIntNumber number2)
    {
      return number1.Add(number2);
    }

    public UIntNumber Subtract(UIntNumber number)
    {
      string result = UIntService.Subtract(this.CoreNumber, number.CoreNumber);
      return new UIntNumber(result);
    }

    public static UIntNumber operator -(UIntNumber number1, UIntNumber number2)
    {
      return number1.Add(number2);
    }

    public UIntNumber Multiple(UIntNumber number)
    {
      string result = UIntService.Multiple(this.CoreNumber, number.CoreNumber);
      return new UIntNumber(result);
    }

    public static UIntNumber operator *(UIntNumber number1, UIntNumber number2)
    {
      return number1.Multiple(number2);
    }

    public UIntNumber Divide(UIntNumber divisor)
    {
      string result = UIntService.Divide(this.CoreNumber, divisor.CoreNumber);
      return new UIntNumber(result);
    }

    public static UIntNumber operator /(UIntNumber dividend, UIntNumber divisor)
    {
      return dividend.Divide(divisor);
    }

    public UIntNumber DivideMod(UIntNumber divisor)
    {
      string result = UIntService.DivideMod(this.CoreNumber, divisor.CoreNumber);
      return new UIntNumber(result);
    }

    public static UIntNumber operator %(UIntNumber dividend, UIntNumber divisor)
    {
      return dividend.DivideMod(divisor);
    }

    public (UIntNumber, UIntNumber) RealDivide(UIntNumber divisor)
    {
      (string, string) result = UIntService.RealDivide(this.CoreNumber, divisor.CoreNumber);
      return (new UIntNumber(result.Item1), new UIntNumber(result.Item2));
    }

    public UIntNumber Multiply10(UIntNumber number)
    {
      string result = UIntService.Multiply10(this.CoreNumber, number.CoreNumber);
      return new UIntNumber(result);
    }

    public UIntNumber Pow(UIntNumber number)
    {
      string result = UIntService.Pow(this.CoreNumber, number.CoreNumber);
      return new UIntNumber(result);
    }

    public UIntNumber Fractorial()
    {
      string temp = this.CoreNumber;
      string result = "1";
      while (UIntService.Compare(temp, "1") == 1)
      {
        result = UIntService.Multiple(result, temp);
        temp = UIntService.Subtract(temp, "1");
      }
      return new UIntNumber(result);
    }

    public static UIntNumber CalculateGreatestCommonFactor(UIntNumber number1, UIntNumber number2)
    {
      string result = UIntService.CalculateGreatestCommonFactor(number1.CoreNumber, number2.CoreNumber);
      return new UIntNumber(result);
    }
  }
}
