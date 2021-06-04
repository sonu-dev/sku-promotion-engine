using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Sku
{
    public class Product
    {
        public Product(string name, int unitPrice) 
        {
            Name = name;
            UnitPrice = unitPrice;
        }

        public string Name { get; private set; }
        public int UnitPrice { get; private set; }       
    }
}
