using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ShoppingCart.Test
{
    [TestClass]
    public class CouponTest
    {
        [TestMethod]
        public void SimpleDiscount()
        {
            var breadHalfPrice = new Coupon(
                new Prerequisite[] { new Prerequisite(2, CartItem.Butter) },
                new CheckoutItem(CartItem.Bread, 0.5)
            );

            Assert.AreEqual(breadHalfPrice.PrerequisiteProducts.First().RequiredCount, 2);
        }

        [TestMethod]
        public void BuyNProductsGetOneFree()
        {
            var fourthMilkFree = new Coupon(
                new Prerequisite[] { new Prerequisite(3, CartItem.Milk) },
                new CheckoutItem(CartItem.Milk, 1));

            Assert.AreEqual(fourthMilkFree.PrerequisiteProducts.First().RequiredCount, 4);
        }
    }
}