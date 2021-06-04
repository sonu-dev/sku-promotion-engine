using PromotionEngine.Cart;
using PromotionEngine.Sku;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Client
{
  public static class ConsoleHelper
    {
        public static void DisplayProducts(List<Product> products)
        {
            Console.WriteLine("Products...");
            Console.WriteLine($"Name_____Price");
            products.ForEach(p =>
            {
                Console.WriteLine($"{p.Name}_____{p.UnitPrice}");
                Console.WriteLine($" ");
                Console.WriteLine($" ");
            });
        }

        public static void DisplayCart(CartManager cart, string title)
        {           
            Console.WriteLine($" ");
            Console.WriteLine(title);
            Console.WriteLine("Unit|Product|Price");
            cart.CartItems.ForEach(item =>
            {
                
                Console.WriteLine($"-----------------------");
                Console.WriteLine($"{item.Quantity} | {item.Product.Name} | {item.Price}");
                Console.WriteLine($" ");
            });           
            Console.WriteLine($"Total Price:{cart.TotalPrice}");
            Console.WriteLine($" ");
        }
    }
}
