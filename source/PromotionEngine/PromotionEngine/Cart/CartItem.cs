using PromotionEngine.Promotions;
using PromotionEngine.Sku;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Cart
{
    public class CartItem
    {
        public CartItem(Product product, int unit)
        {
            Product = product;
            Unit = unit;
            Price = unit * product.UnitPrice;
        }
        public Product Product { get; set; }
        public int Unit { get; set; }
        public int Price {get;set;}
        public bool HasAppliedPromotion { get;set;}
    }
}
