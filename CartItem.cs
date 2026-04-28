using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crocusProject
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string Image { get; set; }   // ✅ ADD THIS
    }
}