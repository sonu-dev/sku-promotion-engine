using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.Cart;
using PromotionEngine.Promotions;
using PromotionEngine.Sku;
using System.Collections.Generic;

namespace PromotionEngine.Test
{
    [TestClass]
    public class CartManagerTest
    {
        private readonly PromotionRulesManager _promotionRulesManager;
        private readonly CartManager _cartManager;
        private readonly Product sku_A;
        private readonly Product sku_B;
        private readonly Product sku_C;
        private readonly Product sku_D;
        private readonly Product sku_E;

        public CartManagerTest()
        {
            _promotionRulesManager = new PromotionRulesManager();
            _cartManager = new CartManager(_promotionRulesManager);

            // Create SKU's/Products
            sku_A = new Product("A", 50);
            sku_B = new Product("B", 30);
            sku_C = new Product("C", 20);
            sku_D = new Product("D", 15);
            sku_E = new Product("E", 100);

            // Create Promotion Rules
            // Add FixedPrice Promotion Rules
            // 3 of A's for 130           
            _promotionRulesManager.AddPromotionRule(new FixedPricePromotionRule("3A", new List<PromotionProduct> { new PromotionProduct("A", 3) }, 130));
            // 2 of B's for 45           
            _promotionRulesManager.AddPromotionRule(new FixedPricePromotionRule("2B", new List<PromotionProduct> { new PromotionProduct("B", 2) }, 45));
            // C & D for 30
            _promotionRulesManager.AddPromotionRule(new FixedPricePromotionRule("CD", new List<PromotionProduct> { new PromotionProduct("C", 1), new PromotionProduct("D", 1) }, 30));
            // E for 10%
            _promotionRulesManager.AddPromotionRule(new PercentagePricePromotionRule("E%100", new List<PromotionProduct> { new PromotionProduct("E", 2) }, 10));
        }
        /// <summary>
        /// Scenario A
        ///1 * A 50
        ///1 * B 30
        ///1 * C 20
        ///Total 100
        /// </summary>
        [TestMethod]
        public void TestScenario_A()
        {
            _cartManager.ClearCart();
            _cartManager.AddToCart(sku_A, 1);
            _cartManager.AddToCart(sku_B, 1);
            _cartManager.AddToCart(sku_C, 1);

            // _cartManager.ApplyPromotions();
            var totalPrice = _cartManager.TotalPrice;
            Assert.AreEqual(100, totalPrice);
        }

        /// <summary>
        /// Scenario B
        ///5 * A 130 + 2*50
        ///5 * B 45 + 45 + 30
        ///1 * C 28
        ///Total 37
        /// </summary>
        [TestMethod]
        public void TestScenario_B()
        {
            _cartManager.ClearCart();           
            _cartManager.AddToCart(sku_A, 5);
            _cartManager.AddToCart(sku_B, 5);
            _cartManager.AddToCart(sku_C, 1);

            _cartManager.ApplyPromotions();
            var totalPrice = _cartManager.TotalPrice;
            Assert.AreEqual(370, totalPrice);
        }

        /// <summary>       
        /// Scenario C
        ///3 * A 130
        ///5 * B 45 + 45 + 1 * 30
        ///1 * C -
        ///1 * D 3
        ///Total 280
        /// </summary>
        [TestMethod]
        public void TestScenario_C()
        {
            _cartManager.ClearCart();          
            _cartManager.AddToCart(sku_A, 3);
            _cartManager.AddToCart(sku_B, 5);
            _cartManager.AddToCart(sku_C, 1);
            _cartManager.AddToCart(sku_D, 1);

            _cartManager.ApplyPromotions();
            var totalPrice = _cartManager.TotalPrice;
            Assert.AreEqual(280, totalPrice);
        }

        /// <summary>
        ///    Scenario D (Custom)
        //1 * A 130
        //1 * B 120
        //1 * D 15
        //7 * E 640
        //Total 905
        /// </summary>
        [TestMethod]
        public void TestScenario_D()
        {
            _cartManager.ClearCart();          
            _cartManager.AddToCart(sku_A, 3);
            _cartManager.AddToCart(sku_B, 5);
            _cartManager.AddToCart(sku_D, 1);
            _cartManager.AddToCart(sku_E, 7);

            _cartManager.ApplyPromotions();
            var totalPrice = _cartManager.TotalPrice;
            Assert.AreEqual(905, totalPrice);
        }
    }
}
