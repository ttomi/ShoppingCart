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

        public int GetCouponCount(Cart cart)
        {
            int comparableItemsInCartCount = GetComparableItems(cart).Count();
            return comparableItemsInCartCount / RequiredCount;
        }

        private IEnumerable<CartItem> GetComparableItems(Cart cart)
        {
            return cart.Products.Where(cartItem => cartItem == Item);
        }
    }
}