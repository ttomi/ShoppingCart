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
    }
}