namespace OrderProcessingSystem
{
    public class Inventory
    {
        private readonly Dictionary<int, Product> _products = new Dictionary<int, Product>();
        // Public read-only property to expose the products
        public IEnumerable<Product> Products => _products.Values;
        public void AddProduct(Product product)
        {
            if (_products.ContainsKey(product.Id))
                throw new InvalidOperationException("Product already exists.");
            _products.Add(product.Id, product);
        }

        public void RemoveProduct(int productId)
        {
            if (!_products.ContainsKey(productId))
                throw new InvalidOperationException("Product does not exist.");
            _products.Remove(productId);
        }

        public Product CheckStock(int productId)
        {
            if (_products.TryGetValue(productId, out var product))
                return product;
            throw new InvalidOperationException("Product does not exist.");
        }

        public void UpdateStock(int productId, int quantity)
        {
            if (_products.TryGetValue(productId, out var product))
            {
                product.UpdateStock(quantity);
            }
            else
            {
                throw new InvalidOperationException("Product does not exist.");
            }
        }
    }
}
