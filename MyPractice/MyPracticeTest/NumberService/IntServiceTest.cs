using MyNumber.Services;
using NUnit.Framework;

namespace MyPracticeTest.NumberService
{
  [TestFixture]
  public class IntServiceTest
  {
    [Test]
    [TestCase("123", true)]
    [TestCase("-00123", true)]
    [TestCase("-123", true)]
    [TestCase("12.3", false)]
    [TestCase("123a", false)]
    public void IsNumberTest(string number, bool expected)
    {
      bool _check = IntService.IsNumber(number);
      if (expected) Assert.IsTrue(_check);
      else Assert.IsFalse(_check);
      Assert.IsTrue(true);
    }

    [Test]
    [TestCase("123", 1, "123")]
    [TestCase("-123", -1, "123")]
    [TestCase("-00123", -1, "00123")]
    public void GetUIntNumberTest(string number, int expectedSign, string expectedInteger)
    {
      (int sign, string num) = IntService.GetUIntNumber(number);
      Assert.IsTrue(sign == expectedSign && num == expectedInteger);
    }

    [Test]
    [TestCase("123", "123")]
    [TestCase("-00123", "-123")]
    [TestCase("-0", "0")]
    public void FormatNumberTest(string number, string expected)
    {
      string fNum = IntService.FormatNumber(number);
      Assert.IsTrue(fNum == expected);
    }

    [Test]
    [TestCase("123", 1, "123")]
    [TestCase("-123", -1, "123")]
    [TestCase("-00123", -1, "123")]
    public void DeepIntNumberTest(string number, int expectedSign, string expectedInteger)
    {
      (int sign, string num) = IntService.DeepIntNumber(number);
      Assert.IsTrue(sign == expectedSign && num == expectedInteger);
    }

    [Test]
    [TestCase("10", "-10", 1)]
    [TestCase("523", "529", -1)]
    [TestCase("-483", "-483", 0)]
    public void CompareTest(string number1, string number2, int expected)
    {
      int result = IntService.Compare(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("12", "-10", "2")]
    [TestCase("12456", "-47859878", "-47847422")]
    [TestCase("0", "1", "1")]
    public void AddTest(string number1, string number2, string expected)
    {
      string result = IntService.FormatNumber(IntService.Add(number1, number2));
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("12", "-10", "22")]
    [TestCase("12456", "-478", "12934")]
    [TestCase("0", "1", "-1")]
    public void SubtractTest(string number1, string number2, string expected)
    {
      string result = IntService.FormatNumber(IntService.Subtract(number1, number2));
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("15", "-2", "-30")]
    [TestCase("-99", "-3", "297")]
    [TestCase("-99", "1234", "-122166")]
    public void MultipleTest(string number1, string number2, string expected)
    {
      string result = IntService.Multiple(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("15", "3", "5")]
    [TestCase("1234", "5", "246")]
    [TestCase("123456", "789", "156")]
    public void DivideTest(string number1, string number2, string expected)
    {
      string result = IntService.Divide(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("-15", "3", "-15000")]
    [TestCase("12", "10", "120000000000")]
    public void Multiply10Test(string number1, string number2, string expected)
    {
      string result = IntService.Multiple10(number1, number2);
      Assert.IsTrue(result == expected);
    }

    [Test]
    [TestCase("-15", "3", "-3375")]
    [TestCase("-12", "10", "61917364224")]
    public void PowTest(string number1, string number2, string expected)
    {
      string result = IntService.Pow(number1, number2);
      Assert.IsTrue(result == expected);
    }
  }
}

