using PromotionEngine.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Promotions
{
    public class PercentagePricePromotionRule : PromotionRule
    {
        private int _percentage;

        public PercentagePricePromotionRule(string ruleName, List<PromotionProduct> promotionProducts, int percentage) : base(ruleName, promotionProducts)
        {
            _percentage = percentage;
        }

        public override List<CartItem> ApplyPromotions(List<CartItem> ruleItems)
        {
            var item = ruleItems.FirstOrDefault();            
            if(item!= null)
            {
                var pProduct = PromotionProducts.FirstOrDefault(p => item.Product.Name.Equals(p.ProductName));

                if(item.Unit >= pProduct.Unit)
                {
                    var pUnits = item.Unit / pProduct.Unit;
                    var pRemUnits = item.Unit % pProduct.Unit;
                    item.Price = ((pUnits * pProduct.Unit * item.Product.UnitPrice) - (pUnits * pProduct.Unit * item.Product.UnitPrice * _percentage) / 100) + pRemUnits * item.Product.UnitPrice;
                    item.HasAppliedPromotion = true;
                }
            }
            return ruleItems;
        }
    }
}
