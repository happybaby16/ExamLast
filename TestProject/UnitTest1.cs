using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void DiscountZeroTest()
        {
            double answer = DiscountDLL.Discount.Get(0, 0);
            Assert.AreEqual(0, answer);
        }

        [TestMethod]
        public void DiscountCountTest()
        {
            double answer = DiscountDLL.Discount.Get(3,400);
            Assert.AreEqual(0.05, answer);
        }

        [TestMethod]
        public void DiscountPriceTest()
        {
            double answer = DiscountDLL.Discount.Get(1, 900);
            Assert.AreEqual(0.01, answer);
        }

        [TestMethod]
        public void DiscountCountAndPriceTest()
        {
            double answer = DiscountDLL.Discount.Get(3, 900);
            Assert.AreEqual(0.06, answer);
        }
    }
}
