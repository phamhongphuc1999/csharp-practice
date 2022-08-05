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
            bool _check = UIntService.IsNumber(number);
            if (expected) Assert.IsTrue(_check);
            else Assert.IsFalse(_check);
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
    }
}

