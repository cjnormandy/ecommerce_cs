namespace OrderProcessingSystem
{
    public class UserInterface
    {
        private Inventory inventory = new();
        private ShoppingCart cart = new();
        private Order? order;

        private IDiscountStrategy? discountStrategy;
        public void Run()
        {
            // Populate inventory with some products
            inventory.AddProduct(new Product(1, "Laptop", 999.99m, 10));
            inventory.AddProduct(new Product(2, "Smartphone", 499.99m, 20));
            inventory.AddProduct(new Product(3, "Tablet", 299.99m, 15));

            bool running = true;

            while (running)
            {
                Console.WriteLine("Welcome to the E-commerce Order Processing System!");
                Console.WriteLine("1. Show Inventory");
                Console.WriteLine("2. Add Product to Cart");
                Console.WriteLine("3. Remove Product from Cart");
                Console.WriteLine("4. Show Cart");
                Console.WriteLine("5. Apply Discount");
                Console.WriteLine("6. Checkout");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ShowInventory();
                        break;
                    case 2:
                        AddProductToCart();
                        break;
                    case 3:
                        RemoveProductFromCart();
                        break;
                    case 4:
                        ShowCart();
                        break;
                    case 5:
                        ApplyDiscount();
                        break;
                    case 6:
                        Checkout();
                        break;
                    case 7:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }

            void ShowInventory()
            {
                Console.WriteLine("Inventory:");
                foreach (var product in inventory.Products)
                {
                    Console.WriteLine($"ID: {product.Id}, Description: {product.Description}, Price: {product.Price:C}, Stock: {product.StockQuantity}");
                }
            }

            void AddProductToCart()
            {
                Console.Write("Enter the ID of the product you want to add to the cart: ");
                int id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    var product = inventory.CheckStock(id);
                    cart.AddProduct(product);
                    Console.WriteLine("Product added to cart.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            void RemoveProductFromCart()
            {
                Console.Write("Enter the ID of the product you want to remove from the cart: ");
                int id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    var product = inventory.CheckStock(id);
                    cart.RemoveProduct(product);
                    Console.WriteLine("Product removed from cart.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            void ShowCart()
            {
                Console.WriteLine("Cart:");
                foreach (var item in cart.Items)
                {
                    Console.WriteLine($"ID: {item.Id}, Description: {item.Description}, Price: {item.Price:C}");
                }
                Console.WriteLine($"Total: {cart.CalculateTotal():C}");
            }

            void ApplyDiscount()
            {
                Console.WriteLine("Select a discount to apply:");
                Console.WriteLine("1. 10% off your purchase");
                Console.WriteLine("2. Buy One Get One Free");
                int discountType = Convert.ToInt32(Console.ReadLine());
                
                switch (discountType)
                {
                    case 1:
                        discountStrategy = new PercentageDiscountStrategy(10);
                        break;
                    case 2:
                        discountStrategy = new BOGOFreeDiscountStrategy();
                        break;
                    default:
                        Console.WriteLine("Invalid discount type.");
                        return;
                }

                Console.WriteLine("Discount applied.");
            }

            void Checkout()
            {
                if (cart.Items.Any())
                {
                    order = new Order(1, cart);
                    if (discountStrategy != null)
                    {
                        Console.WriteLine($"Total before discount: {order.Total:C}");
                        Console.WriteLine($"Total after discount: {discountStrategy.ApplyDiscount(order):C}");
                    }
                    else
                    {
                        Console.WriteLine($"Total: {order.Total:C}");
                    }
                    Console.WriteLine("Order completed. Thank you for your purchase!");
                    
                    // Clear the cart for the next order
                    cart = new ShoppingCart();
                    discountStrategy = null; // Reset discount strategy
                }
                else
                {
                    Console.WriteLine("Your cart is empty.");
                }
            }
        }
    }
}
