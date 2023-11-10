using OrderProcessingSystem;

namespace ecommerce_cs.Tests
{
    public class InventoryTests
    {
        [Fact]
        public void AddProduct_ShouldIncreaseCount()
        {
            // Arrange
            var inventory = new Inventory();
            var product = new Product(1, "Test Product", 10.00m, 5);

            // Act
            inventory.AddProduct(product);

            // Assert
            Assert.Single(inventory.Products);
        }

        [Fact]
        public void RemoveProduct_ShouldDecreaseCount()
        {
            // Arrange
            var inventory = new Inventory();
            var product = new Product(1, "Test Product", 10.00m, 5);
            inventory.AddProduct(product);

            // Act
            inventory.RemoveProduct(1);

            // Assert
            Assert.Empty(inventory.Products);
        }

        [Fact]
        public void RemoveProduct_ThatDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var inventory = new Inventory();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => inventory.RemoveProduct(99));
            Assert.Equal("Product with the given ID does not exist.", exception.Message);
        }
    }
}
