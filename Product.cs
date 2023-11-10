namespace OrderProcessingSystem
{
    public class Product
    {
        public int Id { get; }
        public string Description { get; }
        public decimal Price { get; }
        public int StockQuantity { get; private set; }

        public Product(int id, string description, decimal price, int stockQuantity)
        {
            Id = id;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public void UpdateStock(int quantity)
        {
            if (StockQuantity + quantity < 0)
                throw new InvalidOperationException("Insufficient stock.");
            StockQuantity += quantity;
        }
    }
}
