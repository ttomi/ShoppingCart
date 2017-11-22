namespace ShoppingCart
{
    public class CheckoutItem : CartItem
    {
        // todo override the Price with discounted Price

        public double Discount { get; set; }

        public bool IsFree => !(Discount < 1);

        public CheckoutItem(string name, double price, double discount) : base(name, price)
        {
            this.Discount = discount;
        }

        public CheckoutItem(CartItem cartItem, double discount) : base(cartItem.Name, cartItem.Price)
        {
            Price = cartItem.Price * (1 - discount);
            this.Discount = discount;
        }
    }
}