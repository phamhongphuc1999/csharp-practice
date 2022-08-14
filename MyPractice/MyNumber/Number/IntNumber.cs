using MyNumber.Services;
 
namespace MyNumber.Number
{
    public class IntNumber: IComparable, IComparable<IntNumber>, IEquatable<IntNumber>
    {
        protected string coreNumber = "";

        public string CoreNumber
        {
            get { return coreNumber; }
            set
            {
                coreNumber = IntService.FormatNumber(value);
            }
        }

        public IntNumber(string number)
        {
            this.CoreNumber = IntService.FormatNumber(number);
        }

        public int CompareTo(object? obj)
        {
            IntNumber? num = obj as IntNumber;
            if (num is null) return 1;
            else return this.CompareTo(num);
        }

        public bool Equals(IntNumber? other)
        {
            if (other is null) return false;
            else return this.Equals(other);
        }

        public int CompareTo(IntNumber? other)
        {
            if (other is null) return 1;
            else return IntService.Compare(this.CoreNumber, other.CoreNumber);
        }

        public override bool Equals(object? obj)
        {
            IntNumber? num = obj as IntNumber;
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

        public bool IsLessThan(IntNumber number)
        {
            int result = IntService.Compare(this.CoreNumber, number.CoreNumber);
            return result == -1 ? true : false;
        }

        public static bool operator <(IntNumber number1, IntNumber number2)
        {
            return number1.IsLessThan(number2);
        }

        public bool IsLessThanOrEqual(IntNumber number)
        {
            int result = IntService.Compare(this.CoreNumber, number.CoreNumber);
            return result <= 0 ? true : false;
        }

        public static bool operator <=(IntNumber number1, IntNumber number2)
        {
            return number1.IsLessThanOrEqual(number2);
        }

        public bool IsGreaterThan(IntNumber number)
        {
            int result = IntService.Compare(this.CoreNumber, number.CoreNumber);
            return result == 1 ? true : false;
        }

        public static bool operator >(IntNumber number1, IntNumber number2)
        {
            return number1.IsGreaterThan(number2);
        }

        public bool IsGreaterThanOrEqual(IntNumber number)
        {
            int result = IntService.Compare(this.CoreNumber, number.CoreNumber);
            return result >= 1 ? true : false;
        }

        public static bool operator >=(IntNumber number1, IntNumber number2)
        {
            return number1.IsGreaterThanOrEqual(number2);
        }

        public bool IsEqual(IntNumber number)
        {
            int result = IntService.Compare(this.CoreNumber, number.CoreNumber);
            return result == 0 ? true : false;
        }

        public static bool operator ==(IntNumber number1, IntNumber number2)
        {
            return number1.IsEqual(number2);
        }

        public bool IsNotEqual(IntNumber number)
        {
            int result = IntService.Compare(this.CoreNumber, number.CoreNumber);
            return result != 0 ? true : false;
        }

        public static bool operator !=(IntNumber number1, IntNumber number2)
        {
            return number1.IsNotEqual(number2);
        }

        public static bool IsNumber(string number)
        {
            return IntService.IsNumber(number);
        }

        public static IntNumber Parse(string coreNumber)
        {
            return new IntNumber(coreNumber);
        }

        public static int Compare(IntNumber number1, IntNumber number2)
        {
            return IntService.Compare(number1.CoreNumber, number2.CoreNumber);
        }

        public static IntNumber operator +(IntNumber number)
        {
            return number;
        }

        public static IntNumber operator -(IntNumber number)
        {
            string sNum = number.CoreNumber;
            if (sNum[0] == '-') return new IntNumber(sNum.Substring(1));
            else return new IntNumber($"-${sNum}");
        }

        public UIntNumber Abs()
        {
            string sNum = this.CoreNumber;
            if (sNum[0] == '-') return new UIntNumber(sNum.Substring(1));
            else return new UIntNumber(sNum);
        }

        public (int, UIntNumber) GetUInt()
        {
            (int sign, string uIntNum) = IntService.GetUIntNumber(this.CoreNumber);
            return (sign, new UIntNumber(uIntNum));
        }

        public IntNumber Add(IntNumber number)
        {
            string result = IntService.Add(this.CoreNumber, number.CoreNumber);
            return new IntNumber(result);
        }

        public static IntNumber operator +(IntNumber number1, IntNumber number2)
        {
            return number1.Add(number2);
        }

        public static IntNumber operator +(UIntNumber number1, IntNumber number2)
        {
            string result = IntService.Add(number1.CoreNumber, number2.CoreNumber);
            return new IntNumber(result);
        }

        public static IntNumber operator +(IntNumber number1, UIntNumber number2)
        {
            string result = IntService.Add(number1.CoreNumber, number2.CoreNumber);
            return new IntNumber(result);
        }

        public IntNumber Subtract(IntNumber number)
        {
            string result = IntService.Subtract(this.CoreNumber, number.CoreNumber);
            return new IntNumber(result);
        }

        public static IntNumber operator -(IntNumber number1, IntNumber number2)
        {
            return number1.Subtract(number2);
        }

        public static IntNumber operator -(UIntNumber number1, IntNumber number2)
        {
            string result = IntService.Subtract(number1.CoreNumber, number2.CoreNumber);
            return new IntNumber(result);
        }

        public static IntNumber operator -(IntNumber number1, UIntNumber number2)
        {
            string result = IntService.Subtract(number1.CoreNumber, number2.CoreNumber);
            return new IntNumber(result);
        }

        public IntNumber Multiple(IntNumber number)
        {
            string result = IntService.Multiple(this.CoreNumber, number.CoreNumber);
            return new IntNumber(result);
        }

        public static IntNumber operator *(IntNumber number1, IntNumber number2)
        {
            return number1.Multiple(number2);
        }

        public static IntNumber operator *(UIntNumber number1, IntNumber number2)
        {
            string result = IntService.Multiple(number1.CoreNumber, number2.CoreNumber);
            return new IntNumber(result);
        }

        public static IntNumber operator *(IntNumber number1, UIntNumber number2)
        {
            string result = IntService.Multiple(number1.CoreNumber, number2.CoreNumber);
            return new IntNumber(result);
        }

        public IntNumber Divide(IntNumber number)
        {
            string result = IntService.Divide(this.CoreNumber, number.CoreNumber);
            return new IntNumber(result);
        }

        public static IntNumber operator /(IntNumber number1, IntNumber number2)
        {
            return number1.Divide(number2);
        }

        public static IntNumber operator /(UIntNumber number1, IntNumber number2)
        {
            string result = IntService.Divide(number1.CoreNumber, number2.CoreNumber);
            return new IntNumber(result);
        }

        public static IntNumber operator /(IntNumber number1, UIntNumber number2)
        {
            string result = IntService.Divide(number1.CoreNumber, number2.CoreNumber);
            return new IntNumber(result);
        }

        public IntNumber Multiple10(UIntNumber number)
        {
            string result = IntService.Multiple(this.CoreNumber, number.CoreNumber);
            return new IntNumber(result);
        }

        public IntNumber Pow(UIntNumber number)
        {
            string result = IntService.Pow(this.CoreNumber, number.CoreNumber);
            return new IntNumber(result);
        }
    }
}

