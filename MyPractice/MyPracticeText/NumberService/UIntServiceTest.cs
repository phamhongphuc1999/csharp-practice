using MyNumber.Services;
using NUnit.Framework;

namespace MyPracticeText.NumberService
{
    [TestFixture]
    public class UIntServiceTest
    {
        [TestCase("123", true)]
        [TestCase("00123", true)]
        [TestCase("-123", false)]
        [TestCase("12.3", false)]
        [TestCase("123a", false)]
        public void IsNumberTest(string number, bool check)
        {
            bool _check = UIntService.IsNumber(number);
            if (check) Assert.IsTrue(_check);
            else Assert.IsFalse(_check);
        }
    }
}

