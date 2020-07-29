using ShoppingCart.Business.Models;
using ShoppingCart.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Commands
{
    public class RemoveAllFromCartCommand : ICommand
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;
        private LineItem[] items;

        public RemoveAllFromCartCommand(IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        public bool CanExecute()
        {
            return shoppingCartRepository.All().Any(); //Zawiera jakikolwiek element
        }

        public void Execute()
        {
            items = shoppingCartRepository.All().ToArray(); // Make a local copy

            foreach (var lineItem in items)
            {
                productRepository.IncreaseStockBy(lineItem.Product.ArticleId, lineItem.Quantity);

                shoppingCartRepository.RemoveAll(lineItem.Product.ArticleId);
            }
        }

        public void Undo()
        {
            foreach (var lineItem in items)
            {
                int quantity = lineItem.Quantity;

                productRepository.DecreaseStockBy(lineItem.Product.ArticleId, quantity);

                shoppingCartRepository.Add(lineItem.Product);

                for (int i = 1; i < quantity; i++)
                {
                    shoppingCartRepository.IncreaseQuantity(lineItem.Product.ArticleId);
                }
            }
        }
    }
}
