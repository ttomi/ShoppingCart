using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class Prerequisite
    {
        public int RequiredCount;
        public readonly CartItem Item;

        public Prerequisite(int requiredCount, CartItem item)
        {
            RequiredCount = requiredCount;
            Item = item;
        }

        /// <summary>
        /// Returns how many times is the single prerequisite satisfied.
        /// </summary>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        public int GetCouponCount(List<CartItem> cartItems)
        {
            int comparableItemsInCartCount = GetComparableItems(cartItems).Count();
            return comparableItemsInCartCount / RequiredCount;
        }

        private IEnumerable<CartItem> GetComparableItems(List<CartItem> cartItems)
        {
            return cartItems.Where(cartItem => cartItem == Item);
        }
    }
}