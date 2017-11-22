using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Coupons  = new List<Coupon>();
        }

        public double TotalPrice => _totalPrice;

        public void AddProduct(CartItem cartItem)
        {
            Products.Add(cartItem);
            _totalPrice += cartItem.Price;
        }

        public void GetCoupons()
        {
            foreach (var discountsValue in Coupon.ActiveDiscounts.Values)
            {
                // redundant
                if (!discountsValue.PrerequisiteProducts.All(pp => pp.CheckPrerequisites(this)))
                    continue;

                for (int i = 0; i < discountsValue.PrerequisiteProducts.Min(pp => pp.GetCouponCount(this)); i++)
                {
                    Coupons.Add(discountsValue);
                }
            }
        }

        public void Checkout()
        {
            var tempProducts = new List<CartItem>(Products);
            CheckoutItems = new List<CheckoutItem>();
            GetCoupons();
            ApplyCoupons(tempProducts);
            tempProducts.ForEach(product=> CheckoutItems.Add(new CheckoutItem(product, 0)));
            _totalPrice = CheckoutItems.Sum(ci => ci.Price);
        }

        private void ApplyCoupons(List<CartItem> tempProducts)
        {
            Coupons.ForEach(coupon =>
            {
                var itemForDiscount = tempProducts.FirstOrDefault(p => coupon.ResultingPrices.Name == p.Name);
                if (itemForDiscount != (CartItem) default)
                {
                    CheckoutItems.Add(coupon.ResultingPrices);
                    tempProducts.Remove(itemForDiscount);
                }
            });
        }
    }
}
