using MyNumber.Services;

namespace MyNumber.Number
{
    public class DecimalNumber: IComparable, IComparable<DecimalNumber>, IEquatable<DecimalNumber>
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
            if (num == null) return 1;
            else return this.CompareTo(num);
        }

        public int CompareTo(DecimalNumber? other)
        {
            if (other == null) return 1;
            else return DecimalService.Compare(this.CoreNumber, other.CoreNumber);
        }

        public bool Equals(DecimalNumber? other)
        {
            if (other == null) return false;
            else return this.Equals(other);
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

        public DecimalNumber Subtract(DecimalNumber number)
        {
            string result = DecimalService.Subtract(this.CoreNumber, number.CoreNumber);
            return new DecimalNumber(result);
        }
    }
}

