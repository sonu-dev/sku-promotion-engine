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

        #region Public Methods
        public void AddToCart(Product product, int quantity)
        {
            var cartItem = CartItems.FirstOrDefault(cI => product.Name.Equals(cI.Product.Name));
            if (cartItem != null)
            {
                UpdateCartItem(cartItem, quantity);
                return;
            }
            var newCartItem = new CartItem(product, quantity);
            CartItems.Add(newCartItem);
            TotalPrice = TotalPrice + newCartItem.Price;
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
                    totalPrice = totalPrice + item.Price;
                }
            });
            TotalPrice = totalPrice;
        } 
        
        public void ClearCart()
        {
            CartItems.Clear();
            TotalPrice = 0;
        }
        #endregion

        #region Private Methods
        private void UpdateCartItem(CartItem cartItem, int quantity)
        {
            cartItem.Quantity += quantity;
            cartItem.Price =  cartItem.Quantity * cartItem.Product.UnitPrice;
            RefreshTotalPrice();
        }

        private void RefreshTotalPrice()
        {
            var totalPrice = 0;
            CartItems.ForEach(item =>
            {
                totalPrice = totalPrice + item.Price;
            });
            TotalPrice = totalPrice;
        }
        #endregion
    }
}
