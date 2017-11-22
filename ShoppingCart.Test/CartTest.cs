using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ShoppingCart.Helpers;

namespace ShoppingCart.Test
{
    [TestClass]
    public class CartTest
    {
        private double precision = 0.001;

        [TestMethod]
        public void DoesntGetDiscount()
        {
            var cart = new Cart();
            cart.AddProduct(CartItem.Bread);
            cart.AddProduct(CartItem.Butter);
            cart.AddProduct(CartItem.Milk);
            cart.Checkout();
            Assert.AreEqual(cart.TotalPrice, cart.Products.Sum(product => product.Price), precision);
            Assert.AreEqual(cart.TotalPrice, 2.95, precision);
        }

        [TestMethod]
        public void BreadIsHalfPriceIfTwoButtersAreBought()
        {
            var cart = new Cart();
            cart.AddProduct(CartItem.Bread);
            cart.AddProduct(CartItem.Bread);
            cart.AddProduct(CartItem.Butter);
            cart.AddProduct(CartItem.Butter);
            cart.Checkout();

            var expectedPrice = 2 * CartItem.Butter.Price
                + CartItem.Bread.Price
                + CartItem.Bread.Price * 0.5;

            Assert.AreEqual(cart.TotalPrice, expectedPrice, precision);
            Assert.AreEqual(cart.TotalPrice, 3.10, precision);
        }

        [TestMethod]
        public void TwoBreadsAreHalfPriceBothIfFourButtersAreBought()
        {
            var cart = new Cart();
            cart.AddProduct(CartItem.Bread);
            cart.AddProduct(CartItem.Bread);
            cart.AddProduct(CartItem.Butter);
            cart.AddProduct(CartItem.Butter);
            cart.AddProduct(CartItem.Butter);
            cart.AddProduct(CartItem.Butter);
            cart.Checkout();

            var expectedPrice = 4 * CartItem.Butter.Price
                                + 2* CartItem.Bread.Price * 0.5;

            Assert.AreEqual(cart.TotalPrice, expectedPrice, precision);
            Assert.AreEqual(cart.TotalPrice, 4.2, precision);
        }

        [TestMethod]
        public void FourthMilkShouldBeFree()
        {
            var cart = new Cart();

            for (int i = 0; i < 4; i++)
            {
                cart.AddProduct(CartItem.Milk);
            }
            cart.Checkout();
            var expectedPrice = 3 * CartItem.Milk.Price;

            Assert.AreEqual(cart.TotalPrice, expectedPrice, precision);
            Assert.AreEqual(cart.TotalPrice, 3.45, precision);
        }

        [TestMethod]
        public void EveryFourthMilkShouldBeFree()
        {
            var cart = new Cart();

            for (int i = 0; i < 12; i++)
            {
                cart.AddProduct(CartItem.Milk);
            }
            cart.Checkout();
            var expectedPrice = 9 * CartItem.Milk.Price;

            Assert.AreNotEqual(cart.TotalPrice, 8 * CartItem.Milk.Price, precision);   // 12 / 3 = 4
            Assert.AreEqual(cart.TotalPrice, expectedPrice, precision);
            Assert.AreEqual(cart.TotalPrice, 10.35, precision);
        }

        [TestMethod]
        public void TwelweMilksGiveThreeCoupons()
        {
            var cart = new Cart();

            for (int i = 0; i < 12; i++)
            {
                cart.AddProduct(CartItem.Milk);
            }
            Assert.AreEqual(cart.Coupons.Count, 3);
        }

        [TestMethod]
        public void TwoButterOneBreadEightMilk()
        {
            var cart = new Cart();
            cart.AddProduct(CartItem.Bread);
            cart.AddProduct(CartItem.Butter);
            cart.AddProduct(CartItem.Butter);
            for (int i = 0; i < 8; i++)
            {
                cart.AddProduct(CartItem.Milk);
            }
            cart.Checkout();
            var expectedPrice = 2 * CartItem.Butter.Price
                                + 0.5 * CartItem.Bread.Price
                                + 6 * CartItem.Milk.Price;

            Assert.AreEqual(cart.TotalPrice, expectedPrice, precision);
            Assert.AreEqual(cart.TotalPrice, 9, precision);
        }
    }
}