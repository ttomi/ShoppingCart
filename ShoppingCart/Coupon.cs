using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class Coupon
    {
        // todo maybe make this an interface and implement Discount and FreeItem coupons
        public readonly Prerequisite[] PrerequisiteProducts;
        public readonly CheckoutItem ResultingPrices;   // not an array, because for multiple affected products I'll just create a new entry with same prerequisite

        public Coupon(Prerequisite[] prerequisite, CheckoutItem discountedItem)
        {
            this.PrerequisiteProducts = prerequisite;
            this.ResultingPrices = discountedItem;

            // todo maybe create a separate class for BuyNGetOneFree coupons
            if (discountedItem.IsFree)
            {
                var sameTypeItem = prerequisite.FirstOrDefault(p => p.Item.Name == discountedItem.Name);
                if (sameTypeItem != null) sameTypeItem.RequiredCount += 1;
            }
        }

        #region CouponEntries

        private static Coupon ButterHalfPrice = new Coupon(
            new Prerequisite[] { new Prerequisite(2, CartItem.Butter) },
            new CheckoutItem(CartItem.Bread, 0.5)
        );

        private static Coupon FourthMilkFree = new Coupon(
            new Prerequisite[] { new Prerequisite(3, CartItem.Milk) },
            new CheckoutItem(CartItem.Milk, 1));

        public static Dictionary<int, Coupon> ActiveDiscounts = new Dictionary<int, Coupon>
        {
            { 1, ButterHalfPrice },
            { 2, FourthMilkFree }
        };

        #endregion
    }
}