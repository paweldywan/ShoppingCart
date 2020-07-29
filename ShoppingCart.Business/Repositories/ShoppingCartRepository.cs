using ShoppingCart.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly Dictionary<string, LineItem> lineItems;

        public ShoppingCartRepository()
        {
            lineItems = new Dictionary<string, LineItem>();
        }

        public void Add(Product product)
        {
            lineItems[product.ArticleId] = new LineItem(product, 1);
        }

        public void DecreaseQuantity(string articleId)
        {
            if (!lineItems.ContainsKey(articleId)) return;

            lineItems[articleId].Quantity--;
        }

        public void IncreaseQuantity(string articleId)
        {
            if (!lineItems.ContainsKey(articleId)) return;

            lineItems[articleId].Quantity++;
        }

        public LineItem Get(string articleId)
        {
            if(lineItems.ContainsKey(articleId))
            {
                return lineItems[articleId];
            }

            return null;
        }

        public void RemoveAll(string articleId)
        {
            lineItems.Remove(articleId);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            decimal sum = 0;

            foreach (var item in lineItems.Values)
            {
                var product = item.Product;
                int quantity = item.Quantity;
                decimal total = product.Price * quantity;
                sum += total;

                result.AppendFormat("{0} ${1} x {2} = ${3}", product.ArticleId, product.Price, quantity, total);
                result.AppendLine();
            }

            result.AppendFormat("Total price:    ${0}", sum);

            return result.ToString();
        }
    }

    public interface IShoppingCartRepository
    {
        void Add(Product product);
        void DecreaseQuantity(string articleId);
        void IncreaseQuantity(string articleId);
        LineItem Get(string articleId);
        void RemoveAll(string articleId);
    }
}
