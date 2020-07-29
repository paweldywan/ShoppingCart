using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Models
{
    public class LineItem
    {
        public LineItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
