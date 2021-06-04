using PromotionEngine.Cart;
using PromotionEngine.Sku;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Promotions
{
    public abstract class PromotionRuleBase
    {
        public PromotionRuleBase(string ruleName, List<PromotionProduct> promotionProducts)
        {
            RuleName = ruleName;
            PromotionProducts = promotionProducts;
            IsActive = true;
        }
        /// <summary>
        /// It will be used as rule unique identifier so it must be unique.
        /// </summary>
        public string RuleName { get; set; }
        public string RuleDescription { get; set; }
        public List<PromotionProduct> PromotionProducts { get; set; }
        public bool IsActive {get;set;}      
        public virtual void Execute(List<CartItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (ValidatePromotionProducts(items))
            {
                var ruleProductNames = PromotionProducts.Select(p => p.ProductName).ToList();
                var ruleItems = items.Where(i => ruleProductNames.Contains(i.Product.Name)).ToList();
                ApplyPromotions(ruleItems);
            }
        }
        public abstract List<CartItem> ApplyPromotions(List<CartItem> ruleItems);

        #region Private Methods
        private bool ValidatePromotionProducts(List<CartItem> items)
        {
            // Check if this rule have Promotion products assigned           
            if (PromotionProducts == null || PromotionProducts.Count == 0)
            {
                return false;
            }

            bool hasAllPromotionProductsToCart = true;
            foreach (var pProduct in PromotionProducts)
            {
                if (!items.Any(i => i.Product.Name.Equals(pProduct.ProductName)))
                {
                    hasAllPromotionProductsToCart = false;
                    break;
                }
            }
            return hasAllPromotionProductsToCart;
        }
        #endregion
    }
}
