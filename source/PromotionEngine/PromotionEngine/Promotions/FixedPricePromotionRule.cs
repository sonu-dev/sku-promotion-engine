using PromotionEngine.Cart;
using PromotionEngine.Sku;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Promotions
{
    public class FixedPricePromotionRule : PromotionRule
    {
        private Int32 _fixedPrice;      
        public FixedPricePromotionRule(string ruleName, List<PromotionProduct> promotionProducts, int fixedPrice): base(ruleName, promotionProducts)
        {
            _fixedPrice = fixedPrice;
           
        }        
        
        public override List<CartItem> ApplyPromotion(List<CartItem> items)
        {
            var ruleProductNames = PromotionProducts.Select(p => p.ProductName).ToList();
            var eligibleItems = items.Where(i => ruleProductNames.Contains(i.Product.Name)).ToList();

            foreach(var item in eligibleItems)
            {
                if(item.HasAppliedPromotion)
                {                    
                    break;
                }

                var pProduct = PromotionProducts.FirstOrDefault(p => p.ProductName.Equals(item.Product.Name));
                if (item.Unit > pProduct.Unit)
                {
                    var pPUnits = item.Unit / pProduct.Unit;
                    var pUnits = item.Unit % pProduct.Unit;
                    item.Price = pPUnits * _fixedPrice + pUnits * item.Product.UnitPrice;
                    item.HasAppliedPromotion = true;                   
                }
            }          
            return items;
        }
    }
}
