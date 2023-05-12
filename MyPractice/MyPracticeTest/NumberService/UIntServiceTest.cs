using MyNumber.Services;
using NUnit.Framework;

namespace MyPracticeText.NumberService
{
  [TestFixture]
  public class UIntServiceTest
  {
    [Test]
    [TestCase("123", true)]
    [TestCase("00123", true)]
    [TestCase("-123", false)]
    [TestCase("12.3", false)]
    [TestCase("123a", false)]
    public void IsNumberTest(string number, bool expected)
    {
      bool check = UIntService.IsNumber(number);
      if (expected) Assert.IsTrue(check);
      else Assert.IsFalse(check);
      Assert.IsTrue(true);
    }

    [Test(Author = "123", Description = "FormatNumber")]
    [TestCase("123", "123")]
    [TestCase("00123", "123")]
    [TestCase("0012300", "12300")]
    public void FormatNumberTest(string number, string expected)
    {
      string fNum = UIntService.FormatNumber(number);
      Assert.IsTrue(fNum == expected);
    }

    [Test]
    [TestCase("123", "123", 0)]
    [TestCase("123", "543", -1)]
    [TestCase("99", "123", -1)]
    [TestCase("11111", "99", 1)]
    public void CompareTest(string number1, string number2, int expected)
    {
      int check = UIntService.Compare(number1, number2);
      Assert.IsTrue(check == expected);
    }

    [Test]
    [TestCase("123", "321", "444")]
    [TestCase("1", "0", "1")]
    public void AddTest(string number1, string number2, string expected)
    {
      string result = UIntService.Add(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("10", "10", "0")]
    [TestCase("11", "10", "1")]
    public void SubtractTest(string number1, string number2, string expected)
    {
      string result = UIntService.FormatNumber(UIntService.Subtract(number1, number2));
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("15", "2", "30")]
    [TestCase("99", "3", "297")]
    [TestCase("99", "1234", "122166")]
    public void MultipleTest(string number1, string number2, string expected)
    {
      string result = UIntService.Multiple(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("15", "3", "5")]
    [TestCase("1234", "5", "246")]
    [TestCase("123456", "789", "156")]
    public void DivideTest(string number1, string number2, string expected)
    {
      string result = UIntService.Divide(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("15", "3", "0")]
    [TestCase("1234", "5", "4")]
    [TestCase("123456", "789", "372")]
    public void DivideModTest(string number1, string number2, string expected)
    {
      string result = UIntService.DivideMod(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("15", "3", "5", "0")]
    [TestCase("1234", "5", "246", "4")]
    [TestCase("123456", "789", "156", "372")]
    public void RealDivideTest(string number1, string number2, string expectedInteger, string expectedDecimal)
    {
      (string a, string b) = UIntService.RealDivide(number1, number2);
      Assert.IsTrue(a == expectedInteger && b == expectedDecimal);
    }

    [Test]
    [TestCase("15", "3", "15000")]
    [TestCase("12", "10", "120000000000")]
    public void Multiply10Test(string number1, string number2, string expected)
    {
      string result = UIntService.Multiply10(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("15", "3", "3375")]
    [TestCase("12", "10", "61917364224")]
    public void PowTest(string number1, string number2, string expected)
    {
      string result = UIntService.Pow(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("15", "5", "5")]
    [TestCase("5", "1", "1")]
    public void CalculateGreatestCommonFactorTest(string number1, string number2, string expected)
    {
      string result = UIntService.FormatNumber(UIntService.CalculateGreatestCommonFactor(number1, number2));
      Assert.IsTrue(result == expected);
    }
  }
}
