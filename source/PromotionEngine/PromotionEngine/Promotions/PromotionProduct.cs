using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Promotions
{
    public class PromotionProduct
    {
        public PromotionProduct(string productName, int quantity)
        {
            ProductName = productName;
            Quantity = quantity;
        }
        public string ProductName {get;set;}
        public int Quantity { get; set; }
    }
}
