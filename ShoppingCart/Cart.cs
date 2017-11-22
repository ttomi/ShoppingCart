using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class Cart
    {
        public List<CartItem> Products;
        public List<Coupon> Coupons;
        public List<CheckoutItem> CheckoutItems;
        private double _totalPrice;

        public Cart()
        {
            Products = new List<CartItem>();
            Coupons = new List<Coupon>();
        }

        public double TotalPrice => _totalPrice;

        public void AddProduct(CartItem cartItem)
        {
            Products.Add(cartItem);
            _totalPrice += cartItem.Price;
        }

        /// <summary>
        /// Check discount qualifications and recalculate total price.
        /// </summary>
        public void Checkout()
        {
            var tempProducts = new List<CartItem>(Products);    // not to influence the initial state of products
            CheckoutItems = new List<CheckoutItem>();
            GetCoupons();
            ApplyCoupons(tempProducts);
            tempProducts.ForEach(product => CheckoutItems.Add(new CheckoutItem(product)));
            _totalPrice = CheckoutItems.Sum(ci => ci.Price);
        }

        public void GetCoupons()
        {
            foreach (var discountsValue in Coupon.ActiveDiscounts.Values)
            {
                for (int i = 0; i < discountsValue.PrerequisiteProducts.Min(pp => pp.GetCouponCount(Products)); i++)
                {
                    Coupons.Add(discountsValue);
                }
            }
        }

        private void ApplyCoupons(List<CartItem> tempProducts)
        {
            Coupons.ForEach(coupon =>
            {
                var itemForDiscount = tempProducts.FirstOrDefault(p => coupon.ResultingPrices.Name == p.Name);
                if (itemForDiscount != (CartItem)default)
                {
                    CheckoutItems.Add(coupon.ResultingPrices);
                    tempProducts.Remove(itemForDiscount);
                }
            });
        }
    }
}