using MyNumber.Services;
using NUnit.Framework;

namespace MyPracticeTest.NumberService
{
  [TestFixture]
  public class DecimalServiceTest
  {
    [Test]
    [TestCase("1.0001", 1, "1", "0001")]
    [TestCase("-1.0001", -1, "1", "0001")]
    [TestCase("1.123000", 1, "1", "123000")]
    [TestCase("-01.12300", -1, "01", "12300")]
    public void GetIntegerAndDecimalTest(string number, int expectedSign, string integer, string expectedDecimal)
    {
      (int, string, string) data = DecimalService.GetIntegerAndDecimal(number);
      Assert.IsTrue(data.Item1 == expectedSign && data.Item2 == integer && data.Item3 == expectedDecimal);
    }

    [Test]
    [TestCase("1.0001", "1.0001")]
    [TestCase("2.00010000", "2.0001")]
    [TestCase("-001.000100", "-1.0001")]
    public void FormatNumberTest(string number, string formatNumber)
    {
      string num = DecimalService.FormatNumber(number);
      Assert.IsTrue(num == formatNumber);
    }

    [Test]
    [TestCase("1.0001", 1, "1", "0001")]
    [TestCase("001.000100", 1, "1", "0001")]
    [TestCase("-001.00100", -1, "1", "001")]
    public void DeepGetIntegerAndDecimalTest(string number, int expectedSign, string integer, string expectedDecimal)
    {
      (int, string, string) data = DecimalService.DeepGetIntegerAndDecimal(number);
      Assert.IsTrue(data.Item1 == expectedSign && data.Item2 == integer && data.Item3 == expectedDecimal);
    }
  }
}
