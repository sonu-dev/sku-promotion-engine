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
        
        public override List<CartItem> ApplyPromotions(List<CartItem> ruleItems)
        {
            // One item based rule
            if (ruleItems.Count == 1)
            {
                var item = ruleItems.First();
                var pProduct = PromotionProducts.FirstOrDefault(p => p.ProductName.Equals(item.Product.Name));
                if (item.Unit >= pProduct.Unit)
                {
                    var pUnits = item.Unit / pProduct.Unit;
                    var pRemUnits = item.Unit % pProduct.Unit;
                    item.Price = pUnits * _fixedPrice + pRemUnits * item.Product.UnitPrice;
                    item.HasAppliedPromotion = true;
                }
            }

            // Multiple items based rule
            if (ruleItems.Count > 1)
            {
                var rulePassed = false;
                foreach(var item in ruleItems)
                {
                    var pProduct = PromotionProducts.FirstOrDefault(p => p.ProductName.Equals(item.Product.Name));
                    if(item.Unit >= pProduct.Unit)
                    {
                        rulePassed = true;
                    }
                }
                if(rulePassed)
                {
                    var lastItem = ruleItems.Last();
                    lastItem.Price = _fixedPrice;
                    lastItem.HasAppliedPromotion = true;
                    ruleItems.Except(new List<CartItem> { lastItem }).ToList().ForEach(i =>
                    { 
                        i.Price = 0;
                        i.HasAppliedPromotion = true;
                    });
                }
            }

            return ruleItems;
        }
    }
}
