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
    }
}

