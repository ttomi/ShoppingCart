using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class Coupon
    {
        // todo maybe make this an interface and implement Discount and FreeItem coupons
        public readonly Prerequisite[] PrerequisiteProducts;

        // not an array, because for multiple affected products I'll just create a new entry with same prerequisite
        public readonly CheckoutItem ResultingPrices;

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

        public static Coupon ButterHalfPrice = new Coupon(
            new Prerequisite[] { new Prerequisite(2, CartItem.Butter) },
            new CheckoutItem(CartItem.Bread, 0.5)
            );

        public static Coupon FourthMilkFree = new Coupon(
            new Prerequisite[] { new Prerequisite(3, CartItem.Milk) },
            new CheckoutItem(CartItem.Milk, 1));

        public static Dictionary<int, Coupon> ActiveDiscounts = new Dictionary<int, Coupon>
        {
            { 1, ButterHalfPrice },
            { 2, FourthMilkFree }
        };
    }

    public class Prerequisite
    {
        public int RequiredCount;
        public CartItem Item;

        public Prerequisite(int requiredCount, CartItem item)
        {
            RequiredCount = requiredCount;
            Item = item;
        }

        public int GetCouponCount(Cart cart)
        {
            int comparableItemsInCartCount = GetComparableItems(cart).Count();
            return comparableItemsInCartCount / RequiredCount;
        }

        public bool CheckPrerequisites(Cart cart)
        {
            int comparableItemsInCartCount = GetComparableItems(cart).Count();
            return comparableItemsInCartCount >= RequiredCount;
        }

        private IEnumerable<CartItem> GetComparableItems(Cart cart)
        {
            return cart.Products.Where(cartItem => cartItem == Item);
        }
    }
}