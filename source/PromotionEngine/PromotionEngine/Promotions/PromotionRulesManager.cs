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
        private readonly IList<PromotionRule> _rules;
        public PromotionRulesManager()
        {
            _rules = new List<PromotionRule>();
        }
        public void AddPromotionRule(PromotionRule rule)
        {
            if (_rules.Any(r => r.RuleName == rule.RuleName))
            {
                throw new InvalidOperationException($"Promotion Rule: {rule.RuleName} already exist.");
            }
            _rules.Add(rule);
        }       

        public List<PromotionRule> GetRules(bool isActive)
        {
            return _rules.Where(r => r.IsActive == isActive).ToList();
        }
        public void ApplyPromotionRules(List<CartItem> items)
        {
            var rules = GetRules(true);
            rules.ForEach(rule => rule.Execute(items));
        }
    }
}
