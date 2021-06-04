using PromotionEngine.Cart;
using PromotionEngine.Promotions;
using PromotionEngine.Sku;
using System;
using System.Collections.Generic;

namespace PromotionEngine.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sku's Ids/Names
            const string A = "A";
            const string B = "B";
            const string C = "C";
            const string D = "D";
            const string E = "E";

            // Create Skus 
            var sku_A = new Product(A, 50);
            var sku_B = new Product(B, 30);
            var sku_C = new Product(C, 20);
            var sku_D = new Product(D, 15);
            var sku_E = new Product(E, 100);
            ConsoleHelper.DisplayProducts(new List<Product> { sku_A, sku_B, sku_C, sku_D, sku_E });

            // Create Promotion Rules
            var promotionRulesManager = new PromotionRulesManager();

            // Add FixedPrice Promotion Rules

            // 3 of A's for 130           
            promotionRulesManager.AddPromotionRule(new FixedPricePromotionRule("3A", new List<PromotionProduct> { new PromotionProduct(A, 3) }, 130));

            // 2 of B's for 45           
            promotionRulesManager.AddPromotionRule(new FixedPricePromotionRule("2B", new List<PromotionProduct> { new PromotionProduct(B, 2) }, 45));

            // C & D for 30
            promotionRulesManager.AddPromotionRule(new FixedPricePromotionRule("CD", new List<PromotionProduct> { new PromotionProduct(C, 1), new PromotionProduct(D, 1) }, 30));

            // E for 10%
            promotionRulesManager.AddPromotionRule(new PercentagePricePromotionRule("E%100", new List<PromotionProduct> { new PromotionProduct(E, 2) }, 10));

            /* Scenario A
                        1 * A 50
                        1 * B 30
                        1 * C 20
             */
            // Add Items to Cart
            var cartMgr = new CartManager(promotionRulesManager);
            cartMgr.AddToCart(sku_A, 1);
            cartMgr.AddToCart(sku_B, 1);
            cartMgr.AddToCart(sku_C, 1);
            cartMgr.ApplyPromotions();
            ConsoleHelper.DisplayCart(cartMgr, "Scenario A");

            /* Scenario B
                        5 * A 130 + 2*50
                        5 * B 45 + 45 + 30
                        1 * C 20
             */

            cartMgr.ClearCart();
            cartMgr.AddToCart(sku_A, 5);
            cartMgr.AddToCart(sku_B, 5);
            cartMgr.AddToCart(sku_C, 1);
            cartMgr.ApplyPromotions();
            ConsoleHelper.DisplayCart(cartMgr, "Scenario B");

            /*Scenario C
                        3 * A 130
                        5 * B 45 + 45 + 1 * 30
                        1 * C -
                        1 * D 3
             */
            cartMgr.ClearCart();          
            cartMgr.AddToCart(sku_A, 3);
            cartMgr.AddToCart(sku_B, 5);
            cartMgr.AddToCart(sku_C, 1);
            cartMgr.AddToCart(sku_D, 1);
            cartMgr.ApplyPromotions();           
            ConsoleHelper.DisplayCart(cartMgr, "Scenario C");

            /* Scenario D (Custom)
                        3 * A 130
                        5 * B 120
                        1 * D 15
                        7 * E 640
             */
            cartMgr.ClearCart();
            cartMgr.AddToCart(sku_A, 3);
            cartMgr.AddToCart(sku_B, 5);
            cartMgr.AddToCart(sku_D, 1);         
            cartMgr.AddToCart(sku_E, 7);
            cartMgr.ApplyPromotions();
            ConsoleHelper.DisplayCart(cartMgr, "Scenario D(Custom)");

            Console.ReadKey();
        }
    }
}
