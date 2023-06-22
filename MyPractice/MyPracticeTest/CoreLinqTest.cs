using MyLibrary.CustomLinq;
using NUnit.Framework;

namespace MyPracticeTest
{
  [TestFixture]
  public class CoreLinqTest
  {
    private List<int>? intList;
    private List<string>? stringList;

    [SetUp]
    public void Setup()
    {
      intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
      stringList = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
    }

    [Test, Order(0)]
    public void SwapTest()
    {
      bool isIntSwap = intList.SWAP(0, 1);
      Assert.IsTrue(isIntSwap);

      bool isStrSwap = stringList.SWAP(0, 1);
      Assert.IsTrue(isStrSwap);
    }
  }
}
