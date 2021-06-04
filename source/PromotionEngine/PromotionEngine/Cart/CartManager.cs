using PromotionEngine.Promotions;
using PromotionEngine.Sku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Cart
{
   public class CartManager
    {
        public int TotalPrice { get; private set; }
        public List<CartItem> CartItems { get; private set; }
        private readonly PromotionRulesManager _promotionRulesManager;

        public CartManager(PromotionRulesManager promotionRulesManager)
        {
            TotalPrice = 0;
            CartItems = new List<CartItem>();
            _promotionRulesManager = promotionRulesManager;
        }

        public void AddToCart(Product product, int unit)
        {
            var item = new CartItem(product, unit);
            CartItems.Add(item);
            TotalPrice = TotalPrice + item.Price;
        }       

        public void ApplyPromotions()
        {
            _promotionRulesManager.ApplyPromotionRules(CartItems);

            var totalPrice = 0;            
            CartItems.ForEach(item =>
            {
                if(item.HasAppliedPromotion)
                {
                    totalPrice = totalPrice + item.Price;
                }
                else
                {
                    totalPrice = totalPrice + item.Unit * item.Product.UnitPrice;
                }
            });
            TotalPrice = totalPrice;
        } 
        
        public void ClearCart()
        {
            CartItems.Clear();
            TotalPrice = 0;
        }
    }
}
