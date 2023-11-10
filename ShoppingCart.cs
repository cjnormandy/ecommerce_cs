namespace OrderProcessingSystem
{
    public class ShoppingCart
    {
        private readonly List<Product> _items = new List<Product>();

        public IEnumerable<Product> Items => _items.AsReadOnly();

        public void AddProduct(Product product)
        {
            if (product.StockQuantity <= 0)
                throw new InvalidOperationException("Product is out of stock.");
            _items.Add(product);
            product.UpdateStock(-1); // Decrement stock
        }

        public void RemoveProduct(Product product)
        {
            if (!_items.Remove(product))
                throw new InvalidOperationException("Product is not in the cart.");
            product.UpdateStock(1); // Increment stock
        }

        public decimal CalculateTotal()
        {
            decimal total = 0m;
            foreach (var item in _items)
            {
                total += item.Price;
            }
            return total;
        }
    }
}
