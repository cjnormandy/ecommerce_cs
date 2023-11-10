namespace OrderProcessingSystem
{
    public class Order
    {
        public int OrderId { get; }
        public ShoppingCart ShoppingCart { get; }
        public decimal Total => ShoppingCart.CalculateTotal();

        public Order(int orderId, ShoppingCart shoppingCart)
        {
            OrderId = orderId;
            ShoppingCart = shoppingCart;
        }
    }
}
