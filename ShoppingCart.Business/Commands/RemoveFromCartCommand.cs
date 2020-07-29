using ShoppingCart.Business.Models;
using ShoppingCart.Business.Repositories;
using System;
using System.Linq;

namespace ShoppingCart.Business.Commands
{
    public class RemoveFromCartCommand : ICommand
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;
        private readonly Product product;
        private int quantity;

        public RemoveFromCartCommand(IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository,
            Product product)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
            this.product = product;
        }

        public bool CanExecute()
        {
            if (product == null) return false;

            return shoppingCartRepository.Get(product.ArticleId).Quantity > 0;
        }

        public void Execute()
        {
            if (product == null) return;

            var lineItem = shoppingCartRepository.Get(product.ArticleId);

            quantity = lineItem.Quantity;

            productRepository.IncreaseStockBy(product.ArticleId, quantity);

            shoppingCartRepository.RemoveAll(product.ArticleId);
        }

        public void Undo()
        {
            productRepository.DecreaseStockBy(product.ArticleId, quantity);

            shoppingCartRepository.Add(product);

            for (int i = 1; i < quantity; i++)
            {
                shoppingCartRepository.IncreaseQuantity(product.ArticleId);
            }
        }
    }
}
