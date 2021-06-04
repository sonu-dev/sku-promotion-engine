using PromotionEngine.Promotions;
using PromotionEngine.Sku;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Cart
{
    public class CartItem
    {
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = quantity * product.UnitPrice;
        }
        public Product Product { get; private set; }
        public int Quantity { get; set; }
        public int Price {get; set;}
        public bool HasAppliedPromotion { get;set;}
    }
}
