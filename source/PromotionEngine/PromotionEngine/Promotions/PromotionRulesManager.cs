using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PromotionEngine.Sku;
using PromotionEngine.Cart;

namespace PromotionEngine.Promotions
{
    public class PromotionRulesManager
    {
        private readonly IList<PromotionRuleBase> _rules;
        public PromotionRulesManager()
        {
            _rules = new List<PromotionRuleBase>();
        }
        public void AddPromotionRule(PromotionRuleBase rule)
        {
            if (_rules.Any(r => r.RuleName.Equals(rule.RuleName)))
            {
                throw new InvalidOperationException($"Promotion Rule: {rule.RuleName} already exist.");
            }
            _rules.Add(rule);
        }     
       
        public void ApplyPromotionRules(List<CartItem> items)
        {
            var rules = _rules.Where(r => r.IsActive == true).ToList(); 
            rules.ForEach(rule => rule.Execute(items));
        }
    }
}
