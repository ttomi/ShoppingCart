using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Helpers;

namespace ShoppingCart
{
    public class Cart
    {
        public List<CartItem> Products;
        public List<Coupon> Coupons => GetCoupons(Products);   // not sure if I should remove it or leave it as is to have instant access while debugging

        public List<CheckoutItem> CheckoutItems;
        private double _totalPrice;

        public Cart()
        {
            Products = new List<CartItem>();
        }

        public double TotalPrice => _totalPrice;

        public void AddProduct(CartItem cartItem)
        {
            Products.Add(cartItem);
            _totalPrice += cartItem.Price;
        }

        public void RemoveProduct(CartItem cartItem)
        {
            if (!Products.Any() && !Products.Contains(cartItem))
                return;

            Products.Remove(cartItem);
            _totalPrice -= cartItem.Price;

            // if we have already checked-out we should recheck the price and discounts
            if (CheckoutItems.Any())
            {
                Checkout();
            }
        }

        /// <summary>
        /// Check discount qualifications and recalculate total price.
        /// </summary>
        public void Checkout()
        {
            if (!Products.Any())
                return;

            var tempProducts = new List<CartItem>(Products);    // to not influence the initial state of products
            CheckoutItems = new List<CheckoutItem>();
            var earnedCoupons = GetCoupons(Products);
            ApplyCoupons(tempProducts, earnedCoupons);
            tempProducts.ForEach(product => CheckoutItems.Add(new CheckoutItem(product)));
            _totalPrice = CheckoutItems.Sum(ci => ci.Price);
        }

        /// <summary>
        /// Passes through the list of active discounts and adds earned coupons
        /// </summary>
        public List<Coupon> GetCoupons(List<CartItem> products)
        {
            var coupons = new List<Coupon>();
            foreach (var discountCoupon in Coupon.ActiveDiscounts.Values)
            {
                for (int i = 0; i < products.GetCouponCount(discountCoupon.PrerequisiteProducts); i++)
                {
                    coupons.Add(discountCoupon);
                }
            }
            return coupons;
        }

        private void ApplyCoupons(List<CartItem> tempProducts, List<Coupon> coupons)
        {
            coupons.ForEach(coupon =>
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