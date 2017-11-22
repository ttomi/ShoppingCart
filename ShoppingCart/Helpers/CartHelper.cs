using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Helpers
{
    public static class CartHelper
    {
        /// <summary>
        /// Returns the number of coupons earned with the current purchases.
        /// Used for complex coupon prerequisites with different product types.
        /// </summary>
        /// <param name="cartItems"></param>
        /// <param name="prerequisites"></param>
        /// <returns></returns>
        public static int GetCouponCount(this IEnumerable<CartItem> cartItems, IEnumerable<Prerequisite> prerequisites)
        {
            return prerequisites.Min(cartItems.GetCouponCount);
        }

        /// <summary>
        /// Returns the number of coupons earned with the current purchases.
        /// Handles a simple single-product-type prerequisite.
        /// </summary>
        /// <param name="cartItems"></param>
        /// <param name="prerequisite"></param>
        /// <returns></returns>
        private static int GetCouponCount(this IEnumerable<CartItem> cartItems, Prerequisite prerequisite)
        {
            int comparableItemsInCartCount = cartItems.GetComparableItems(prerequisite.Item).Count();
            return comparableItemsInCartCount / prerequisite.RequiredCount;
        }

        private static IEnumerable<CartItem> GetComparableItems(this IEnumerable<CartItem> cartItems, CartItem prerequisiteItem)
        {
            return cartItems.Where(cartItem => cartItem.Equals(prerequisiteItem));
        }
    }
}