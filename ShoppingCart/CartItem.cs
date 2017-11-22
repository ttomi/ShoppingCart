using System.Collections.Generic;

namespace ShoppingCart
{
    public class CartItem
    {
        public readonly string Name;
        public double Price { get; protected set; }

        // to simulate database entries
        protected CartItem(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }

        public static CartItem Bread = new CartItem("Bread", 1);
        public static CartItem Butter = new CartItem("Butter", 0.8);
        public static CartItem Milk = new CartItem("Milk", 1.15);

        // list of all products
        public static readonly Dictionary<int, CartItem> ProductDictionary = new Dictionary<int, CartItem>
        {
            { 1, Bread },
            { 2, Butter },
            { 3, Milk }
        };

        public override string ToString()
        {
            return Name + " " + Price;
        }
    }
}