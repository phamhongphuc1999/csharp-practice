using MyNumber.Services;

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

        public UIntNumber(string coreNumber)
        {
            this.CoreNumber = coreNumber;
        }

        public int CompareTo(object? obj)
        {
            UIntNumber? num = obj as UIntNumber;
            if (num == null) return 1;
            else return this.CompareTo(num);
        }

        public bool Equals(UIntNumber? other)
        {
            if (other == null) return false;
            else return this.Equals(other);
        }

        public int CompareTo(UIntNumber? other)
        {
            if (other == null) return 1;
            else return UIntService.Compare(this.CoreNumber, other.CoreNumber);
        }

        public override bool Equals(object? obj)
        {
            UIntNumber? num = obj as UIntNumber;
            if (num == null) return true;
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

        public bool IsGresterThan(UIntNumber number)
        {
            int result = UIntService.Compare(this.CoreNumber, number.CoreNumber);
            return result == 1 ? true : false;
        }

        public static bool operator >(UIntNumber number1, UIntNumber number2)
        {
            return number1.IsGresterThan(number2);
        }

        public bool IsGresterThanOrEqual(UIntNumber number)
        {
            int result = UIntService.Compare(this.CoreNumber, number.CoreNumber);
            return result >= 0 ? true : false;
        }

        public static bool operator >=(UIntNumber number1, UIntNumber number2)
        {
            return number1.IsGresterThanOrEqual(number2);
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
    }
}

