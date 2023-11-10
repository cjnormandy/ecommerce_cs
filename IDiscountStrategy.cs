namespace OrderProcessingSystem
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(Order order);
    }

    // Concrete Discount Strategies
    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _percentage;

        public PercentageDiscountStrategy(decimal percentage)
        {
            _percentage = percentage;
        }

        public decimal ApplyDiscount(Order order)
        {
            return order.Total - (order.Total * _percentage / 100);
        }
    }

    public class BOGOFreeDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(Order order)
        {
            // Assuming every second product is free
            var products = new List<Product>(order.ShoppingCart.Items);
            decimal discount = 0m;
            for (int i = 0; i < products.Count; i += 2)
            {
                discount += products[i].Price;
            }
            return order.Total - discount;
        }
    }
}