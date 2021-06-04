using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Promotions
{
    public class PromotionProduct
    {
        public PromotionProduct(string productName, int unit)
        {
            ProductName = productName;
            Unit = unit;
        }
        public string ProductName {get;set;}
        public int Unit { get; set; }
    }
}
