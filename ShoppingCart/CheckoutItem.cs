namespace ShoppingCart
{
    public class CheckoutItem : CartItem
    {
        public double Discount { get; private set; }

        public bool IsFree => !(Discount < 1);

        public CheckoutItem(CartItem cartItem) : base(cartItem.Name, cartItem.Price)
        {
            this.Discount = 0;
        }

        public CheckoutItem(CartItem cartItem, double discount) : base(cartItem.Name, cartItem.Price)
        {
            Price = cartItem.Price * (1 - discount);
            this.Discount = discount;
        }
    }
}